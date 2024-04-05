# Estágio de construção
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia os arquivos do projeto e restaura os pacotes NuGet
COPY ["../gustavoserivocs/Sistema.Bico.Infra/Sistema.Bico.Infra.csproj", "Sistema.Bico.Infra/"]
COPY ["../gustavoserivocs/SistemaBico.API/SistemaBico.API.csproj", "SistemaBico.API/"]
COPY ["../gustavoserivocs/Sistema.Bico.Domain/Sistema.Bico.Domain.csproj", "Sistema.Bico.Domain/"]
RUN dotnet restore "SistemaBico.API/SistemaBico.API.csproj"

# Copia o restante dos arquivos do projeto e compila
COPY . .
WORKDIR "/src/SistemaBico.API"
RUN dotnet build "SistemaBico.API.csproj" -c Release -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "SistemaBico.API.csproj" -c Release -o /app/publish

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Copia a aplicação publicada do estágio de publicação
COPY --from=publish /app/publish .

# Define o comando de entrada para iniciar a aplicação
ENTRYPOINT ["dotnet", "SistemaBico.API.dll"]