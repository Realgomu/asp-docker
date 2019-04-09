FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

# copy project files
WORKDIR /app
COPY Api/. ./Api/

# restore
WORKDIR /app/Api
RUN dotnet restore

# publish
WORKDIR /app/Api
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/Api/out ./
ENTRYPOINT ["dotnet", "Api.dll"]