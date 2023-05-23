FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Blazor_Server/Blazor_Server.csproj", "Blazor_Server/"]
RUN dotnet restore "Blazor_Server/Blazor_Server.csproj"
COPY . .
WORKDIR "/src/Blazor_Server"
RUN dotnet build "Blazor_Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blazor_Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blazor_Server.dll"]
