# ====== BUILD STAGE ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# ====== RUNTIME STAGE ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Render задаЄт переменную PORT Ч об€заны еЄ слушать
ENV ASPNETCORE_URLS=http://+:${PORT}

ENTRYPOINT ["dotnet", "DeviceStatsT.dll"]
