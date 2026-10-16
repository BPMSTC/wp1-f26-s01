FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY src/Wp1.Web/Wp1.Web.csproj src/Wp1.Web/
RUN dotnet restore src/Wp1.Web/Wp1.Web.csproj
COPY src/ src/
RUN dotnet publish src/Wp1.Web/Wp1.Web.csproj -c Release --no-restore -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "Wp1.Web.dll"]
