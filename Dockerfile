# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia apenas o csproj e restaura
COPY src/Balder.FiapCloudGames.Api/*.csproj ./src/Balder.FiapCloudGames.Api/
RUN dotnet restore ./src/Balder.FiapCloudGames.Api/Balder.FiapCloudGames.Api.csproj

# Copia todo o conteúdo do projeto
COPY . .

# Publica o projeto
RUN dotnet publish ./src/Balder.FiapCloudGames.Api/Balder.FiapCloudGames.Api.csproj -c Release -o /app/publish

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Balder.FiapCloudGames.Api.dll"]
