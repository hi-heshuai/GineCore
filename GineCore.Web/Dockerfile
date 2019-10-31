FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["GineCore.Web/GineCore.Web.csproj", "GineCore.Web/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Entity/Entity.csproj", "Entity/"]
COPY ["EntityFrameworkCore/EntityFrameworkCore.csproj", "EntityFrameworkCore/"]
RUN dotnet restore "GineCore.Web/GineCore.Web.csproj"
COPY . .
WORKDIR "/src/GineCore.Web"
RUN dotnet build "GineCore.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GineCore.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GineCore.Web.dll"]