#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Inflow.DataService/Inflow.DataService.csproj", "Inflow.DataService/"]
COPY ["Inflow.Common/Inflow.Common.csproj", "Inflow.Common/"]
COPY ["Inflow.Data/Inflow.Data.csproj", "Inflow.Data/"]
RUN dotnet restore "Inflow.DataService/Inflow.DataService.csproj"
COPY . .
WORKDIR "/src/Inflow.DataService"
RUN dotnet build "Inflow.DataService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inflow.DataService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inflow.DataService.dll"] 