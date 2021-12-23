FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["CarteiraSafra.csproj", "./"]
RUN dotnet restore "CarteiraSafra.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CarteiraSafra.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarteiraSafra.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m myappuser
USER myappuser

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet CarteiraSafra.dll
