FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ElectronFlowSim.API/ElectronFlowSim.API.csproj", "ElectronFlowSim.API/"]
COPY ["ElectronFlowSim.Common/ElectronFlowSim.Common.csproj", "ElectronFlowSim.Common/"]
COPY ["ElectronFlowSim.DTO/ElectronFlowSim.DTO.csproj", "ElectronFlowSim.DTO/"]

RUN dotnet restore "ElectronFlowSim.API/ElectronFlowSim.API.csproj"
COPY . .
WORKDIR "/src/ElectronFlowSim.API"
RUN dotnet build "ElectronFlowSim.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ElectronFlowSim.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElectronFlowSim.API.dll"]
