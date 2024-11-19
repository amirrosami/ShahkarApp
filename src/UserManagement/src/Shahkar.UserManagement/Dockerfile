#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/UserManagement/src/Shahkar.UserManagement/Shahkar.UserManagement.csproj", "src/UserManagement/src/Shahkar.UserManagement/"]
COPY ["src/UserManagement/src/Shahkar.UserManagement.AppService/Shahkar.UserManagement.AppService.csproj", "src/UserManagement/src/Shahkar.UserManagement.AppService/"]
COPY ["src/UserManagement/src/Shahkar.UserManagement.Db.Query/Shahkar.UserManagement.Db.Query.csproj", "src/UserManagement/src/Shahkar.UserManagement.Db.Query/"]
RUN dotnet restore "./src/UserManagement/src/Shahkar.UserManagement/Shahkar.UserManagement.csproj"
COPY . .
WORKDIR "/src/src/UserManagement/src/Shahkar.UserManagement"
RUN dotnet build "./Shahkar.UserManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Shahkar.UserManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shahkar.UserManagement.dll"]