FROM mcr.microsoft.com/dotnet/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:2.2 AS build
WORKDIR /src
COPY ["RefactorThis.Api.scproj", "./"]
RUN dotnet restore "RefactorThis.Api.scproj"
COPY . .
RUN dotnet publish "RefactorThis.Api.scproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RefactorThis.Api.dll"]
