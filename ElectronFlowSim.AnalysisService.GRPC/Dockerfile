FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ElectronFlowSim.AnalysisService.GRPC/ElectronFlowSim.AnalysisService.GRPC.csproj", "ElectronFlowSim.AnalysisService.GRPC/"]
COPY ["ElectronFlowSim.DTO/ElectronFlowSim.DTO.csproj", "ElectronFlowSim.DTO/"]
RUN dotnet restore "ElectronFlowSim.AnalysisService.GRPC/ElectronFlowSim.AnalysisService.GRPC.csproj"
COPY . .
WORKDIR "/src/ElectronFlowSim.AnalysisService.GRPC"
RUN dotnet build "ElectronFlowSim.AnalysisService.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ElectronFlowSim.AnalysisService.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElectronFlowSim.AnalysisService.GRPC.dll"]
