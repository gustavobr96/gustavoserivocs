FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

# Imagem de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia a solução
COPY ["SistemaBicoCore.sln", "./"]
COPY . .

# Restaura as dependências
RUN dotnet restore "SistemaBicoCore.sln"

# Builda a solução
RUN dotnet build "SistemaBicoCore.sln" -c Release -o /app/build

# Publica a api
RUN dotnet publish "SistemaBico.API/SistemaBico.API.csproj" -c Release -r linux-x64 --self-contained true -o /app/publish /p:PublishTrimmed=false /p:PublishSingleFile=false

# Imagem final
FROM base AS final
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/publish .

# Cria o volume e ajusta permissões
RUN mkdir -p /app/data && chmod 777 /app/data
VOLUME /app/data

# Define variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production

# EntryPoint da aplicação
ENTRYPOINT ["dotnet", "SistemaBico.API.dll"]