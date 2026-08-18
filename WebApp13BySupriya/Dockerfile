FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy csproj and restore
COPY ["WebApp13BySupriya.csproj", "./"]
RUN dotnet restore "WebApp13BySupriya.csproj"

# copy everything and publish
COPY . .
RUN dotnet publish "WebApp13BySupriya.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApp13BySupriya.dll"]
