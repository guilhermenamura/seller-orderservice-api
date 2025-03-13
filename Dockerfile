# Etapa 1: Construir a aplicação
# Usamos a imagem SDK do .NET para compilar o código
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia apenas os arquivos necessários (do diretório da WebAPI)
COPY seller-orderservice-api.WebAPI/seller-orderservice-api.WebAPI.csproj ./seller-orderservice-api.WebAPI/
COPY seller-orderservice-api.Application/seller-orderservice-api.Application.csproj ./seller-orderservice-api.Application/
COPY seller-orderservice-api.Infrastructure/seller-orderservice-api.Infrastructure.csproj ./seller-orderservice-api.Infrastructure/
COPY seller-orderservice-api.Domain/seller-orderservice-api.Domain.csproj ./seller-orderservice-api.Domain/

# Restaura as dependências
RUN dotnet restore seller-orderservice-api.WebAPI/seller-orderservice-api.WebAPI.csproj

# Copia o restante dos arquivos de código fonte
COPY . ./ 

# Compila o projeto
RUN dotnet build seller-orderservice-api.WebAPI/seller-orderservice-api.WebAPI.csproj -c Release -o /app/build

# Etapa 2: Publicar a aplicação
# Publicamos a aplicação para produção
FROM build AS publish
RUN dotnet publish seller-orderservice-api.WebAPI/seller-orderservice-api.WebAPI.csproj -c Release -o /app/publish

# Etapa 3: Imagem final
# Usamos a imagem base de runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copia os arquivos compilados da etapa anterior
COPY --from=publish /app/publish .

# Configura o Kestrel para escutar em todas as interfaces (não só localhost)
ENV ASPNETCORE_URLS=http://+:80

# Expõe a porta 80 para que o contêiner seja acessível
EXPOSE 80

# Comando para rodar a aplicação quando o contêiner for iniciado
ENTRYPOINT ["dotnet", "seller-orderservice-api.WebAPI.dll"]
