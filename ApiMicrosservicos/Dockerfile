FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy and build everything else
COPY . ./
RUN dotnet publish -c Release -o out

#Optimize size
FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 5000/tcp
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "Livraria.Api.dll"]
