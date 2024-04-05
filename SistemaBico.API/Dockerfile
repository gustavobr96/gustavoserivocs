#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Sistema.Bico.Infra/Sistema.Bico.Infra.csproj", "Sistema.Bico.Infra/"]
COPY ["SistemaBico.API/SistemaBico.API.csproj", "SistemaBico.API/"]
COPY ["Sistema.Bico.Domain/Sistema.Bico.Domain.csproj", "Sistema.Bico.Domain/"]

RUN dotnet restore "./SistemaBico.API/SistemaBico.API.csproj"
COPY . .
WORKDIR "/src/SistemaBico.API"
RUN dotnet build "./SistemaBico.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SistemaBico.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SistemaBico.API.dll"]