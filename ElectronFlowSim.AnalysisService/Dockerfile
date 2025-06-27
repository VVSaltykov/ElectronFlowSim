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

# Копируем исходники и собираем
COPY . .
WORKDIR "/src/ElectronFlowSim.AnalysisService"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Публикуем артефакты
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ с Wine и Xvfb
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER root
WORKDIR /app

# Устанавливаем системные пакеты
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
        wget \
        gnupg2 \
        software-properties-common \
        xvfb \
        xauth \
        x11vnc \
        fluxbox \
        xterm \
        libxcursor1 \
        libxrandr2 \
        libxi6 \
        libgl1-mesa-dri \
        libgl1-mesa-glx \
        libxtst6 \
        libxrender1 \
        procps \
        x11-utils \
        cabextract \
        unzip \
        curl \
        fonts-freefont-ttf && \
    # Устанавливаем winetricks вручную
    curl -fsSL https://raw.githubusercontent.com/Winetricks/winetricks/master/src/winetricks -o /usr/local/bin/winetricks && \
    chmod +x /usr/local/bin/winetricks && \
    # Добавляем i386-архитектуру и репозиторий WineHQ
    dpkg --add-architecture i386 && \
    mkdir -pm755 /etc/apt/keyrings && \
    wget -O /etc/apt/keyrings/winehq-archive.key https://dl.winehq.org/wine-builds/winehq.key && \
    echo "deb [signed-by=/etc/apt/keyrings/winehq-archive.key] https://dl.winehq.org/wine-builds/debian/ bookworm main" \
      | tee /etc/apt/sources.list.d/winehq.list && \
    apt-get update && \
    apt-get install -y --no-install-recommends \
        winehq-stable \
        wine32 \
        winbind \
        fonts-freefont-ttf && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

# Переменные окружения для Wine
ENV WINEPREFIX=/root/.wine \
    WINEARCH=win32 \
    DISPLAY=:99 \
    XVFB_ARGS="-screen 0 1024x768x16 -ac +extension GLX +render -noreset" \
    WINEDEBUG=-all

# Создаём префикс Wine
RUN mkdir -p /root/.wine && chown -R root:root /root/.wine

# Файл для отключения ненужных служб
RUN printf 'REGEDIT4\n\n\
[HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\winebth]\n"Start"=dword:00000004\n\n\
[HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\PlugPlay]\n"Start"=dword:00000004\n\n\
[HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\Eventlog]\n"Start"=dword:00000004\n' \
  > /root/disable_wine_services.reg

# Тест Xvfb
RUN Xvfb $DISPLAY $XVFB_ARGS & \
    sleep 3 && \
    xdpyinfo -display $DISPLAY >/dev/null 2>&1 && \
    pkill Xvfb

# Инициализация Wine (доработано с подавлением exit-кодов)
RUN Xvfb $DISPLAY $XVFB_ARGS & \
    sleep 5 && \
    wine wineboot --init || true && \
    sleep 10 && \
    pkill Xvfb || true

# Отключаем ненужные службы в реестре
RUN Xvfb $DISPLAY $XVFB_ARGS & \
    sleep 3 && \
    wine regedit /root/disable_wine_services.reg || true && \
    pkill Xvfb || true && \
    rm /root/disable_wine_services.reg

# Настройка цветности
RUN Xvfb $DISPLAY $XVFB_ARGS & \
    sleep 3 && \
    printf 'REGEDIT4\n\n[HKEY_CURRENT_USER\\Control Panel\\Colors]\n"BitDepth"="16"\n' \
      > /root/color.reg && \
    wine regedit /root/color.reg || true && \
    pkill Xvfb || true && \
    rm /root/color.reg

# Установка компонентов через winetricks (с подавлением ошибок)
RUN Xvfb $DISPLAY $XVFB_ARGS & sleep 3 && winetricks -q corefonts || true && pkill Xvfb || true
RUN Xvfb $DISPLAY $XVFB_ARGS & sleep 3 && winetricks -q vb6run    || true && pkill Xvfb || true
RUN Xvfb $DISPLAY $XVFB_ARGS & sleep 3 && winetricks -q mfc42    || true && pkill Xvfb || true
RUN Xvfb $DISPLAY $XVFB_ARGS & sleep 3 && winetricks -q d3d9    || true && pkill Xvfb || true

# Копируем готовое .NET-приложение
COPY --from=publish /app/publish .

# Точка входа
ENTRYPOINT ["dotnet", "ElectronFlowSim.AnalysisService.dll"]
