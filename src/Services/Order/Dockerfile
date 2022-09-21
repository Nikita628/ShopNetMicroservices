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
COPY ["src/Services/Order/Order.Api/Order.Api.csproj", "Services/Order/Order.Api/"]
COPY ["src/Services/Order/Order.Application/Order.Application.csproj", "Services/Order/Order.Application/"]
COPY ["src/Services/Order/Order.Core/Order.Core.csproj", "Services/Order/Order.Core/"]
COPY ["src/Services/Order/Order.Infrastructure/Order.Infrastructure.csproj", "Services/Order/Order.Infrastructure/"]
COPY ["src/Shared/EventBus/EventBus.csproj", "Shared/EventBus/"]
RUN dotnet restore "Services/Order/Order.Api/Order.Api.csproj"

# copy the whole service - you need to copy everything into the project
COPY ["src/Services/Order", "Services/Order"]
COPY ["src/Shared/EventBus", "Shared/EventBus"]

# build and publish the service - here you need to step into the directory containing .csproj file
WORKDIR /src/Services/Order/Order.Api
RUN dotnet build "Order.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]
