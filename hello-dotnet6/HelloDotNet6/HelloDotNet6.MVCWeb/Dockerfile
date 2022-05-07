#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelloDotNet6.MVCWeb/HelloDotNet6.MVCWeb.csproj", "HelloDotNet6.MVCWeb/"]
COPY ["HelloDotNet6.Data/HelloDotNet6.Data.csproj", "HelloDotNet6.Data/"]
RUN dotnet restore "HelloDotNet6.MVCWeb/HelloDotNet6.MVCWeb.csproj"
COPY . .
WORKDIR "/src/HelloDotNet6.MVCWeb"
RUN dotnet build "HelloDotNet6.MVCWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloDotNet6.MVCWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloDotNet6.MVCWeb.dll"]