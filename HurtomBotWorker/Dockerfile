#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HurtomBotWorker/HurtomBotWorker.csproj", "HurtomBotWorker/"]
RUN dotnet restore "HurtomBotWorker/HurtomBotWorker.csproj"
COPY . .
WORKDIR "/src/HurtomBotWorker"
RUN dotnet build "HurtomBotWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HurtomBotWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HurtomBotWorker.dll"]