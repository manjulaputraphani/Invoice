using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace SriDurgaHariHaraBackend.Api.Attributes
{
     public class RequestValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public RequestValidationMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var attr = endpoint?.Metadata.GetMetadata<ValidateRequestAttribute>();
            if (attr == null || context.Request.ContentLength == null || context.Request.ContentLength == 0)
            {
                await _next(context);
                return;
            }

            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            object? dto;
            try
            {
                dto = JsonSerializer.Deserialize(body, attr.DtoType, _jsonOptions);
            }
            catch (JsonException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Malformed JSON in request body." });
                return;
            }

            if (dto == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Request body empty or could not be deserialized." });
                return;
            }

            // Resolve IValidator<TDto> dynamically
            var validatorType = typeof(IValidator<>).MakeGenericType(attr.DtoType);
            var validator = context.RequestServices.GetService(validatorType);
            if (validator == null)
            {
                // No validator registered -- skip validation
                await _next(context);
                return;
            }

            // Call ValidateAsync via reflection (returns Task<ValidationResult>)
            var validateAsync = validatorType.GetMethod("ValidateAsync", new[] { typeof(ValidationContext<>).MakeGenericType(attr.DtoType), typeof(CancellationToken) })
                              ?? validatorType.GetMethods().FirstOrDefault(m => m.Name == "ValidateAsync");

            if (validateAsync == null)
            {
                await _next(context);
                return;
            }

            // Build ValidationContext<TDto>
            var validationContextType = typeof(FluentValidation.ValidationContext<>).MakeGenericType(attr.DtoType);
            var ctor = validationContextType.GetConstructors().FirstOrDefault(c => c.GetParameters().Length == 1);
            var validationContext = ctor?.Invoke(new[] { dto });

            var task = (Task?)validateAsync.Invoke(validator, new[] { validationContext, CancellationToken.None });
            if (task == null)
            {
                await _next(context);
                return;
            }

            await task.ConfigureAwait(false);
            var validationResultProp = task.GetType().GetProperty("Result");
            var validationResult = (ValidationResult?)validationResultProp?.GetValue(task);

            if (validationResult != null && !validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => string.IsNullOrWhiteSpace(e.PropertyName) ? "_global" : e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { errors });
                return;
            }

            // Reset body for downstream
            var bytes = Encoding.UTF8.GetBytes(body);
            context.Request.Body = new MemoryStream(bytes);
            context.Request.ContentLength = bytes.Length;
            context.Request.Body.Position = 0;

            await _next(context);
        }
    }
}