#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Inflow.Core.DataService/Inflow.Core.DataService.csproj", "Inflow.Core.DataService/"]
COPY ["Inflow.Core.Common/Inflow.Core.Common.csproj", "Inflow.Core.Common/"]
COPY ["Inflow.Core.Data/Inflow.Core.Data.csproj", "Inflow.Core.Data/"]
RUN dotnet restore "Inflow.Core.DataService/Inflow.Core.DataService.csproj"
COPY . .
WORKDIR "/src/Inflow.Core.DataService"
RUN dotnet build "Inflow.Core.DataService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inflow.Core.DataService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inflow.Core.DataService.dll"] 