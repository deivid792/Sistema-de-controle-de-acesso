# 🛂 Visitor Service API - Backend

> Uma aplicação Backend API moderna e escalável para gerenciamento de acesso físico, permitindo que visitantes solicitem entrada e gestores aprovem ou rejeitem solicitações em tempo real.

<p align="center"> <img src="https://img.shields.io/badge/C%23-239120?style=flat-square&logo=c-sharp&logoColor=white">
  <img src="https://img.shields.io/badge/Status-Em desenvolvimento-yellow?style=flat-square"> </p>

---

## 🛠 Status do Projeto
Este projeto está em **desenvolvimento ativo**. Atualmente, estou focado na camada de Infraestrutura e Domínio.

- [x] Refatoração de Entidades (Design by Contracts)
- [ ] Identificação e desacoplamento de lógicas legadas (Em Andamento)
- [x] Migração para EntityTypeConfiguration (Fluent API)
- [x] Configuração de Relacionamentos Many-to-Many
- [x] Configuração de Relacionamentos One-to-Many
- [x] Implementação de Authentication/JWT
- [ ] Implementação de Cache
- [ ] Implementação de MCP Server
- [x] Unit Tests (xUnit)

---

## 🚀 Diferenciais de Engenharia

Diferente de CRUDS convencionais, este projeto aplica padrões de grandes sistemas corporativos:


Result & Notification Patterns: Substituição de exceções por fluxos de retorno semânticos, aumentando a performance e a previsibilidade da API.


Clean Architecture: Separação física rigorosa entre camadas, permitindo que a regra de negócio (Domínio) seja independente de frameworks e banco de dados.


CI/CD Pipeline: Integração contínua configurada via GitHub Actions, garantindo que cada commit passe por build e testes automatizados

---

## 📌 Descrição do Projeto

Este projeto é uma evolução autoral baseada no desafio técnico proposto durante a Residência On-Board (Porto Digital), onde colaborei com o Squad 10 em uma solução de controle de acesso. O presente repositório refina aquela arquitetura original, aplicando padrões avançados de .NET 9 e Clean Architecture.

### A aplicação possui duas principais áreas de acesso:

- 👤 Visitante — solicita visitas, acompanha status, acessa o portal.
- 🧑‍💼 Gestor — gerencia solicitações pendentes.
- 🔐 Autenticação — login com controle de sessão (em desenvolvimento) e proteção de rotas por role (já pronta).

O código segue padrões limpos, Design Pattern e separação física entre domínio, aplicação, infraestrutura interface e camada de testes.

---

## 📁 Estrutura da Solução

A estrutura prioriza clareza e escalabilidade:
```
application
 ├── Dtos              → Estrutura do JSON
 ├── Interfaces        → Desacopla a dependencia 
 └── UseCases          → Handllers
      ├── LoginHandler/
      ├── RegisterVisitorHandler/
      ├── CreateVisitHandler/
      └── UpdateVisitStatusHandler

Domain                → Entidades, Camada pricipal da aplicação
 ├── Entities
 ├── Services
 ├── Shared
 ├── Enums
 ├── Interfaces
 └── ValueObjects

Infrastructure            → Banco de dados/ Repositórios
 ├── Context              → Configuração e mapeamento EFCORE
 ├── Migrations           → Migrações do banco de dados
 └── Repositories         → Implementações concretas repositório

Interfaces/WebAPI         → Ponto de entrada da aplicação
 ├── dockerfile
 ├── Middleware
 └── Controllers          → Expõe as rotas da API

Tests                     → Camada de testes da aplicação
 └── Unit                 → Testes Unitários
```
---
## ⚙️ Tecnologias Utilizadas.

- NET 9.0 (C#).
- Entity Framework Core (SQL Server).
- JWT para autenticação segura.
- xUnit para testes automatizados.
- Docker para containerização e padronização de ambiente

---

## 🧩 Filosofia da Arquitetura

- Separação por camadas.
- Open closed principle.
- Implementações dependem de interfaces.
- Facilita a manutenção e os testes.
- Facilmente escalável.

---

## 🚀 Como Rodar o Projeto
### 1. Clonar o repositório
```
git clone https://github.com/deivid792/visitor-service-api

cd visitor-service-api
```
### 2. Restaure as dependências
```
dotnet restore
```
### 3. Execute as migrações (se necessário)
```
dotnet ef database update
```
### 4. Build do projeto
```
dotnet build
```

Caso use Frontend próprio, atualize a URL conforme necessário.

### 5. Rodar o servidor de desenvolvimento
```
dotnet run --project VisitorService.Interface
```


Acesse em:

📍 http://localhost:7163

---

## 🌐 Aplicação Deployada

### 🔗 Live Demo:

👉 [em breve]

---

## 🔐 Autenticação & Proteção de Rotas
### A aplicação utiliza:

- Token JWT.
- Rotas protegidas de acordo com a Role do usuario.

---

## 🏗️ Camada de API
### Toda comunicação com o FrontEnd ocorre em:

```
VisitorService/Interface/Controller
 ├── AuthController.cs
 └── VisitsController.cs
```

Com responsabilidades bem definidas:

``` AuthController```  → login, autenticação, criação do usuario

```VisitsController``` → registro de visitas

---

## 🎯 Projeções Futuras

- [ ] Documentação de cada camada da API
- [ ] Aumentar a quantidade de testes Unitários
- [ ] Testes de integração
- [ ] Implementação de relatórios detalhados para auditoria das visitas
