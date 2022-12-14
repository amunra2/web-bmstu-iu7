FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers

COPY *.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY /. ./
WORKDIR /app/
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
# EXPOSE 5005

ENV ASPNETCORE_URLS=http://+:5005
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "ServerING.dll"]

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 5005

# ENV ASPNETCORE_URLS=http://+:5005

# # Creates a non-root user with an explicit UID and adds permission to access the /app folder
# # For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["ServerING.csproj", "./"]
# RUN dotnet restore "ServerING.csproj"
# COPY . .
# WORKDIR "/src/."
# RUN dotnet build "ServerING.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "ServerING.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "ServerING.dll"]
