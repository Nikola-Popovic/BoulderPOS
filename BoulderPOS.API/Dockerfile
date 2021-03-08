FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 49156

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY *.csproj ./
RUN ["dotnet", "restore"]
COPY . .
RUN dotnet build "BoulderPOS.API.csproj" -c Release -o /app/build

## To run Ef migrations when starting Postgres
RUN chmod +x ./script/databaseStartup.sh
CMD /bin/bash ./script/databaseStartup.sh

FROM build AS publish
RUN dotnet publish -c Release -o /app/out

FROM base as final
WORKDIR /app
COPY --from=publish /app/out .
ENTRYPOINT ["dotnet", "BoulderPOS.API.dll"]