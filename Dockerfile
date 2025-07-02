FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagem de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia a solução e os projetos
COPY ["SistemaBicoCore.sln", "./"]
COPY . .

# Restaura as dependências
RUN dotnet restore "SistemaBicoCore.sln"

# Builda a solução
RUN dotnet build "SistemaBicoCore.sln" -c Release -o /app/build

# Publica a api
RUN dotnet publish "SistemaBico.API/SistemaBico.API.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/publish .

# Copia o arquivo firebase.json
COPY firebase.json ./firebase.json

# Cria o volume e ajusta permissões
RUN mkdir -p /app/data && chmod 777 /app/data
VOLUME /app/data

# Define variáveis de ambiente
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_SYSTEM_NET_SOCKETS_INLINE_COMPLETIONS=1

# EntryPoint da aplicação

ENTRYPOINT ["dotnet", "SistemaBico.API.dll"]
