# Projeto Vendas Taking

Este projeto é uma avaliação para candidatos à vaga de desenvolvedor sênior. Ele implementa um sistema de CRUD para vendas, com regras de negócio específicas, utilização de design patterns (como o Mediator), EF Core para ORM, testes unitários e de integração, além de um frontend Angular utilizando Angular Material.

## Sumário

- [Visão Geral](#visão-geral)
- [Funcionalidades](#funcionalidades)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Configuração e Execução](#configuração-e-execução)
  - [Backend (.NET 8)](#backend-net-8)
  - [Frontend (Angular)](#frontend-angular)
- [Testes](#testes)
- [Regras de Negócio](#regras-de-negócio)
- [Publicação de Eventos](#publicaçãode-eventos)
- [Licença](#licença)

## Visão Geral

O sistema de vendas permite:

- **Criar, atualizar, consultar e cancelar vendas.**
- Gerenciar itens de venda com regras de negócio:
  - Compras abaixo de 4 itens não têm desconto.
  - Compras com 4 a 9 itens têm desconto de 10%.
  - Compras com 10 a 20 itens têm desconto de 20%.
  - Não é permitido vender mais de 20 itens iguais.
- **Paginação, filtragem e ordenação** da listagem de vendas.
- Um frontend responsivo e intuitivo desenvolvido com Angular Material.

## Funcionalidades

- **CRUD de Vendas:**  
  - Criação com adição dinâmica de itens.
  - Atualização de dados da venda e dos itens.
  - Consulta de vendas (por ID e listagem com paginação).
  - Cancelamento de vendas.
- **Regras de Negócio:**  
  - Validação de quantidade de itens.
  - Cálculo de descontos e totais.
- **Publicação de Eventos:**  
  - Operações que alteram o estado da venda (criação, atualização e cancelamento) publicam eventos via Rebus, possibilitando integração com outros sistemas ou auditoria.
- **Testes:**  
  - Testes unitários com xUnit e NSubstitute.
  - Testes de integração utilizando WebApplicationFactory.
- **Configuração de Banco de Dados:**  
  - Uso de PostgreSQL para produção e InMemory para testes.
- **Autenticação JWT:**  
  - Configuração preparada para autenticação (não obrigatório para este teste).

## Tecnologias Utilizadas

- **Backend:**
  - .NET 8.0
  - Entity Framework Core (ORM)
  - PostgreSQL (produção) / InMemory (para testes)
  - MediatR (padrão Mediator)
  - AutoMapper
  - Rebus (publicação e consumo de eventos)
  - xUnit, NSubstitute (testes)
  - Serilog (logging)

- **Frontend:**
  - Angular 14+ (componentes standalone)
  - Angular Material
  - HTML, SCSS

## Estrutura do Projeto

O projeto está organizado em camadas, seguindo os princípios do Domain-Driven Design (DDD):

- **Domain:** Entidades e regras de negócio (ex.: `Sale`, `SaleItem`, `User`).
- **Application:** Comandos, queries, handlers, validações, mapeamentos e eventos.
- **ORM/Infrastructure:** Implementação do Entity Framework Core, repositórios e UnitOfWork.
- **WebApi:** Controllers, configuração de endpoints REST, CORS e integração com a camada Application.
- **Frontend:** Projeto Angular com componentes standalone para o CRUD de vendas.

## Configuração e Execução

### Backend (.NET 8)

1. **Pré-requisitos:**
   - .NET 8 SDK
   - PostgreSQL (se for utilizar o banco real) ou InMemory para testes.
   - Visual Studio 2022 ou VS Code.

2. **Configuração:**
   - Abra o arquivo `appsettings.json` e configure a string de conexão e a chave JWT:
     ```json
     {
       "UseInMemoryDatabase": true,
       "ConnectionStrings": {
         "DefaultConnection": "Host=localhost;Port=5432;Database=DeveloperEvaluation;Username=postgres;Password=SuaSenha;Trust Server Certificate=true"
       },
       "Jwt": {
         "SecretKey": "gKm4P/eA9qR2Tt8/5NbfM2k1uR7vXx+Z"
       },
       "Logging": {
         "LogLevel": {
           "Default": "Information",
           "Microsoft": "Warning",
           "Microsoft.Hosting.Lifetime": "Information"
         }
       },
       "AllowedHosts": "*"
     }
     ```
     > **Dica:** Altere `"UseInMemoryDatabase": true` para `false` se desejar usar PostgreSQL e substitua `"SuaSenha"` pela senha correta.

3. **Execução:**
   - Compile e execute o projeto:
     ```bash
     dotnet run
     ```
   - A API ficará disponível, por exemplo, em `https://localhost:7181`.

### Frontend (Angular)

1. **Pré-requisitos:**
   - Node.js (versão LTS recomendada)
   - Angular CLI

2. **Instalação:**
   - Navegue até o diretório `template/frontend`:
     ```bash
     cd template/frontend
     npm install
     ```

3. **Execução:**
   - Inicie o servidor de desenvolvimento:
     ```bash
     ng serve
     ```
   - A aplicação estará disponível em `http://localhost:4200`.

## Testes

### Testes Unitários

- **Backend:**
  - Utilize xUnit e NSubstitute para rodar os testes.
  - Execute:
    ```bash
    dotnet test
    ```
  - Os testes cobrem handlers, regras de negócio e repositórios (usando InMemory).

### Testes de Integração

- Utilize o WebApplicationFactory para testar os endpoints REST completos, verificando códigos de status e contratos da API.

### Testes de Contrato

- Implemente testes end-to-end para validar o formato das respostas e os contratos da API, se necessário.

## Regras de Negócio

- **Quantidade de Itens:**  
  Não é permitido vender mais de 20 itens iguais.
  
- **Descontos:**  
  - Quantidade < 4: 0% de desconto.  
  - Quantidade entre 4 e 9: 10% de desconto.  
  - Quantidade entre 10 e 20: 20% de desconto.

Essas regras são aplicadas no backend e refletidas no frontend.

## Publicação de Eventos

Este projeto utiliza o **Rebus** para publicar e consumir eventos relacionados às operações que alteram o estado da venda.  
**Observação:** Operações de GET não publicam eventos, pois não alteram o estado.

### Configuração do Rebus

No `Program.cs`, o Rebus é configurado para usar um transporte InMemory (ideal para desenvolvimento e testes):

```csharp
using Rebus.Config;
using Rebus.ServiceProvider;
using Rebus.Transport.InMem;

// ...

builder.Services.AddRebus(configure => configure
    .Logging(l => l.Console())
    .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "myQueue"))
);

// Registra automaticamente os handlers do Rebus do assembly
builder.Services.AutoRegisterHandlersFromAssemblyOf<SaleCreatedEventHandler>();

// Após construir o app:
app.Services.UseRebus();

### Licença

Este projeto está licenciado sob os termos da [MIT License](LICENSE).
