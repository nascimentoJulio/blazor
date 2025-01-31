# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as dependências
COPY . .  
RUN dotnet restore  
RUN dotnet publish -c Release -o /out  

# Etapa 2: Imagem final para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0  
WORKDIR /app

# Copia os arquivos compilados da etapa anterior
COPY --from=build /out .  

# Define a porta padrão do container
ENV ASPNETCORE_URLS=http://+:8080  
EXPOSE 8080  

# Comando de inicialização
CMD ["dotnet", "DashJobs.dll"]
