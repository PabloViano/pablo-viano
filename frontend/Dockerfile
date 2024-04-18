FROM mcr.microsoft.com/dotnet/sdk:8.9.191-jammy-amd64 as build

COPY . /source

WORKDIR /source/SolucionInmobiliaria

RUN dotnet restore \
    && dotnet publish --configuration Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.9.191-jammy-amd64 as runtime

LABEL MAINTAINER pabloviano13@gmail.com

EXPOSE 5000

ENV ASPNETCORE_URLS="http://*:5000"

COPY --from=build /publish /app
WORKDIR /app
ENTRYPOINT ["dotnet", "SolucionInmobiliaria.dll"]