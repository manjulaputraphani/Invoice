# Runtime-only image that uses your published 'out' folder
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published files (your `out/` folder)
COPY out/ .

# Let the container listen on the port provided by the platform (we expose 8080)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SriDurgaHariHaraBackend.Api.dll"]
