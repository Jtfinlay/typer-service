FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["TyperService/TyperService.csproj", "TyperService/"]
RUN dotnet restore "TyperService/TyperService.csproj"
COPY . .
WORKDIR "/src/TyperService"
RUN dotnet build "TyperService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TyperService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TyperService.dll"]