using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using SriDurgaHariHaraBackend.Data.Models;

namespace SriDurgaHariHaraBackend.Application.ProductValidations
{
    public class InvoiceValidation : AbstractValidator<Invoice>
    {
         public InvoiceValidation()
        {
            //RuleFor(x => x.FyCode).NotEmpty().WithMessage("Name is required.")MinimumLength(3).WithMessage("At least 3 characters.");
        }
    }
}