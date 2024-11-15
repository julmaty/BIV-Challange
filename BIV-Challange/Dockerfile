#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app/publish
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -ep BIV-Challange.pfx  -p crypticpassword
WORKDIR /src
COPY ["BIV-Challange/BIV-Challange.csproj", "BIV-Challange/"]
RUN dotnet restore "./BIV-Challange/BIV-Challange.csproj"
COPY . .
WORKDIR "/src/BIV-Challange"
RUN dotnet build "./BIV-Challange.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BIV-Challange.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN chmod og+r BIV-Challange.pfx
USER app
WORKDIR /app
ENTRYPOINT ["dotnet", "BIV-Challange.dll"]