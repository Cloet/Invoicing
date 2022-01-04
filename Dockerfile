#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update; apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update; apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["Invoicing/Invoicing.csproj", "Invoicing/"]
COPY ["Invoicing.EntityFramework/Invoicing.EntityFramework.csproj", "Invoicing.EntityFramework/"]
COPY ["Invoicing.Domain/Invoicing.Domain.csproj", "Invoicing.Domain/"]
COPY ["Invoicing.Base/Invoicing.Base.csproj", "Invoicing.Base/"]
RUN dotnet restore "Invoicing/Invoicing.csproj"
COPY . .
WORKDIR "/src/Invoicing"
RUN dotnet build "Invoicing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Invoicing.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Invoicing.dll"]
