#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.1-runtime-nanoserver-1803 AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY TestBarigui.Send/TestBarigui.Send.csproj TestBarigui.Send/
COPY TestBarigui/TestBarigui.Domain.Core.csproj TestBarigui/
COPY TestBarigui.Domain/TestBarigui.Domain.csproj TestBarigui.Domain/
COPY TestBarigui.Ioc/TestBarigui.Ioc.csproj TestBarigui.Ioc/
RUN dotnet restore TestBarigui.Send/TestBarigui.Send.csproj
COPY . .
WORKDIR /src/TestBarigui.Send
RUN dotnet build TestBarigui.Send.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TestBarigui.Send.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TestBarigui.Send.dll"]
