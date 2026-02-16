# 🔍 Análise Completa da Documentação - Estrutura de Serviços

Após revisão detalhada dos documentos, aqui está a **arquitetura de microserviços recomendada** para o AndreasBankV2:

---

## 🏗️ **Serviços Identificados** (6 Microserviços)

### **1. Auth.Service** 🔐
**Bounded Context**: Autenticação e Autorização

**Entidades de Domínio**:
- `Usuario`
- `Sessao`
- `TokenRecuperacao`

**Responsabilidades**:
- RF27: Login
- RF28: Logout
- RF29: Recuperação de senha
- RF30: Alteração de senha
- RN16: Política de senhas
- RN17: Bloqueio de conta
- RN18: Gestão de tokens e sessões
- US15, US16, US17

**APIs**:
- `POST /api/auth/login`
- `POST /api/auth/logout`
- `POST /api/auth/recuperar-senha`
- `POST /api/auth/redefinir-senha`
- `GET /api/auth/validar-token`

---

### **2. Cliente.Service** 👤
**Bounded Context**: Gestão de Clientes Pessoa Física

**Entidades de Domínio**:
- `Cliente` (Aggregate Root)
- `Endereco`
- Value Objects: `Cpf`, `Email`, `Telefone`

**Responsabilidades**:
- RF01: Cadastrar cliente PF
- RF02: Validar CPF
- RF03: Atualizar dados cadastrais
- RF04: Consultar dados do cliente
- RF05: Inativar cadastro
- RN11: Idade mínima
- RN12: Documentação obrigatória
- US01, US02, US03

**APIs**:
- `POST /api/clientes`
- `GET /api/clientes/{cpf}`
- `PUT /api/clientes/{cpf}`
- `PATCH /api/clientes/{cpf}/inativar`
- `GET /api/clientes/{cpf}/validar`

---

### **3. Conta.Service** 🏦
**Bounded Context**: Gestão de Contas Bancárias

**Entidades de Domínio**:
- `Conta` (Aggregate Root)
- `ContaCorrente` : Conta
- `ContaPoupanca` : Conta
- Value Objects: `NumeroConta`, `Agencia`, `Saldo`

**Responsabilidades**:
- RF06: Abrir conta corrente
- RF07: Abrir conta poupança
- RF08: Consultar dados da conta
- RF09: Encerrar conta
- RF10: Listar contas do cliente
- RN13: Limite de contas por cliente
- RN14: Encerramento de conta
- RN15: Conta inativa
- US04, US05, US06, US07

**APIs**:
- `POST /api/contas/corrente`
- `POST /api/contas/poupanca`
- `GET /api/contas/{numeroConta}`
- `GET /api/contas/cliente/{cpf}`
- `DELETE /api/contas/{numeroConta}`

---

### **4. Transacao.Service** 💰
**Bounded Context**: Operações Financeiras e Transações

**Entidades de Domínio**:
- `Transacao` (Aggregate Root)
- `Deposito` : Transacao
- `Saque` : Transacao
- `Transferencia` : Transacao
- `TED` : Transacao
- `DOC` : Transacao
- `Comprovante`

**Responsabilidades**:
- RF11-13: Operações de depósito
- RF14-16: Operações de saque
- RF17-21: Transferências (internas, TED, DOC)
- RN01: Saldo mínimo
- RN02: Tarifas de transferência
- RN03: Horário de funcionamento
- RN04: Limite de transferência
- RN08: Valores mínimos e máximos
- RN09: Tempo de compensação
- RN10: Estorno de transações
- US08, US09, US12, US13, US14

**APIs**:
- `POST /api/transacoes/deposito`
- `POST /api/transacoes/saque`
- `POST /api/transacoes/transferencia`
- `POST /api/transacoes/ted`
- `POST /api/transacoes/doc`
- `GET /api/transacoes/{id}`
- `POST /api/transacoes/{id}/estornar`

---

### **5. Extrato.Service** 📊
**Bounded Context**: Consultas e Relatórios (CQRS - Query Side)

**Modelos de Leitura**:
- `ExtratoDTO`
- `SaldoDTO`
- `TransacaoResumo`

