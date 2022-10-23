FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Services.csproj", "Services/"]
COPY ["DTO/DTO.csproj", "DTO/"]
COPY ["Persistance/Persistance.csproj", "Persistance/"]
COPY ["ExcuseMakerApi/ExcuseMakerApi.csproj", "ExcuseMakerApi/"]
COPY ["ExcuseMakerApi.sln", "./"]

RUN dotnet restore
COPY . .

RUN dotnet publish "ExcuseMakerApi/ExcuseMakerApi.csproj" -c Release -o /build

FROM base AS final
WORKDIR /app
COPY --from=build /build .
ENTRYPOINT ["dotnet", "ExcuseMakerApi.dll"]