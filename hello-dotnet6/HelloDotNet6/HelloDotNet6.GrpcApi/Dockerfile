#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelloDotNet6.GrpcApi/HelloDotNet6.GrpcApi.csproj", "HelloDotNet6.GrpcApi/"]
RUN dotnet restore "HelloDotNet6.GrpcApi/HelloDotNet6.GrpcApi.csproj"
COPY . .
WORKDIR "/src/HelloDotNet6.GrpcApi"
RUN dotnet build "HelloDotNet6.GrpcApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloDotNet6.GrpcApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloDotNet6.GrpcApi.dll"]