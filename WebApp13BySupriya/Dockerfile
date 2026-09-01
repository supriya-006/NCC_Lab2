FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project file and restore dependencies
COPY ["WebApp13BySupriya.csproj", "./"]
RUN dotnet restore "WebApp13BySupriya.csproj"

# Copy source code and publish
COPY . .
RUN dotnet publish "WebApp13BySupriya.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

ENTRYPOINT ["dotnet", "WebApp13BySupriya.dll"]