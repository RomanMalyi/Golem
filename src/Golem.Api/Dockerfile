FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Golem.Api/Golem.Api.csproj", "Golem.Api/"]
COPY ["Golem.Data/Golem.Data.csproj", "Golem.Data/"]
COPY ["Golem.Core/Golem.Core.csproj", "Golem.Core/"]
RUN dotnet restore "Golem.Api/Golem.Api.csproj"
COPY . .
WORKDIR "/src/Golem.Api"
RUN dotnet build "Golem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Golem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Golem.Api.dll"]