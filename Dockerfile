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

# Copy only the project files first, into folders without redundant 'src/' prefix inside container
COPY ["src/SAS.EventsService.API/SAS.EventsService.API.csproj", "SAS.EventsService.API/"]
COPY ["src/SAS.EventsService.Application/SAS.EventsService.Application.csproj", "SAS.EventsService.Application/"]
COPY ["src/SAS.EventsService.Domain/SAS.EventsService.Domain.csproj", "SAS.EventsService.Domain/"]
COPY ["src/SAS.EventsService.Infrastructure/SAS.EventsService.Infrastructure.Persistence/SAS.EventsService.Infrastructure.Persistence.csproj", "SAS.EventsService.Infrastructure/SAS.EventsService.Infrastructure.Persistence/"]
COPY ["src/SAS.EventsService.Infrastructure/SAS.EventsService.Infrastructure.Services/SAS.EventsService.Infrastructure.Services.csproj", "SAS.EventsService.Infrastructure/SAS.EventsService.Infrastructure.Services/"]
COPY ["src/SAS.EventsService.Presentation/SAS.EventsService.Presentation.csproj", "SAS.EventsService.Presentation/"]

# Restore dependencies
RUN dotnet restore "SAS.EventsService.API/SAS.EventsService.API.csproj"

# Copy all source code files to the container
COPY . .

WORKDIR "/src/SAS.EventsService.API"

# Build the project
RUN dotnet build "SAS.EventsService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage to prepare the final output
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SAS.EventsService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage - production image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SAS.EventsService.API.dll"]
