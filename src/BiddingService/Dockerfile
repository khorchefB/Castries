FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7003

ENV ASPNETCORE_URLS=http://+:7003

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/BiddingService/BiddingService.csproj", "src/BiddingService/"]
RUN dotnet restore "src/BiddingService/BiddingService.csproj"
COPY . .
WORKDIR "/src/src/BiddingService"
RUN dotnet build "BiddingService.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BiddingService.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BiddingService.dll"]
