FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY DeliveryService/DeliveryService.csproj DeliveryService/
RUN dotnet restore DeliveryService/DeliveryService.csproj
COPY . .
WORKDIR /src/DeliveryService
RUN dotnet build DeliveryService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish DeliveryService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DeliveryService.dll"]