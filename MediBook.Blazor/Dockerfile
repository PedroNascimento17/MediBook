FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MediBook.Blazor/MediBook.Blazor.csproj", "MediBook.Blazor/"]
COPY ["MediBook.Application/MediBook.Application.csproj", "MediBook.Application/"]
COPY ["MediBook.Infrastructure/MediBook.Infrastructure.csproj", "MediBook.Infrastructure/"]
COPY ["MediBook.Domain/MediBook.Domain.csproj", "MediBook.Domain/"]
RUN dotnet restore "./MediBook.Blazor/MediBook.Blazor.csproj"
COPY . .
WORKDIR "/src/MediBook.Blazor"
RUN dotnet build "./MediBook.Blazor.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MediBook.Blazor.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediBook.Blazor.dll"]