FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://*:5001
ENV DbAdapterConfiguration__SqlConnectionString="server=database,1433;Initial Catalog=DbUsuario;User ID=sa;Password=Testando@123;"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Usuarios.WebApi/Semear.Usuarios.WebApi/Semear.Usuarios.WebApi.csproj", "Usuarios.WebApi/Semear.Usuarios.WebApi/"]
RUN dotnet restore "Usuarios.WebApi/Semear.Usuarios.WebApi/Semear.Usuarios.WebApi.csproj"
COPY . .
WORKDIR "/src/Usuarios.WebApi/Semear.Usuarios.WebApi"
RUN dotnet build "Semear.Usuarios.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Semear.Usuarios.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Semear.Usuarios.WebApi.dll"]
