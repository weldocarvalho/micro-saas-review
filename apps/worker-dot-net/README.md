# ServiceWorker Service (.NET 9)

Background Worker do **ServiceWorker Micro-SaaS** - Serviço responsável por processamento pesado de análises de pele.

## 🏗️ Arquitetura

```
Worker/              # Entry point (Program.cs, Host setup)
├── Program.cs
├── ServiceWorker.csproj
├── appsettings.json
└── logs/

Application/         # Casos de uso, Consumers (Application Layer)
├── consumers/
│   └── SkinAnalysisConsumer.cs    # Consome eventos de análise de pele
├── models/
│   └── InitiateSkinAnalysisEvent.cs
└── ServiceWorker.Application.csproj

Domain/             # Interfaces de serviços, DTOs (Domain Layer)
├── Services/
│   ├── IImageProcessorService.cs      # Processar imagens
│   ├── ISkinAnalysisService.cs        # Análise de pele (IA)
│   └── IAnalysisRepository.cs         # Persistência
└── ServiceWorker.Domain.csproj

Infrastructure/              # Implementações Mock, Repositórios, Adapters (Infra Layer)
├── Services/
│   ├── MockImageProcessorService.cs
│   ├── MockSkinAnalysisService.cs
│   └── MockAnalysisRepository.cs
└── ServiceWorker.Infrastructure.csproj

docker-compose.yml  # RabbitMQ + PostgreSQL local
```

## 🚀 Quick Start

### 1. Pré-requisitos
- .NET 9 SDK
- Docker & Docker Compose

### 2. Iniciar dependências
```bash
docker-compose up -d
```

Isso inicia:
- **RabbitMQ**: `http://localhost:15672` (guest/guest)
- **PostgreSQL**: localhost:5432 (service_worker_user/service_worker_password)

### 3. Build & Run
```bash
dotnet build
dotnet run
```

Output esperado:
```
🚀 Vamos ver se está aqui... ServiceWorker iniciado - aguardando mensagens...
```

## 📦 Dependências Principais

- **MassTransit** (8.2.4) - Mensageria com RabbitMQ
- **Serilog** (4.1.0) - Logging estruturado
- **Microsoft.Extensions.Hosting** (9.0.0) - Worker Service

## 🔄 Fluxo de Processamento

1. BFF (Node.js) publica evento `InitiateSkinAnalysisEvent` no RabbitMQ
2. Worker consome via `SkinAnalysisConsumer`
3. Processa:
   - ✅ Redimensiona/normaliza imagem (MockImageProcessorService)
   - ✅ Executa análise de IA (MockSkinAnalysisService)
   - ✅ Persiste resultado (MockAnalysisRepository)
4. Retorna resultado ao BFF (próximos marcos)

## 🧪 Mocks para Desenvolvimento

Por enquanto, todos os serviços são **Mocks em-memória**:
- `MockImageProcessorService` - Simula processamento de imagem
- `MockSkinAnalysisService` - Simula análise com IA
- `MockAnalysisRepository` - Armazena em-memória

**Próximos marcos** integrarão:
- OpenAI/Claude para análise real
- PostgreSQL para persistência
- AWS S3/Cloudflare R2 para storage de imagens

## 📝 Configuração

### appsettings.json (Produção)
```json
{
  "RabbitMQ": {
    "Host": "rabbitmq-prod",
    "Username": "user",
    "Password": "pass"
  },
  "Database": {
    "ConnectionString": "Server=postgres-prod;..."
  }
}
```

### appsettings.Development.json (Dev)
Automático - usa mocks + RabbitMQ local

## 📚 Padrões & Boas Práticas

- **Clean Architecture**: Domain → Application → Infra
- **Dependency Injection**: MassTransit + Microsoft.Extensions.DependencyInjection
- **Structured Logging**: Serilog com propriedades nomeadas
- **Records** para DTOs (C# 9+)
- **Async/Await** em toda stack

## 🛠️ Próximos Passos

1. ✅ **Milestone 1** (ATUAL): Build sem erros
2. **Milestone 2**: Integração com OpenAI API
3. **Milestone 3**: PostgreSQL real + EF Core
4. **Milestone 4**: AWS S3 para imagens
5. **Milestone 5**: Integração com BFF (Node.js)

## 📞 Contato

Desenvolvido como parte do **ServiceWorker Micro-SaaS** para aprender tecnologias de alta demanda.

Útil: cd /Users/weldocarvalho/Documents/projects/micro-saas-review/apps/worker-dot-net/Worker
dotnet ef migrations add InitialCreate --project ../Infrastructure --output-dir ../Infrastructure/Data/Migrations
dotnet ef database update --project ../Infrastructure
