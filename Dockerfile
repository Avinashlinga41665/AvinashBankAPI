# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

# Expose the default port
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "AvinashBackEndAPI.dll"]
