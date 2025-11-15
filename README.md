# 📝 README - Georeferenciamento API

## 🚀 Visão Geral
API para kedu, desenvolvida em .NET com Entity Framework Core.

## ⚙️ Configuração do Ambiente

### Pré-requisitos
- [.NET 8.0]
- [Entity Framework Core CLI](https://docs.microsoft.com/ef/core/cli/dotnet)
- Banco de dados configurado (PostgreSql)


## 🏗️ Estrutura do Projeto
```
src/
├── Kedu.Api/      # Camada de API/Web
├── Kedu.Application/    # Camada de Application
├── Kedu.Infra/    # Camada de Infraestrutura
├── Kedu.Infra.EF/    # Camada de Infraestrutura (Manipula dados do banco)
├── Kedu.Domain/     # Camada de Domínio
```

## 🛠️ Comandos Úteis

### Executar Migrations
```bash
dotnet ef database update --project .\src\Kedu.Infra.EF\ --startup-project .\src\Kedu.Api\
```

## 🌐 Swagger/OpenAPI
Acesse a documentação da API em:
```
[https://localhost:7050](https://localhost:7050)/swagger
```

## Collection 
```
https://www.postman.com/orange-comet-64215/kedu/collection/1aku50q/kedu?action=share&creator=19254849
```

