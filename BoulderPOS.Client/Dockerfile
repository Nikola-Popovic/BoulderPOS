#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BoulderPOS.Client/BoulderPOS.Client.csproj", "BoulderPOS.Client/"]
RUN dotnet restore "BoulderPOS.Client/BoulderPOS.Client.csproj"
COPY . .
WORKDIR "/src/BoulderPOS.Client"
RUN dotnet build "BoulderPOS.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoulderPOS.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoulderPOS.Client.dll"]
