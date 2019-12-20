#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Worker/DotNetSurfer_Backend.API/DotNetSurfer_Backend.API.csproj", "src/Worker/DotNetSurfer_Backend.API/"]
COPY ["src/Infrastructure/DotNetSurfer_Backend.Infrastructure/DotNetSurfer_Backend.Infrastructure.csproj", "src/Infrastructure/DotNetSurfer_Backend.Infrastructure/"]
COPY ["src/Core/DotNetSurfer_Backend.Core/DotNetSurfer_Backend.Core.csproj", "src/Core/DotNetSurfer_Backend.Core/"]
RUN dotnet restore "src/Worker/DotNetSurfer_Backend.API/DotNetSurfer_Backend.API.csproj"
COPY . .
WORKDIR "/src/src/Worker/DotNetSurfer_Backend.API"
RUN dotnet build "DotNetSurfer_Backend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetSurfer_Backend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetSurfer_Backend.API.dll"]