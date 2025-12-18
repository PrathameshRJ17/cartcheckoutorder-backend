FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FoodOrderingSystem.API/FoodOrderingSystem.API.csproj", "FoodOrderingSystem.API/"]
RUN dotnet restore "FoodOrderingSystem.API/FoodOrderingSystem.API.csproj"
COPY . .
WORKDIR "/src/FoodOrderingSystem.API"
RUN dotnet build "FoodOrderingSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodOrderingSystem.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodOrderingSystem.API.dll"]