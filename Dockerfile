# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5200
# EXPOSE 8080
# EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SAS.EventsService.API/SAS.EventsService.API.csproj", "src/SAS.EventsService.API/"]
COPY ["src/SAS.EventsService.Application/SAS.EventsService.Application.csproj", "src/SAS.EventsService.Application/"]
COPY ["src/SAS.EventsService.Domain/SAS.EventsService.Domain.csproj", "src/SAS.EventsService.Domain/"]
COPY ["src/SAS.EventsService.Infrastructure/SAS.EventsService.Infrastructure.Persistence/SAS.EventsService.Infrastructure.Persistence.csproj", "src/SAS.EventsService.Infrastructure/SAS.EventsService.Infrastructure.Persistence/"]
COPY ["src/SAS.EventsService.Infrastructure/SAS.EventsService.Infrastructure.Services/SAS.EventsService.Infrastructure.Services.csproj", "src/SAS.EventsService.Infrastructure/SAS.EventsService.Infrastructure.Services/"]
COPY ["src/SAS.EventsService.Presentation/SAS.EventsService.Presentation.csproj", "src/SAS.EventsService.Presentation/"]
RUN dotnet restore "./src/SAS.EventsService.API/SAS.EventsService.API.csproj"
COPY . .
WORKDIR "/src/SAS.EventsService.API"
RUN dotnet build "./SAS.EventsService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SAS.EventsService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SAS.EventsService.API.dll"]
