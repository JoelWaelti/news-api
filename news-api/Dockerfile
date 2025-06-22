FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["news-api/news-api.csproj", "news-api/"]
RUN dotnet restore "./news-api/news-api.csproj"
COPY . .
WORKDIR "/src/news-api"
RUN dotnet publish "./news-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "news-api.dll"]