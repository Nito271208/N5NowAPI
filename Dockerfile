FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app
COPY ./N5NowAPI/bin/Release/net8.0/publish/ .
ENTRYPOINT [ "dotnet", "N5NowAPI.dll"]