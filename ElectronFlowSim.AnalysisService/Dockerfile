# Базовый образ для рантайма ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем и восстанавливаем зависимости
COPY ["ElectronFlowSim.AnalysisService/ElectronFlowSim.AnalysisService.csproj", "ElectronFlowSim.AnalysisService/"]
COPY ["ElectronFlowSim.Common/ElectronFlowSim.Common.csproj", "ElectronFlowSim.Common/"]
RUN dotnet restore "ElectronFlowSim.AnalysisService/ElectronFlowSim.AnalysisService.csproj"

# Копируем остальные исходники и собираем проект
COPY . .
WORKDIR "/src/ElectronFlowSim.AnalysisService"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Публикуем артефакты
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с wine и xvfb
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER root
WORKDIR /app

# Устанавливаем необходимые пакеты для Wine
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
        wget \
        gnupg2 \
        software-properties-common && \
    # Добавляем архитектуру i386
    dpkg --add-architecture i386 && \
    # Добавляем репозиторий WineHQ
    mkdir -pm755 /etc/apt/keyrings && \
    wget -O /etc/apt/keyrings/winehq-archive.key https://dl.winehq.org/wine-builds/winehq.key && \
    echo "deb [signed-by=/etc/apt/keyrings/winehq-archive.key] https://dl.winehq.org/wine-builds/debian/ bookworm main" | tee /etc/apt/sources.list.d/winehq.list && \
    apt-get update && \
    # Устанавливаем Wine и зависимости (явно указываем wine32)
    apt-get install -y --no-install-recommends \
        winehq-stable \
        wine32 \
        xvfb \
        xauth \
        winbind \
        fonts-freefont-ttf \
        cabextract \
        unzip \
        p7zip-full && \
    # Устанавливаем winetricks
    wget -q https://raw.githubusercontent.com/Winetricks/winetricks/master/src/winetricks -O /usr/local/bin/winetricks && \
    chmod +x /usr/local/bin/winetricks && \
    # Очищаем кеш
    apt-get clean && \
    rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

# Инициализируем Wine prefix с явным указанием архитектуры
RUN WINEARCH=win32 WINEDEBUG=-all xvfb-run --auto-servernum --server-args="-screen 0 1024x768x16" wineboot --init && \
    # Даем больше времени на инициализацию
    sleep 30 && \
    # Настраиваем Windows 7 compatibility
    WINEARCH=win32 WINEDEBUG=-all xvfb-run --auto-servernum winetricks -q win7 && \
    # Устанавливаем основные шрифты Windows
    WINEARCH=win32 WINEDEBUG=-all xvfb-run --auto-servernum winetricks -q corefonts && \
    # Настраиваем глубину цвета
    echo '[HKEY_CURRENT_USER\Control Panel\Colors]' > /tmp/colors.reg && \
    echo '"BitDepth"="16"' >> /tmp/colors.reg && \
    WINEARCH=win32 xvfb-run --auto-servernum wine regedit /tmp/colors.reg && \
    rm /tmp/colors.reg

# Создаем скрипт-обертку для запуска wine через xvfb
RUN echo '#!/bin/sh\nXvfb :99 -screen 0 1024x768x16 &\nPID=$!\nexport DISPLAY=:99\nWINEARCH=win32 wine "$@"\nkill $PID' > /usr/local/bin/winewrap && \
    chmod +x /usr/local/bin/winewrap

# Копируем собранное .NET приложение
COPY --from=publish /app/publish .

# Точка входа
ENTRYPOINT ["dotnet", "ElectronFlowSim.AnalysisService.dll"]