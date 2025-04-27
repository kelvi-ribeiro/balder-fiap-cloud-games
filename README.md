
# Balder.FiapCloudGames

Este repositório contém o desenvolvimento da **FIAP Cloud Games (FCG)**, uma plataforma voltada à venda de jogos digitais e gestão de servidores para partidas online. O projeto foi criado como parte do **Tech Challenge** da FIAP, integrando os conhecimentos adquiridos nas disciplinas da fase.

## 📚 Sobre o Projeto

O objetivo desta primeira fase é o desenvolvimento de um **MVP** de uma API REST em `.NET 8`, com foco no **cadastro de usuários**, **autenticação/autorizações via JWT** e **gerenciamento da biblioteca de jogos adquiridos**.

A arquitetura segue os princípios da **Clean Architecture** em um **monolito modularizado**, para facilitar o desenvolvimento ágil e a futura expansão com funcionalidades como matchmaking e gerenciamento de servidores.

---

## 🏗️ Estrutura da Solução

```bash
Balder.FiapCloudGames.sln
└── src
    ├── Balder.FiapCloudGames.Api           # Projeto Web API (.NET 8)
    ├── Balder.FiapCloudGames.Domain        # Regras de domínio e entidades
    ├── Balder.FiapCloudGames.Application   # Casos de uso, interfaces e DTOs
    ├── Balder.FiapCloudGames.Infrastructure# Persistência, repositórios e serviços externos
    └── Balder.FiapCloudGames.Tests         # Testes automatizados com xUnit
```

---

## 🚀 Funcionalidades

### Cadastro de Usuários
- Registro com nome, e-mail e senha.
- Validação de e-mail.
- Validação de senha forte (mín. 8 caracteres com letras, números e especiais).

### Autenticação e Autorização
- Autenticação com **JWT**.
- Dois níveis de acesso:
  - `Usuário`: acesso à plataforma e à biblioteca de jogos.
  - `Administrador`: gerenciamento de usuários, jogos e promoções.

---

## 🧱 Arquitetura

A solução adota uma **estrutura em camadas** separando responsabilidades:

- **API**: Camada de entrada (controllers e middlewares).
- **Application**: Casos de uso, serviços de aplicação e DTOs.
- **Domain**: Regras de negócio e entidades.
- **Infrastructure**: Integração com bancos de dados, autenticação, serviços externos.
- **Tests**: Testes de unidade e integração.

---

## 🛠️ Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Passos para criar e configurar o projeto

```bash
# 1. Entre na pasta API 
cd src/Balder.FiapCloudGames.Api

# 2. Execute a API
dotnet run
```

---

## 📦 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- ASP.NET Core Web API
- xUnit
- JWT Authentication
- Clean Architecture

---

## 🎯 Objetivo do Tech Challenge

> O Tech Challenge é uma atividade avaliativa integradora da fase atual do curso, valendo **90% da nota**. O projeto é dividido em quatro fases, e esta é a primeira delas, com foco na arquitetura inicial e funcionalidades básicas da plataforma FIAP Cloud Games.

---

## 🧪 Testes

Execute os testes unitários com:

```bash
# 1. Entre na pasta de teste
cd src/Balder.FiapCloudGames.Tests

# 2. Execute o teste
dotnet test
```

---

## 📌 Observações

- O projeto está em fase inicial de MVP com arquitetura monolítica.
- As próximas fases incluirão funcionalidades como matchmaking, gerenciamento de servidores, promoções e melhorias na escalabilidade.

---

## 👥 Time - Grupo 62

Desenvolvido por estudantes da FIAP como parte do desafio Tech Challenge.  
- Kelvi Ribeiro
- Rodrigo Varga
- Natanel Felix
- Arthur Zimmermann de Oliveira


## Comando Docker para criar o banco 

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=FiapGames!" -p 1433:1433 --name sqlFiapGames --hostname sqlFiapGames -d mcr.microsoft.com/mssql/server:2022-latest
```

---

## Realizar o migrations 

```bash
# 1. Entre na pasta de infraestrutura
cd src/Balder.FiapCloudGames.Infrastructure

# 2. Inicie a migração
dotnet ef migrations add InitialCreate

# 3. Aplique a migração
dotnet ef database update
```
