FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001
#ENV ASPNETCORE_URLS="http://localhost:5001"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://*:5001
ENV DbAdapterConfiguration__SqlConnectionString="server=database,1433;Initial Catalog=DbAnuncio;User ID=sa;Password=Testando@123;"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Anuncios.WebApi/Semear.Anuncios.WebApi/Semear.Anuncios.WebApi.csproj", "Anuncios.WebApi/Semear.Anuncios.WebApi/"]
RUN dotnet restore "Anuncios.WebApi/Semear.Anuncios.WebApi/Semear.Anuncios.WebApi.csproj"
COPY . .
WORKDIR "/src/Anuncios.WebApi/Semear.Anuncios.WebApi"
RUN dotnet build "Semear.Anuncios.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Semear.Anuncios.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Semear.Anuncios.WebApi.dll"]
