FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build-env
WORKDIR /app

COPY . .
RUN dotnet build
RUN dotnet publish MoviePlaybackSystem.ConsoleUI/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:2.2
WORKDIR /app

COPY --from=build-env /app/MoviePlaybackSystem.ConsoleUI/out/ ./
ENTRYPOINT [ "dotnet", "MoviePlaybackSystem.ConsoleUI.dll" ]
