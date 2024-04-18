# Estágio de construção
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
EXPOSE 8080
WORKDIR /src
COPY . .
ENV ASPNETCORE_URLS="http://*:8080"


# Define o comando de entrada para iniciar a aplicação
ENTRYPOINT ["dotnet", "SistemaBico.Web.dll"]