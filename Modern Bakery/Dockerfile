# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore "ModernBakery.csproj"
RUN dotnet publish "ModernBakery.csproj" -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "ModernBakery.dll"]
