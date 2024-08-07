# Use the official ASP.NET Core runtime image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiTest/ApiTest.csproj", "ApiTest/"]
RUN dotnet restore "ApiTest/ApiTest.csproj"
COPY . .
WORKDIR "/src/ApiTest"
RUN dotnet build "ApiTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiTest.csproj" -c Release -o /app/publish

# Use the base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiTest.dll"]
