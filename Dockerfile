FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

USER root
RUN mkdir -p /app/DataProtectionKeys && chown -R $APP_UID:$APP_UID /app/DataProtectionKeys
USER $APP_UID

WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DeoTools/GeoTools/GeoTools.csproj", "GeoTools/GeoTools/"]
COPY ["GeoTools/GeoTools.Client/GeoTools.Client.csproj", "GeoTools/GeoTools.Client/"]
RUN dotnet restore "GeoTools/GeoTools/GeoTools.csproj"
COPY . .
WORKDIR "/src/GeoTools/GeoTools"
RUN dotnet build "GeoTools.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GeoTools.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeoTools.dll"]