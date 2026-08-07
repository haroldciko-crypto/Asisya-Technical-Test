FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY . .

RUN dotnet restore "src/Asisya.Api/Asisya.Api.csproj"

RUN dotnet publish "src/Asisya.Api/Asisya.Api.csproj" \
    -c Release \
    -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Asisya.Api.dll"]