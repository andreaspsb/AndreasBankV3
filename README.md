# AndreasBankV3 🏦

[![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)]()
[![Tipo](https://img.shields.io/badge/tipo-projeto%20acad%C3%AAmico-blue)]()
[![Documentação](https://img.shields.io/badge/docs-completa-green)]()

## 📋 Sobre o Projeto

O **AndreasBankV3** é um projeto acadêmico que modela um sistema bancário digital fictício voltado para pessoas físicas. Este repositório contém a documentação completa de requisitos, user stories e cenários de testes em formato BDD (Behavior-Driven Development).

## 🎯 Objetivos

- Demonstrar a aplicação de técnicas de Engenharia de Requisitos
- Praticar a escrita de User Stories seguindo padrões ágeis
- Criar cenários de teste usando BDD/Gherkin
- Estabelecer rastreabilidade entre requisitos, histórias e testes
- Servir como base para implementação futura de um sistema bancário

## 🏗️ Escopo do Sistema

O AndreasBankV3 cobre as seguintes funcionalidades principais:

- **Gestão de Clientes**: Cadastro, atualização e consulta de dados de clientes pessoas físicas
- **Gestão de Contas**: Abertura, consulta e encerramento de contas correntes e poupança
- **Operações Financeiras**: Depósitos, saques, transferências (TED, DOC, entre contas)
- **Consultas**: Saldo, extratos e histórico de transações
- **Segurança**: Autenticação, autorização e auditoria de operações

## 📚 Estrutura da Documentação

```
AndreasBankV3/
├── README.md                    # Este arquivo
└── docs/
    ├── requisitos.md            # Requisitos funcionais, não-funcionais e regras de negócio
    ├── user-stories.md          # User stories organizadas por épicos
    ├── cenarios-bdd.md          # Cenários de teste em Gherkin (pt-BR)
    ├── glossario.md             # Glossário de termos e definições
    └── modelo-dados.md          # Modelo conceitual de dados
```

## 🗂️ Navegação Rápida

### Documentos Principais

- [📝 Requisitos do Sistema](docs/requisitos.md)
  - Requisitos Funcionais (RF01-RF30)
  - Requisitos Não-Funcionais (RNF01-RNF25)
  - Regras de Negócio (RN01-RN20)

- [👤 User Stories](docs/user-stories.md)
  - Épico 1: Cadastro de Clientes
  - Épico 2: Gestão de Contas
  - Épico 3: Operações Financeiras
  - Épico 4: Segurança

- [🧪 Cenários BDD](docs/cenarios-bdd.md)
  - Cenários de teste em formato Gherkin
  - Cobertura completa das funcionalidades
  - Casos de sucesso e exceções

- [📖 Glossário](docs/glossario.md)
  - Termos bancários
  - Personas do sistema
  - Abreviações e convenções

- [🗄️ Modelo de Dados](docs/modelo-dados.md)
  - Entidades e atributos
  - Relacionamentos
  - Diagrama ERD

## 🎓 Rastreabilidade

A documentação mantém rastreabilidade completa:

```
Requisitos (RF) → User Stories (US) → Cenários BDD
```

Cada user story referencia os requisitos que atende, e cada cenário BDD está vinculado a uma ou mais user stories.

## 👥 Personas

O sistema considera as seguintes personas principais:

- **Cliente**: Pessoa física que utiliza os serviços bancários
- **Gerente**: Responsável por aprovações e operações especiais
- **Administrador**: Responsável pela gestão do sistema

## 🚀 Próximos Passos

Este projeto está documentado e pronto para implementação. Possíveis próximos passos incluem:

1. Definição da arquitetura técnica
2. Escolha de stack tecnológica
3. Implementação de APIs e banco de dados
4. Desenvolvimento de interfaces (web/mobile)
5. Automação dos cenários BDD como testes

## 📄 Licença

Este é um projeto acadêmico para fins educacionais.

## ✨ Autor

**Andreas** - Projeto Acadêmico AndreasBankV3

---

**Data de Criação**: Fevereiro de 2026  
**Versão da Documentação**: 1.0
