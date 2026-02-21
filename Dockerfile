# Use the official .NET 10.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files for caching
# COPY src/HMS.sln ./
COPY src/HMS.Api/HMS.Api.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY src/HMS.Api ./

# Build the API
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 10.0 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Set the URL to listen on all interfaces
ENV ASPNETCORE_URLS=http://+:8080

# Expose the port the app runs on
EXPOSE 8080

# Set the entry point
ENTRYPOINT ["dotnet", "HMS.Api.dll"]