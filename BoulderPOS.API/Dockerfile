FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /boulder-api/app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /boulder-api/src
COPY *.csproj ./
RUN ["dotnet", "restore"]
COPY . ./
RUN ["dotnet", "build"]

## To run Ef migrations when starting Postgres
RUN chmod +x ./script/databaseStartup.sh

FROM build AS publish
RUN dotnet publish -c Release -o /boulder-api/app/out

FROM base as final
WORKDIR /boulder-api/app
COPY --from=publish /boulder-api/app/out .
ENTRYPOINT ["dotnet", "BoulderPOS.API.dll"]