**Responsabilidades**:
- RF22: Consultar saldo
- RF23: Gerar extrato por período
- RF24: Gerar extrato completo
- RF25: Filtrar transações por tipo
- RF26: Exportar extrato (PDF)
- US10, US11

**APIs**:
- `GET /api/extrato/saldo/{numeroConta}`
- `GET /api/extrato/{numeroConta}?dataInicio&dataFim`
- `GET /api/extrato/{numeroConta}/completo`
- `GET /api/extrato/{numeroConta}/tipo/{tipo}`
- `GET /api/extrato/{numeroConta}/pdf`

---

### **6. Auditoria.Service** 📝
**Bounded Context**: Logs e Auditoria (Cross-Cutting)

**Entidades de Domínio**:
- `AuditoriaLog`
- `LogTransacao`
- `LogAcesso`

**Responsabilidades**:
- RNF21: Log de transações
- RNF22: Log de acessos
- RNF23: Rastreabilidade de operações
- RNF24: Retenção de logs
- RNF25: Monitoramento em tempo real
- RN19: Detecção de fraude
- RN23: Rastreabilidade

**APIs**:
- `POST /api/auditoria/log`
- `GET /api/auditoria/logs/usuario/{id}`
- `GET /api/auditoria/logs/conta/{id}`
- `GET /api/auditoria/logs/transacao/{id}`
- `GET /api/auditoria/analise-fraude/{cpf}`

---

## 📋 **Resumo Executivo**

| # | Serviço | Aggregate Roots | RFs | RNs | USs | Status |
|---|---------|-----------------|-----|-----|-----|--------|
| 1 | **Auth.Service** | Usuario | 4 | 3 | 3 | 🟡 Iniciado |
| 2 | **Cliente.Service** | Cliente | 5 | 2 | 3 | ⚪ Pendente |
| 3 | **Conta.Service** | Conta | 5 | 3 | 4 | ⚪ Pendente |
| 4 | **Transacao.Service** | Transacao | 11 | 7 | 5 | ⚪ Pendente |
| 5 | **Extrato.Service** | - (Read Model) | 5 | 0 | 2 | ⚪ Pendente |
| 6 | **Auditoria.Service** | AuditoriaLog | 0 (RNF) | 2 | 0 | ⚪ Pendente |

---

## 🔄 **Comunicação Entre Serviços**

```
┌─────────────────┐
│   API Gateway   │
└────────┬────────┘
         │
    ┌────┴────┬────────┬─────────┬──────────┬──────────┐
    ▼         ▼        ▼         ▼          ▼          ▼
┌──────┐ ┌─────────┐ ┌──────┐ ┌──────────┐ ┌────────┐ ┌──────────┐
│ Auth │ │Cliente  │ │Conta │ │Transacao │ │Extrato │ │Auditoria │
└───┬──┘ └────┬────┘ └───┬──┘ └─────┬────┘ └───┬────┘ └─────┬────┘
    │         │           │          │          │            │
    └─────────┴───────────┴──────────┴──────────┴────────────┘
              Event Bus (RabbitMQ/Kafka/Azure Service Bus)
```

**Eventos de Domínio** (Event-Driven Architecture):
- `ClienteCadastrado`
- `ContaAberta`
- `TransacaoRealizada`
- `SaldoAtualizado`
- `LoginRealizado`

---

## 🎯 **Ordem de Implementação Recomendada**

### **Fase 1: Fundação** (2-3 semanas)
1. **Auth.Service** ✅ (já iniciado)
2. **Cliente.Service** → começar aqui!
3. **Auditoria.Service** (paralelamente)

### **Fase 2: Core Bancário** (3-4 semanas)
4. **Conta.Service**
5. **Transacao.Service**

### **Fase 3: Consultas e Otimização** (1-2 semanas)
6. **Extrato.Service** (CQRS)



**Estrutura final dos repositórios**:
```
AndreasBankV2/
├── Auth.sln             → Auth.Service
├── Cliente.sln          → Cliente.Service
├── Conta.sln            → Conta.Service
├── Transacao.sln        → Transacao.Service
├── Extrato.sln          → Extrato.Service (separado ou integrado)
└── Auditoria.sln        → Auditoria.Service
```

