# Use the .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything from the current directory (.) to the /app directory in the container
COPY . ./

# Restore dependencies for the project using the dotnet restore command
RUN dotnet restore

# Build and publish a release version of the application in the /out directory
RUN dotnet publish -c Release -o /out

# Build the runtime image using the .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published application from the build-env stage to the /app directory in the runtime image
COPY --from=build-env /out .

# Set environment variables for the application
ENV ASP_PORT=8080
ENV POSTGRES_HOST=postgres
ENV POSTGRES_PORT=5432
ENV POSTGRES_USER=postgres
ENV POSTGRES_PASSWORD=postgres
ENV POSTGRES_DB=postgres

# Expose the port 8080 for the application
EXPOSE $ASP_PORT

# Set the entry point for the application to run when the container is started
ENTRYPOINT ["dotnet", "source.dll"]
