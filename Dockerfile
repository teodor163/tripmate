# Use official ASP.NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["TripMate_TeodorLazar.csproj", "."]
RUN dotnet restore "TripMate_TeodorLazar.csproj"

# Copy everything else and build the project
COPY . .
RUN dotnet build "TripMate_TeodorLazar.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "TripMate_TeodorLazar.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TripMate_TeodorLazar.dll"]
