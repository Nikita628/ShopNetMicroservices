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
COPY ["src/ApiGateways/Shop.Aggregator/Shop.Aggregator.csproj", "ApiGateways/Shop.Aggregator/"]
RUN dotnet restore "ApiGateways/Shop.Aggregator/Shop.Aggregator.csproj"

# copy the whole service - you need to copy everything into the project
COPY ["src/ApiGateways/Shop.Aggregator", "ApiGateways/Shop.Aggregator"]

# build and publish the service - here you need to step into the directory containing .csproj file
WORKDIR /src/ApiGateways/Shop.Aggregator
RUN dotnet build "Shop.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Aggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Aggregator.dll"]
