#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/Catalog/JCP.Catalog.API/JCP.Catalog.API.csproj", "Services/Catalog/JCP.Catalog.API/"]
COPY ["Services/Catalog/JCP.Catalog.Domain/JCP.Catalog.Domain.csproj", "Services/Catalog/JCP.Catalog.Domain/"]
COPY ["Services/Catalog/JCP.Catalog.Infrastructure/JCP.Catalog.Infrastructure.csproj", "Services/Catalog/JCP.Catalog.Infrastructure/"]
RUN dotnet restore "Services/Catalog/JCP.Catalog.API/JCP.Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/JCP.Catalog.API"
RUN dotnet build "JCP.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JCP.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JCP.Catalog.API.dll"]