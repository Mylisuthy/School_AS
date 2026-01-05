FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SchoolAs.Api/SchoolAs.Api.csproj", "SchoolAs.Api/"]
COPY ["SchoolAs.Application/SchoolAs.Application.csproj", "SchoolAs.Application/"]
COPY ["SchoolAs.Domain/SchoolAs.Domain.csproj", "SchoolAs.Domain/"]
COPY ["SchoolAs.Infrastructure/SchoolAs.Infrastructure.csproj", "SchoolAs.Infrastructure/"]
RUN dotnet restore "./SchoolAs.Api/SchoolAs.Api.csproj"
COPY . .
WORKDIR "/src/SchoolAs.Api"
RUN dotnet build "./SchoolAs.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SchoolAs.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchoolAs.Api.dll"]
