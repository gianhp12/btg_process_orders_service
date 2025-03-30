# btg\_process\_orders\_service

> Microserviço em .NET 8 Worker para consumir mensagens do RabbitMQ e armazenar pedidos no MongoDB.

## 📖 Visão Geral

Este projeto é um microserviço desenvolvido em **.NET 8 Worker Service** para consumir mensagens de pedidos de uma fila RabbitMQ e armazenar os dados no MongoDB.

## 🚀 Tecnologias Utilizadas

- **.NET 8 Worker Service**
- **RabbitMQ**
- **MongoDB** 
- **Docker**

## 📋 Pré-requisitos

Antes de iniciar, certifique-se de ter:

- **.NET 8 SDK** instalado
- **Docker e Docker Compose** instalados
- **RabbitMQ e MongoDB** no container Docker

## 📦 Instalação e Execução

1. **Clone o repositório:**

   ```sh
   git clone https://github.com/gianhp12/btg_process_orders_service.git
   cd btg_process_orders_service
   ```

2. **Configure as variáveis de ambiente:** Crie um arquivo `.env` na raiz do projeto e defina as credenciais necessárias:

   ```env
   RABBITMQ_HOST=localhost
   RABBITMQ_QUEUE=btg-pactual-order-created
   MONGODB_CONNECTION_STRING=mongodb://localhost:27017
   MONGODB_DATABASE=btg_pactual_orders
   ```

3. **Suba os serviços do RabbitMQ e MongoDB usando Docker:**

   ```sh
   docker-compose up -d
   ```

4. **Execute o Worker:**

   ```sh
   dotnet run
   ```

## ▶️ Como Funciona

1. O Worker escuta mensagens na fila **btg-pactual-order-created** do RabbitMQ.
2. Ao receber um pedido, ele converte a mensagem para um objeto **Order**.
3. A ordem é validada e armazenada no **MongoDB**.

### 📄 Exemplo de Mensagem (JSON)

```json
{
    "codigoPedido": 1001,
    "codigoCliente": 1,
    "itens": [
        {
            "produto": "lápis",
            "quantidade": 100,
            "preco": 1.10
        }
    ]
}
```

## 📂 Estrutura do Projeto

```
 btg_process_orders_service/
 │── Infra/
 │   ├── Extensions/
 │   ├── Di/
 │   ├── NoSql/
 │   ├── Queue/
 │── Domain/
 │   ├── Entity/
 │   ├── VO/
 │── Application/
 │   ├── Controllers/
 │   ├── Services/
 │── Program.cs
 │── docker-compose.yml
 │── README.md
```


