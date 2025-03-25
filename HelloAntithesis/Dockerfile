###### Stage 1 of 2 - Build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore WebService/WebService.csproj
RUN dotnet build WebService/WebService.csproj --configuration Release --no-restore
RUN dotnet publish WebService/WebService.csproj --configuration Release --no-build -o out

##### Stage 2 of 2 - Production Image

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Symlink to the folder where Antithesis will automatically instrument the binaries.
RUN mkdir -p /opt/antithesis/catalog
RUN ln -s ../../../app /opt/antithesis/catalog/app

# Copy our Test Composer scripts to the folder where Antithesis will find them.
# Change the scripts' permissions to allow them to be executed.
COPY test_composer /opt/antithesis/test/v1/helloantithesis
RUN chmod -R +x /opt/antithesis/test/v1/helloantithesis

ENTRYPOINT ["dotnet", "WebService.dll"]