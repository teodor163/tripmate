FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["TripMate_TeodorLazar/TripMate_TeodorLazar.csproj", "TripMate_TeodorLazar/"]

RUN dotnet restore "TripMate_TeodorLazar/TripMate_TeodorLazar.csproj"

COPY . .

WORKDIR "/src/TripMate_TeodorLazar"

RUN dotnet publish "TripMate_TeodorLazar.csproj" -c Release -o /app/publish /p:useAppHost=false
FROM base AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TripMate_TeodorLazar.dll"]