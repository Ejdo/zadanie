FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5085

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Server-Mac.csproj", "."]
RUN dotnet restore "./Server-Mac.csproj"
COPY . .
ENTRYPOINT ["dotnet", "run"]