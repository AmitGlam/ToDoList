#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.7 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0.302 AS build
WORKDIR /src
COPY ["dMvcToDoList.csproj", "."]
RUN dotnet restore "./MvcToDoList.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MvcToDoList.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MvcToDoList.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY .git /.git
ENTRYPOINT ["dotnet", "ToDoList.dll"]