# Use the .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build and publish a release version
RUN dotnet publish -c Release -o /out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

COPY --from=build-env /out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ConsoleApp1.dll"]
