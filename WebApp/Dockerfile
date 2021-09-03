FROM mcr.microsoft.com/dotnet/aspnet:6.0.0-preview.7-bullseye-slim-amd64
COPY bin/Release/net6.0/publish/ app/
WORKDIR /app
ENTRYPOINT ["dotnet", "WebApp.dll"]

