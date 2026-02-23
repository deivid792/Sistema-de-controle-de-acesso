# 🛂 Sistema de Controle de Visitantes - Backend

> Uma aplicação fullstack moderna e escalável para gerenciamento de acesso físico, permitindo que visitantes solicitem entrada e gestores aprovem ou rejeitem solicitações em tempo real.

<p align="center"> <img src="https://img.shields.io/badge/C%23-239120?style=flat-square&logo=c-sharp&logoColor=white">
  <img src="https://img.shields.io/badge/Status-Em desenvolvimento-yellow?style=flat-square"> </p>

---

## 🛠 Status do Projeto
Este projeto está em **desenvolvimento ativo**. Atualmente, estou focado na camada de Infraestrutura e Domínio.

- [x] Refatoração de Entidades (Design by Contracts)
- [x] Migração para EntityTypeConfiguration (Fluent API)
- [x] Configuração de Relacionamentos Many-to-Many
- [x] Configuração de Relacionamentos One-to-Many
- [ ] Implementação de Authentication/JWT
- [ ] Implementação de Cache
- [x] Unit Tests (xUnit)

---

## 📌 Descrição do Projeto

Esta aplicação foi desenvolvida pelo Squad 10 como proposta da Residência On-Board do Porto Digital em parceria com o Arco Mix.
Este repositório contém o backend do Sistema de Controle de Visitantes, desenvolvido utilizando C# e Dotnet 9.0 seguindo princípios de DDD e Clean Arquitecture e Design Patterns.

### A aplicação possui duas principais áreas de acesso:

- 👤 Visitante — solicita visitas, acompanha status, acessa o portal.
- 🧑‍💼 Gestor — gerencia solicitações pendentes.
- 🔐 Autenticação — login com controle de sessão (em desenvolvimento) e proteção de rotas por role (já pronta).

O código segue padrões limpos, Design Pattern e separação física entre domínio, aplicação, infraestrutura interface e camada de testes.

---

## 📁 Estrutura de Pastas

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

Interfaces                → Comunicação da API
 ├── dockerfile
 ├── Middleware
 └── Controllers          → Expõe as rotas da API

Tests                     → Camada de testes da aplicação
 └── Unit                 → Testes Unitários
```
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
git clone https://github.com/seu-repo.git
cd seu-repo
```
### 2. Instalar dependências
```
dotnet restore
```
### 3. Build do projeto
```
dotnet build
```

Caso use Frontend próprio, atualize a URL conforme necessário.

### 4. Rodar o servidor de desenvolvimento
```
dotnet run
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
