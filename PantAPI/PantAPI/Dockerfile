FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /app
ENV HOME=/root
# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS="http://+:5000"
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "PantAPI.dll"]