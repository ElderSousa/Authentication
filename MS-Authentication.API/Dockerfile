# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MS-Authentication.API/MS-Authentication.API.csproj", "MS-Authentication.API/"]
COPY ["MS-Authentication.CrossCutting/MS-Authentication.CrossCutting.csproj", "MS-Authentication.CrossCutting/"]
COPY ["MS-Authentication.Application/MS-Authentication.Application.csproj", "MS-Authentication.Application/"]
COPY ["MS-Authentication.Domain/MS-Authentication.Domain.csproj", "MS-Authentication.Domain/"]
COPY ["MS-Authentication.Infrastructure/MS-Authentication.Infrastructure.csproj", "MS-Authentication.Infrastructure/"]
RUN dotnet restore "./MS-Authentication.API/MS-Authentication.API.csproj"
COPY . .
WORKDIR "/src/MS-Authentication.API"
RUN dotnet build "./MS-Authentication.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MS-Authentication.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS-Authentication.API.dll"]