# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the shared library files if any
COPY ./shared/ ./shared/

# Copy the microservice source code
COPY ./services/${SERVICE_NAME}/ ./services/${SERVICE_NAME}/

# Restore dependencies
RUN dotnet restore ./services/${SERVICE_NAME}/src/${SERVICE_NAME}.csproj

# Publish the application
RUN dotnet publish ./services/${SERVICE_NAME}/src/${SERVICE_NAME}.csproj -c Release -o /app/publish

# Use the official ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "${SERVICE_NAME}.dll"]
