FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV MONGO_CONNECTION_STRING=NOVALUE
ENV MONGO_DATABASE_NAME=NONAME
ENV MONGO_COLLECTION_NAME=NONAME
WORKDIR /src
COPY mongo.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "mongo.dll"]