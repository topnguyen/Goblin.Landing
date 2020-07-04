# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .

COPY Goblin.Core/Goblin.Core/*.csproj ./Goblin.Core/Goblin.Core/
COPY Goblin.Core/Goblin.Core.Web/*.csproj ./Goblin.Core/Goblin.Core.Web/

COPY src/Cross/Goblin.Landing.Core/*.csproj ./src/Cross/Goblin.Landing.Core/
COPY src/Cross/Goblin.Landing.Mapper/*.csproj ./src/Cross/Goblin.Landing.Mapper/

COPY src/Service/Goblin.Landing.Contract.Service/*.csproj ./src/Service/Goblin.Landing.Contract.Service/
COPY src/Service/Goblin.Landing.Service/*.csproj ./src/Service/Goblin.Landing.Service/

COPY src/Web/Goblin.Landing/*.csproj ./src/Web/Goblin.Landing/

RUN dotnet restore

# copy everything else and build app

COPY Goblin.Core/Goblin.Core/. ./Goblin.Core/Goblin.Core/
COPY Goblin.Core/Goblin.Core.Web/. ./Goblin.Core/Goblin.Core.Web/

COPY src/Cross/Goblin.Landing.Core/. ./src/Cross/Goblin.Landing.Core/
COPY src/Cross/Goblin.Landing.Mapper/. ./src/Cross/Goblin.Landing.Mapper/

COPY src/Service/Goblin.Landing.Contract.Service/. ./src/Service/Goblin.Landing.Contract.Service/
COPY src/Service/Goblin.Landing.Service/. ./src/Service/Goblin.Landing.Service/

COPY src/Web/Goblin.Landing/. ./src/Web/Goblin.Landing/

WORKDIR /source
RUN dotnet publish -c release -o /publish --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /publish
COPY --from=build /publish ./
ENTRYPOINT ["dotnet", "Goblin.Landing.dll"]