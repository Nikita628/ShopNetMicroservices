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
COPY ["src/Services/Basket/Basket.Api/Basket.Api.csproj", "Services/Basket/Basket.Api/"]
COPY ["src/Services/Basket/Basket.Application/Basket.Application.csproj", "Services/Basket/Basket.Application/"]
COPY ["src/Services/Discount/Discount.Grpc/Discount.Grpc.csproj", "Services/Discount/Discount.Grpc/"]
COPY ["src/Shared/EventBus/EventBus.csproj", "Shared/EventBus/"]
RUN dotnet restore "Services/Basket/Basket.Api/Basket.Api.csproj"

# copy the whole service - you need to copy everything into the project
COPY ["src/Services/Basket", "Services/Basket"]
COPY ["src/Services/Discount/Discount.Grpc", "Services/Discount/Discount.Grpc"]
COPY ["src/Shared/EventBus", "Shared/EventBus"]

# build and publish the service - here you need to step into the directory containing .csproj file
WORKDIR /src/Services/Basket/Basket.Api
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]
