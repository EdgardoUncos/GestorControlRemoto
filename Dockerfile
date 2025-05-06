# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar todo el código fuente
COPY . .

# Restaurar dependencias
RUN dotnet restore GestorControlRemoto.sln

# Publicar el proyecto Server (ruta corregida)
RUN dotnet publish GestorControlRemoto/Server/GestorControlRemoto.Server.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./
EXPOSE 80
ENTRYPOINT ["dotnet", "GestorControlRemoto.Server.dll"]






