FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5042

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["configuracion-ms/Configuracion-ms.csproj", "configuracion-ms/"]
RUN dotnet restore "./configuracion-ms/Configuracion-ms.csproj"
COPY . .
WORKDIR "/src/configuracion-ms"
RUN dotnet build "Configuracion-ms.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Configuracion-ms.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "configuracion-ms.dll"]
