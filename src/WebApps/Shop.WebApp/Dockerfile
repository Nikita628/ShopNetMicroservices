FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy project files and start restoring
COPY ["src/WebApps/Shop.WebApp/Shop.WebApp.csproj", "WebApps/Shop.WebApp"]
RUN dotnet restore "WebApps/Shop.WebApp/Shop.WebApp.csproj"

# copy the whole service - you need to copy everything into the project
COPY ["src/WebApps/Shop.WebApp", "WebApps/Shop.WebApp"]

# build and publish the service - here you need to step into the directory containing .csproj file
WORKDIR /src/WebApps/Shop.WebApp
RUN dotnet build "Shop.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.WebApp.dll"]
