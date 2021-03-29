FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY *.csproj ./
RUN ["dotnet", "restore"]
COPY . .
RUN dotnet build "BoulderPOS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/out

FROM base as final
WORKDIR /app
COPY --from=publish /app/out .
ENTRYPOINT ["dotnet", "BoulderPOS.API.dll"]