FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Journalism.RestApi/Journalism.RestApi.csproj", "Journalism.RestApi/"]
RUN dotnet restore "Journalism.RestApi/Journalism.RestApi.csproj"
COPY . .
WORKDIR "/src/Journalism.RestApi"
RUN dotnet build "Journalism.RestApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Journalism.RestApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Journalism.RestApi.dll"]
