# seller-orderservice-api
Revenda Service (Cadastra revendas).


Estrutura da Arquitetura:
Web/API (Controller)
Application (Serviço de Casos de Uso)
Domain (Entidades e Lógica de Negócios)
Service (Integrações com bancos de dados e outros sistemas)
RabbitMQ (Fila) (Integração com RabbitMQ)


+---------------------------+       +---------------------------+       +---------------------------+
|    Web/API (Controller)    | <---> |   Application (Serviços)   | <---> |     Domain (Core)         |
|---------------------------|       |---------------------------|       |---------------------------|
| Controller (API)           |       | PedidoService              |       | Pedido (Entidades)        |
| - Recebe requisições HTTP  |       | Valida regras de negócios  |       | Produtos (Entidades)      |
| - Valida entrada de dados  |       | Interage com RabbitMQ      |       | Validação de pedidos      |
| - Delegação para Services  |       | Orquestra lógica de negócio|       | Regras de negócios        |
+---------------------------+       +---------------------------+       +---------------------------+
                |                                |                                    |
                v                                v                                    v
+---------------------------+       +---------------------------+       +---------------------------+
|        Service (RabbitMQ)  | <---> |  Service (Integrações)     | <---> |   Persistence (Repository) |
|---------------------------|       |---------------------------|       |---------------------------|
| - Enviar/Consumir mensagens|       | - Acesso ao banco de dados |       | - Banco relacional (SQL)   |
| - Comunicação com RabbitMQ |       | - Integrações externas     |       | - Banco não relacional     |
| - Processamento assíncrono |       | - Integração com APIs externas|   | - Repositórios de dados    |
+---------------------------+       +---------------------------+       +---------------------------+
                |                                |
                v                                v
+---------------------------+
|        Infraestrutura      |
|---------------------------|
| - Configuração de RabbitMQ |
| - Configuração de Banco de Dados |
| - Configuração de API      |
+---------------------------+


Vamos seguir uma arquitetura onde:

1.OrderService cria um pedido.
2.OrderService envia uma mensagem para o RabbitMQ notificando sobre o pedido criado.
3.InventoryService escuta a fila no RabbitMQ para saber quando um novo pedido foi criado e, a partir daí, atualiza o estoque ou realiza algum outro processamento.

+------------------+              +---------------------+             +------------------+
|   OrderService   | --(Mensagem)->|      RabbitMQ       | <--(Mensagem)-| InventoryService |
|------------------|              |---------------------|             |------------------|
| - Recebe pedido  |              | - Fila de mensagens |             | - Consome pedido |
| - Valida pedido  |              | - Envia evento      |             | - Atualiza estoque|
| - Envia evento   |              | - Consumidor        |             | - Resposta/ação   |
|                  |              +---------------------+             +------------------+
+------------------+ 


Fluxo de Comunicação Assíncrona
1. OrderService recebe uma requisição HTTP para criar um novo pedido.
2. Após validar o pedido, o OrderService cria um pedido no banco de dados.
3. O OrderService envia uma mensagem para a fila do RabbitMQ, notificando que um novo pedido foi criado. Essa mensagem pode conter o ID do pedido, a lista de produtos e o total do pedido.
4. O InventoryService está escutando a fila do RabbitMQ e, assim que recebe uma mensagem, processa a informação e realiza ações no estoque, como diminuir a quantidade dos produtos solicitados.

