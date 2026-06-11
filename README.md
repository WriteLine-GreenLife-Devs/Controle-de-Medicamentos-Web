# 💊 Sistema de Controle de Medicamentos e Estoque Hospitalar

<p align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge\&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?style=for-the-badge\&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge\&logo=csharp)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge\&logo=bootstrap)
![JSON](https://img.shields.io/badge/JSON-Persistence-black?style=for-the-badge\&logo=json)
![Status](https://img.shields.io/badge/Status-Concluído-success?style=for-the-badge)

</p>

<p align="center">
Sistema web desenvolvido em ASP.NET Core MVC para gerenciamento de medicamentos, estoque hospitalar, pacientes, fornecedores e funcionários.
</p>

---

# 📖 Sobre o Projeto

O **Sistema de Controle de Medicamentos e Estoque Hospitalar** é uma aplicação web desenvolvida utilizando **ASP.NET Core MVC**, com o objetivo de auxiliar no gerenciamento de medicamentos e no controle de movimentações de estoque em ambientes hospitalares, clínicas e farmácias.

A aplicação permite o cadastro e gerenciamento de:

* 💊 Medicamentos
* 📦 Estoque
* 🏥 Pacientes
* 🚚 Fornecedores
* 👨‍💼 Funcionários

O projeto foi desenvolvido seguindo boas práticas de arquitetura de software, utilizando separação em camadas, injeção de dependência, persistência em arquivos JSON e padrões de projeto amplamente utilizados no mercado.

---

# 🎯 Objetivos

* Centralizar o controle de medicamentos.
* Gerenciar entradas e saídas do estoque.
* Controlar pacientes vinculados às retiradas de medicamentos.
* Gerenciar fornecedores responsáveis pelo abastecimento.
* Administrar funcionários responsáveis pelas movimentações.
* Aplicar conceitos modernos de desenvolvimento web utilizando .NET.

---

# ✨ Funcionalidades

## 💊 Módulo de Medicamentos

* Cadastro de medicamentos
* Edição de medicamentos
* Exclusão de medicamentos
* Consulta de medicamentos
* Associação com fornecedores
* Controle de informações do produto

---

## 📦 Módulo de Estoque

* Registro de entrada de medicamentos
* Registro de saída de medicamentos
* Controle de quantidade disponível
* Histórico de movimentações
* Associação entre medicamentos, funcionários e pacientes

---

## 🏥 Módulo de Pacientes

* Cadastro de pacientes
* Alteração de informações
* Exclusão de registros
* Consulta de pacientes
* Controle de Cartão SUS

---

## 🚚 Módulo de Fornecedores

* Cadastro de fornecedores
* Consulta de fornecedores
* Atualização de informações
* Exclusão de registros
* Controle de CNPJ
* Controle de telefone

---

## 👨‍💼 Módulo de Funcionários

* Cadastro de funcionários
* Consulta de funcionários
* Atualização de dados
* Exclusão de registros
* Controle de CPF

---

# 🏗️ Arquitetura do Sistema

O sistema foi estruturado utilizando uma arquitetura em camadas para garantir organização, manutenção e escalabilidade.

```text
┌──────────────────────────┐
│      Apresentação        │
│ Controllers + Views MVC │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│        Aplicação         │
│ Serviços e Casos de Uso │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│         Domínio          │
│ Entidades e Contratos   │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│      Infraestrutura      │
│ Repositórios JSON       │
└──────────────────────────┘
```

---

# 🔄 Fluxo da Aplicação

```text
Usuário
   │
   ▼
View (Razor)
   │
   ▼
Controller
   │
   ▼
Service
   │
   ▼
Repository
   │
   ▼
Contexto JSON
   │
   ▼
dados.json
```

---

# 📂 Estrutura do Projeto

```text
ControleDeMedicamentosWeb.WebApp
│
├── Compartilhado
│   ├── Aplicacao
│   ├── Dominio
│   ├── Infra
│   └── Apresentacao
│
├── ModuloFornecedor
│
├── ModuloFuncionario
│
├── ModuloPaciente
│
├── ModuloMedicamento
│
├── ModuloEstoque
│
├── Views
│
├── wwwroot
│   ├── css
│   ├── js
│   └── imagens
│
└── Program.cs
```

---

# 🛠️ Tecnologias Utilizadas

## Backend

* ASP.NET Core MVC
* .NET 8
* C#
* AutoMapper
* FluentResults

## Frontend

* Razor Views
* Bootstrap 5
* HTML5
* CSS3
* JavaScript

## Persistência

* JSON
* System.Text.Json

## Arquitetura

* MVC
* Repository Pattern
* Dependency Injection
* DTO Pattern
* Camadas (Layered Architecture)

---

# 🔒 Regras de Negócio

O sistema implementa diversas validações para garantir a integridade dos dados:

### Funcionários

* CPF obrigatório
* CPF único
* Nome obrigatório

### Pacientes

* Cartão SUS obrigatório
* Cartão SUS único

### Fornecedores

* CNPJ obrigatório
* CNPJ único
* Telefone único

### Medicamentos

* Nome obrigatório
* Fornecedor obrigatório

### Estoque

* Não permite saída superior ao estoque disponível
* Entrada apenas de medicamentos cadastrados
* Saída vinculada a paciente e funcionário

---

# 💾 Persistência dos Dados

A aplicação utiliza persistência local por meio de arquivos JSON.

```text
AppData/
└── ControleMedicamentos/
    └── dados.json
```

Essa abordagem elimina a necessidade de um banco de dados relacional, tornando o projeto mais simples para fins acadêmicos e de aprendizado.

---

# 🚀 Como Executar o Projeto

## Pré-requisitos

* Visual Studio 2022
* .NET SDK 8.0

---

## Clonar o Repositório

```bash
git clone https://github.com/seu-usuario/controle-medicamentos-web.git
```

---

## Entrar na Pasta do Projeto

```bash
cd ControleDeMedicamentosWeb.WebApp
```

---

## Restaurar Dependências

```bash
dotnet restore
```

---

## Executar

```bash
dotnet run
```

ou pressione:

```text
F5
```

no Visual Studio.

---

# 📸 Telas do Sistema

Adicione aqui capturas de tela do sistema.

### Tela Inicial

<p align="center">
  <img src=".docs\images\home.PNG">
</p>


### Cadastro de Medicamentos

<p align="center">
  <img src=".docs\images\medicamentos.PNG">
</p>

### Controle de Estoque

<p align="center">
  <img src=".docs\images\estoque.jpeg">
</p>

### Cadastro de Pacientes

<p align="center">
  <img src=".docs\images\pacientes.PNG">
</p>

---

# 📚 Conceitos Aplicados

Durante o desenvolvimento deste projeto foram aplicados conceitos importantes de engenharia de software:

* Programação Orientada a Objetos
* SOLID
* MVC
* Repository Pattern
* Dependency Injection
* AutoMapper
* DTOs
* Persistência de Dados
* Validação de Regras de Negócio
* Arquitetura em Camadas

---

# 🔮 Melhorias Futuras

* Integração com banco de dados SQL Server
* Controle de usuários e autenticação
* Controle de permissões
* Relatórios PDF
* Dashboard com gráficos
* Controle de lotes e validade
* API REST
* Notificações automáticas de estoque baixo

---

# 👨‍💻 Autor


Projeto desenvolvido por Gustavo Tessaro e Alec Luí para fins acadêmicos e de aprendizado em desenvolvimento web com ASP.NET Core MVC.
