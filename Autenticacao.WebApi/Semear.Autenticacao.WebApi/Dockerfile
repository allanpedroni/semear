FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001
#ENV ASPNETCORE_URLS="http://localhost:5001"
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://*:5001
ENV DbAdapterConfiguration__SqlConnectionString="server=database,1433;Initial Catalog=DbUsuario;User ID=sa;Password=Testando@123;"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Autenticacao.WebApi/Semear.Autenticacao.WebApi/Semear.Autenticacao.WebApi.csproj", "Autenticacao.WebApi/Semear.Autenticacao.WebApi/"]
RUN dotnet restore "Autenticacao.WebApi/Semear.Autenticacao.WebApi/Semear.Autenticacao.WebApi.csproj"
COPY . .
WORKDIR "/src/Autenticacao.WebApi/Semear.Autenticacao.WebApi"
RUN dotnet build "Semear.Autenticacao.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Semear.Autenticacao.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Semear.Autenticacao.WebApi.dll"]
