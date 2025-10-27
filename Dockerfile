# Osnovna slika za ASP.NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Slika za izgradnjo aplikacije
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# kopiranje .csproj v delovni direktorij
COPY ["TripMate_TeodorLazar.csproj", "./"]

# obnovitev odvisnosti
RUN dotnet restore "TripMate_TeodorLazar.csproj"

# kopiranje ostalih datotek
COPY . .

# izgradnja projekta
RUN dotnet build "TripMate_TeodorLazar.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TripMate_TeodorLazar.csproj" -c Release -o /app/publish /p:UseAppHost=false

# končna slika
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TripMate_TeodorLazar.dll"]
