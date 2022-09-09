# docker container - environment that contains your running app (image of an app)
# image - app and dependencies

# ----------------------------------------------------------------------------------------

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
COPY ["src/Services/Catalog/Catalog.Api/Catalog.Api.csproj", "Services/Catalog/Catalog.Api/"]
COPY ["src/Services/Catalog/Catalog.Application/Catalog.Application.csproj", "Services/Catalog/Catalog.Application/"]
RUN dotnet restore "Services/Catalog/Catalog.Api/Catalog.Api.csproj"

# copy the whole service - you need to copy everything into the project
COPY ["src/Services/Catalog", "Services/Catalog"]

# build and publish the service - here you need to step into the directory containing .csproj file
WORKDIR /src/Services/Catalog/Catalog.Api
RUN dotnet build "Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]
