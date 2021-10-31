FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
COPY Models/Models.csproj ./Models/
COPY Repository/Repository.csproj ./Repository/
COPY Services/Services.csproj ./Services/
COPY BusinessLogic/BusinessLogic.csproj ./BusinessLogic/
COPY UnitTests/UnitTests.csproj ./UnitTests/
COPY WebAPI/WebAPI.csproj ./WebAPI/
COPY WebAPI.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as run-env
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
