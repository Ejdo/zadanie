
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /app

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY . /src
WORKDIR /src
RUN dotnet build "Server-Mac.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server-Mac.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server-Mac.dll"]