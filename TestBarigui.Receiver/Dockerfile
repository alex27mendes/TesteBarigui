

FROM microsoft/dotnet:2.1-runtime-nanoserver-1803 AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY TestBarigui.Receiver/TestBarigui.Receiver.csproj TestBarigui.Receiver/
COPY TestBarigui/TestBarigui.Domain.Core.csproj TestBarigui/
COPY TestBarigui.Domain/TestBarigui.Domain.csproj TestBarigui.Domain/
COPY TestBarigui.Ioc/TestBarigui.Ioc.csproj TestBarigui.Ioc/
RUN dotnet restore TestBarigui.Receiver/TestBarigui.Receiver.csproj
COPY . .
WORKDIR /src/TestBarigui.Receiver
RUN dotnet build TestBarigui.Receiver.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TestBarigui.Receiver.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TestBarigui.Receiver.dll"]


# Docker-compose for Graylog


