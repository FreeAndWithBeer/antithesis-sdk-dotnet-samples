###### Stage 1 of 2 - Build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet build --no-restore
RUN dotnet publish --no-build -o out

##### Stage 2 of 2 - Production Image

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Symlink to the folder where Antithesis will automatically instrument the binaries.
RUN mkdir -p /opt/antithesis/catalog
RUN ln -s ../../../app /opt/antithesis/catalog/app

ENTRYPOINT ["dotnet", "HelloWeb.dll"]