seller-orderservice-api
Revenda Service (Cadastra revendas).

Estrutura da Arquitetura
A arquitetura do projeto segue o padrão de camadas, facilitando a manutenção e escalabilidade da aplicação. As camadas principais são:

Web/API (Controller): Responsável por receber as requisições HTTP e delegar o processamento.
Application (Serviço de Casos de Uso): Contém a lógica de aplicação, orquestrando os serviços de negócio.
Domain (Entidades e Lógica de Negócios): Contém as entidades e regras de negócio.
Service (Integrações com bancos de dados e outros sistemas): Responsável pela comunicação com o banco de dados e integrações com sistemas externos, como o RabbitMQ.
RabbitMQ (Fila): Utilizado para comunicação assíncrona entre microserviços.
Estrutura de Diretórios

![image](https://github.com/user-attachments/assets/2fde9b29-8610-42eb-897c-831f65099b5f)


Fluxo da Arquitetura
1. Web/API (Controller)
Controller (API)
Recebe requisições HTTP.
Valida entrada de dados.
Delegação para os serviços na camada Application.
2. Application (Serviços de Casos de Uso)
PedidoService
Valida regras de negócios.
Interage com RabbitMQ.
Orquestra lógica de negócio.
3. Domain (Entidades e Lógica de Negócios)
Pedido (Entidades)
Definição de entidades e validações.
Produtos (Entidades)
Entidades associadas aos produtos.
Regras de negócios
Implementação das regras de validação de pedidos e produtos.
4. Service (Integrações com RabbitMQ e Banco de Dados)
RabbitMQ

Envia e consome mensagens da fila.
Comunicação assíncrona com outros microserviços.
Banco de Dados

Acesso a banco de dados relacional e não relacional.
Repositórios para persistência de dados.
5. Infraestrutura
Configuração de RabbitMQ
Setup do broker de mensagens.
Configuração de Banco de Dados
Setup de conexão com os bancos de dados.
Configuração de API
Setup dos controladores e rotas da API.
Fluxo de Comunicação Assíncrona
O sistema segue o seguinte fluxo para garantir a comunicação eficiente entre microserviços:

OrderService cria um novo pedido.
OrderService envia uma mensagem para o RabbitMQ notificando sobre o pedido criado.
InventoryService escuta a fila no RabbitMQ e, ao receber a mensagem, realiza o processamento necessário, como atualizar o estoque.

Diagrama de Fluxo

![image](https://github.com/user-attachments/assets/c997c642-235c-4055-9d64-0cbdf604331c)


Esse fluxo garante que o OrderService notifique o InventoryService de forma assíncrona, permitindo que os serviços funcionem de maneira independente e escalável.


1. Preparando o Ambiente para .NET 9
a. Instalar o SDK do .NET 9
Para rodar a aplicação, você precisa ter o SDK do .NET 9 instalado. Se você ainda não tem o SDK, siga as instruções abaixo:

No Windows:

Baixe o instalador do SDK .NET 9 no site oficial.
Execute o instalador e siga as instruções.
No Linux (Ubuntu):

Abra o terminal e execute os seguintes comandos para instalar o .NET 9:

wget https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-9.0-linux-x64-binaries
sudo apt-get install -y dotnet-sdk-9.0


b. Verificar a Instalação
dotnet --version

c. Subir o Container com Docker Compose
docker-compose up --build

d. para executar local
dotnet run --project seller-orderservice-api.WebAPI
