FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["inSync.Web/inSync.Web.csproj", "inSync.Web/"]
RUN dotnet restore "inSync.Web/inSync.Web.csproj"
COPY . .
WORKDIR "/src/inSync.Web"
RUN dotnet build "inSync.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "inSync.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "inSync.Web.dll"]
