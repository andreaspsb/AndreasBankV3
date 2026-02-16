# Cenários BDD - AndreasBankV2

## Índice

- [1. Cadastro de Clientes](#1-cadastro-de-clientes)
- [2. Gestão de Contas](#2-gestão-de-contas)
- [3. Operações de Depósito](#3-operações-de-depósito)
- [4. Operações de Saque](#4-operações-de-saque)
- [5. Transferências](#5-transferências)
- [6. Consultas](#6-consultas)
- [7. Autenticação](#7-autenticação)

---

## Sobre este Documento

Este documento contém cenários de teste escritos em **Gherkin** (formato BDD - Behavior-Driven Development) em **português brasileiro**. Os cenários descrevem o comportamento esperado do sistema AndreasBankV2 e podem ser automatizados usando frameworks como Cucumber, Behave ou SpecFlow.

**Sintaxe Utilizada:**
- `Funcionalidade`: Descrição da funcionalidade sendo testada
- `Contexto`: Pré-condições comuns a todos os cenários
- `Cenário`: Caso de teste específico
- `Dado`: Estado inicial / pré-condições
- `Quando`: Ação executada
- `Então`: Resultado esperado
- `E`: Conjunção para adicionar mais passos
- `Esquema do Cenário`: Template para múltiplos cenários com diferentes dados
- `Exemplos`: Tabela de dados para esquema do cenário

---

## 1. Cadastro de Clientes

### Funcionalidade: Cadastro de Novo Cliente
**User Story:** US01  
**Requisitos:** RF01, RF02

#### Contexto:
```gherkin
Dado que o sistema AndreasBankV2 está disponível
E que a página de cadastro está acessível
```

#### Cenário: Cadastro bem-sucedido de novo cliente

```gherkin
Cenário: Cadastro bem-sucedido de novo cliente
  Dado que sou uma pessoa física sem cadastro no sistema
  Quando eu preencho o formulário de cadastro com os seguintes dados:
    | Campo              | Valor                    |
    | CPF                | 123.456.789-09          |
    | Nome Completo      | João da Silva Santos    |
    | Data de Nascimento | 15/03/1990              |
    | Email              | joao.silva@email.com    |
    | Telefone           | (11) 98765-4321         |
  E clico no botão "Cadastrar"
  Então o sistema deve validar os dados informados
  E deve criar um novo cadastro de cliente
  E deve exibir a mensagem "Cadastro realizado com sucesso!"
  E deve enviar um email de confirmação para "joao.silva@email.com"
  E devo ser redirecionado para a página de abertura de conta
```

#### Cenário: Tentativa de cadastro com CPF inválido

```gherkin
Cenário: Tentativa de cadastro com CPF inválido
  Dado que sou uma pessoa física sem cadastro no sistema
  Quando eu preencho o campo CPF com "123.456.789-00"
  E preencho os demais campos obrigatórios corretamente
  E clico no botão "Cadastrar"
  Então o sistema deve exibir a mensagem de erro "CPF inválido"
  E não deve criar o cadastro
```

#### Cenário: Tentativa de cadastro com CPF já existente

```gherkin
Cenário: Tentativa de cadastro com CPF já existente
  Dado que existe um cliente cadastrado com CPF "123.456.789-09"
  Quando eu tento me cadastrar com o CPF "123.456.789-09"
  E preencho os demais campos obrigatórios
  E clico no botão "Cadastrar"
  Então o sistema deve exibir a mensagem "CPF já cadastrado no sistema"
  E não deve criar um novo cadastro
```

#### Cenário: Tentativa de cadastro com cliente menor de 16 anos

```gherkin
Cenário: Tentativa de cadastro com cliente menor de 16 anos
  Dado que sou uma pessoa física sem cadastro no sistema
  Quando eu preencho a data de nascimento com "01/01/2015"
  E preencho os demais campos obrigatórios corretamente
  E clico no botão "Cadastrar"
  Então o sistema deve exibir a mensagem "Cliente deve ter no mínimo 16 anos"
  E não deve criar o cadastro
```

#### Esquema do Cenário: Validação de campos obrigatórios

```gherkin
Esquema do Cenário: Validação de campos obrigatórios
  Dado que estou na página de cadastro
  Quando eu deixo o campo "<campo>" vazio
  E preencho os demais campos obrigatórios
  E clico no botão "Cadastrar"
  Então o sistema deve exibir a mensagem "<mensagem_erro>"
  E não deve criar o cadastro

  Exemplos:
    | campo              | mensagem_erro                       |
    | CPF                | Campo CPF é obrigatório             |
    | Nome Completo      | Campo Nome Completo é obrigatório   |
    | Data de Nascimento | Campo Data de Nascimento é obrigatório |
    | Email              | Campo Email é obrigatório           |
    | Telefone           | Campo Telefone é obrigatório        |
```

---

### Funcionalidade: Atualização de Dados Cadastrais
**User Story:** US02  
**Requisitos:** RF03

#### Contexto:
```gherkin
Dado que sou um cliente cadastrado no sistema
E estou autenticado
E tenho os seguintes dados cadastrados:
  | Campo        | Valor                 |
  | CPF          | 123.456.789-09       |
  | Nome         | João da Silva Santos |
  | Email        | joao@email.com       |
  | Telefone     | (11) 98765-4321      |
```

#### Cenário: Atualização bem-sucedida de email

```gherkin
Cenário: Atualização bem-sucedida de email
  Quando acesso a página de atualização de dados
  E altero o email para "joao.novo@email.com"
  E clico no botão "Salvar Alterações"
  Então o sistema deve solicitar confirmação
  Quando confirmo a alteração
  Então o sistema deve atualizar o email
  E deve exibir a mensagem "Dados atualizados com sucesso!"
  E deve enviar notificação de alteração para o novo email
```

#### Cenário: Tentativa de alterar CPF (campo não editável)

```gherkin
Cenário: Tentativa de alterar CPF (campo não editável)
  Quando acesso a página de atualização de dados
  Então o campo CPF deve estar bloqueado para edição
  E deve exibir apenas os últimos 2 dígitos do CPF
```

#### Cenário: Atualização de múltiplos campos

```gherkin
Cenário: Atualização de múltiplos campos
  Quando acesso a página de atualização de dados
  E altero os seguintes campos:
    | Campo    | Novo Valor              |
    | Email    | novo.email@email.com    |
    | Telefone | (11) 99999-8888         |
    | Endereço | Rua Nova, 123           |
  E clico no botão "Salvar Alterações"
  E confirmo a alteração
  Então o sistema deve atualizar todos os campos alterados
  E deve exibir a mensagem "Dados atualizados com sucesso!"
```

---

## 2. Gestão de Contas

### Funcionalidade: Abertura de Conta Corrente
**User Story:** US04  
**Requisitos:** RF06

#### Contexto:
```gherkin
Dado que sou um cliente cadastrado no sistema
E tenho 18 anos ou mais
E estou autenticado
E não possuo conta corrente
```

#### Cenário: Abertura bem-sucedida de conta corrente

```gherkin
Cenário: Abertura bem-sucedida de conta corrente
  Quando acesso a opção "Abrir Conta Corrente"
  E confirmo que li e aceito os termos e condições
  E clico no botão "Abrir Conta"
  Então o sistema deve criar uma nova conta corrente
  E deve gerar automaticamente um número de conta
  E deve gerar automaticamente um número de agência
  E deve definir o status da conta como "Ativa"
  E deve exibir os dados da conta criada:
    | Tipo    | Conta Corrente |
    | Status  | Ativa          |
    | Saldo   | R$ 0,00        |
  E deve enviar email com os dados da nova conta
```

#### Cenário: Tentativa de abertura de segunda conta corrente

```gherkin
Cenário: Tentativa de abertura de segunda conta corrente
  Dado que já possuo uma conta corrente ativa
  Quando tento abrir uma nova conta corrente
  Então o sistema deve exibir a mensagem "Você já possui uma conta corrente"
  E não deve criar nova conta
```

#### Cenário: Tentativa de abertura por menor de 18 anos

```gherkin
Cenário: Tentativa de abertura por menor de 18 anos
  Dado que tenho 17 anos
  Quando tento abrir uma conta corrente
  Então o sistema deve exibir a mensagem "Idade mínima para conta corrente é 18 anos"
  E não deve criar a conta
```

---

### Funcionalidade: Abertura de Conta Poupança
**User Story:** US05  
**Requisitos:** RF07

#### Cenário: Abertura bem-sucedida de conta poupança

```gherkin
Cenário: Abertura bem-sucedida de conta poupança
  Dado que sou um cliente cadastrado no sistema
  E tenho 16 anos ou mais
  E estou autenticado
  E não possuo conta poupança
  Quando acesso a opção "Abrir Conta Poupança"
  E confirmo que li e aceito os termos e condições
  E clico no botão "Abrir Conta"
  Então o sistema deve criar uma nova conta poupança
  E deve gerar automaticamente um número de conta
  E deve gerar automaticamente um número de agência
  E deve definir o status da conta como "Ativa"
  E deve exibir informação do rendimento mensal: "0,5% ao mês + TR"
  E deve exibir os dados da conta criada
  E deve enviar email com os dados da nova conta
```

---

### Funcionalidade: Encerramento de Conta
**User Story:** US06  
**Requisitos:** RF09

#### Contexto:
```gherkin
Dado que sou um cliente autenticado
E possuo uma conta bancária
```

#### Cenário: Encerramento bem-sucedido de conta com saldo zerado

```gherkin
Cenário: Encerramento bem-sucedido de conta com saldo zerado
  Dado que minha conta possui saldo de R$ 0,00
  E não há transações pendentes
  Quando acesso a opção "Encerrar Conta"
  E informo o motivo: "Mudança para outro banco"
  E confirmo o encerramento
  Então o sistema deve alterar o status da conta para "Encerrada"
  E deve registrar a data de encerramento
  E deve registrar o motivo do encerramento
  E deve exibir a mensagem "Solicitação de encerramento registrada. Conta será encerrada em 30 dias."
  E deve enviar email confirmando o encerramento
```

#### Cenário: Tentativa de encerramento com saldo positivo

```gherkin
Cenário: Tentativa de encerramento com saldo positivo
  Dado que minha conta possui saldo de R$ 150,00
  Quando tento encerrar a conta
  Então o sistema deve exibir a mensagem "Não é possível encerrar conta com saldo. Por favor, zere o saldo antes de encerrar."
  E não deve encerrar a conta
```

#### Cenário: Tentativa de encerramento com transações pendentes

```gherkin
Cenário: Tentativa de encerramento com transações pendentes
  Dado que minha conta possui saldo de R$ 0,00
  Mas há um depósito em cheque pendente de compensação
  Quando tento encerrar a conta
  Então o sistema deve exibir a mensagem "Não é possível encerrar conta com transações pendentes"
  E não deve encerrar a conta
```

---

### Funcionalidade: Consulta de Dados da Conta
**User Story:** US07  
**Requisitos:** RF08, RF10

#### Cenário: Consulta de dados de uma única conta

```gherkin
Cenário: Consulta de dados de uma única conta
  Dado que sou um cliente autenticado
  E possuo uma conta corrente
  Quando acesso a opção "Minhas Contas"
  Então o sistema deve exibir os seguintes dados da conta:
    | Campo   | Exemplo        |
    | Tipo    | Conta Corrente |
    | Agência | 0001           |
    | Conta   | 12345-6        |
    | Status  | Ativa          |
    | Saldo   | R$ 1.500,00    |
```

#### Cenário: Consulta de múltiplas contas

```gherkin
Cenário: Consulta de múltiplas contas
  Dado que sou um cliente autenticado
  E possuo uma conta corrente e uma conta poupança
  Quando acesso a opção "Minhas Contas"
  Então o sistema deve listar ambas as contas
  E cada conta deve exibir: tipo, agência, número, status e saldo
  E devo poder selecionar uma conta para ver mais detalhes
```

---

## 3. Operações de Depósito

### Funcionalidade: Realizar Depósito
**User Story:** US08  
**Requisitos:** RF11, RF12, RF13

#### Contexto:
```gherkin
Dado que sou um cliente autenticado
E possuo uma conta ativa
```

#### Cenário: Depósito em dinheiro bem-sucedido

```gherkin
Cenário: Depósito em dinheiro bem-sucedido
  Dado que minha conta possui saldo de R$ 100,00
  Quando acesso a opção "Depositar"
  E seleciono "Depósito em Dinheiro"
  E informo o valor de R$ 50,00
  E confirmo a operação
  Então o sistema deve processar o depósito imediatamente
  E deve atualizar o saldo para R$ 150,00
  E deve gerar um comprovante com:
    | Campo            | Conteúdo              |
    | Tipo             | Depósito em Dinheiro  |
    | Valor            | R$ 50,00              |
    | Data/Hora        | <data e hora atual>   |
    | Número Autenticação | <número único>     |
  E deve exibir a mensagem "Depósito realizado com sucesso!"
```

#### Cenário: Depósito em cheque com compensação

```gherkin
Cenário: Depósito em cheque com compensação
  Dado que acesso a opção "Depositar"
  Quando seleciono "Depósito em Cheque"
  E informo os dados do cheque:
    | Número do Cheque | 123456        |
    | Banco            | 001 - Banco X |
    | Valor            | R$ 200,00     |
  E confirmo a operação
  Então o sistema deve registrar o depósito
  E deve informar prazo de compensação: "1 dia útil"
  E deve gerar comprovante do depósito
  E o valor não deve estar disponível imediatamente
  E deve exibir mensagem "Depósito registrado. Valor disponível após compensação."
```

#### Cenário: Tentativa de depósito com valor abaixo do mínimo

```gherkin
Cenário: Tentativa de depósito com valor abaixo do mínimo
  Quando acesso a opção "Depositar"
  E informo o valor de R$ 0,50
  E confirmo a operação
  Então o sistema deve exibir a mensagem "Valor mínimo para depósito é R$ 1,00"
  E não deve processar o depósito
```

#### Esquema do Cenário: Depósitos com diferentes valores

```gherkin
Esquema do Cenário: Depósitos com diferentes valores
  Dado que minha conta possui saldo de R$ <saldo_inicial>
  Quando realizo um depósito de R$ <valor_deposito>
  Então o saldo deve ser atualizado para R$ <saldo_final>

  Exemplos:
    | saldo_inicial | valor_deposito | saldo_final |
    | 0,00          | 100,00         | 100,00      |
    | 50,00         | 50,00          | 100,00      |
    | 100,00        | 1,00           | 101,00      |
    | 1000,00       | 500,50         | 1500,50     |
```

---

## 4. Operações de Saque

### Funcionalidade: Realizar Saque
**User Story:** US09  
**Requisitos:** RF14, RF15, RF16

#### Contexto:
```gherkin
Dado que sou um cliente autenticado
E possuo uma conta ativa
```

#### Cenário: Saque bem-sucedido

```gherkin
Cenário: Saque bem-sucedido
  Dado que minha conta possui saldo de R$ 500,00
  Quando acesso a opção "Sacar"
  E informo o valor de R$ 100,00
  E confirmo a operação
  Então o sistema deve validar o saldo disponível
  E deve debitar R$ 100,00 da conta
  E deve atualizar o saldo para R$ 400,00
  E deve gerar comprovante de saque
  E deve exibir a mensagem "Saque realizado com sucesso!"
```

#### Cenário: Tentativa de saque com saldo insuficiente

```gherkin
Cenário: Tentativa de saque com saldo insuficiente
  Dado que minha conta possui saldo de R$ 50,00
  Quando tento sacar R$ 100,00
  Então o sistema deve exibir a mensagem "Saldo insuficiente"
  E não deve processar o saque
  E o saldo deve permanecer R$ 50,00
```

#### Cenário: Tentativa de saque abaixo do valor mínimo

```gherkin
Cenário: Tentativa de saque abaixo do valor mínimo
  Dado que minha conta possui saldo de R$ 500,00
  Quando tento sacar R$ 5,00
  Então o sistema deve exibir a mensagem "Valor mínimo para saque é R$ 10,00"
  E não deve processar o saque
```

#### Cenário: Tentativa de saque acima do valor máximo

```gherkin
Cenário: Tentativa de saque acima do valor máximo
  Dado que minha conta possui saldo de R$ 5.000,00
  Quando tento sacar R$ 1.500,00
  Então o sistema deve exibir a mensagem "Valor máximo para saque é R$ 1.000,00 por operação"
  E não deve processar o saque
```

#### Cenário: Validação de limite diário de saque

```gherkin
Cenário: Validação de limite diário de saque
  Dado que minha conta possui saldo de R$ 5.000,00
  E já saquei R$ 800,00 hoje
  Quando tento sacar R$ 300,00
  Então o sistema deve exibir a mensagem "Limite diário de saque atingido. Limite: R$ 1.000,00. Disponível hoje: R$ 200,00"
  E não deve processar o saque
```

---

## 5. Transferências

### Funcionalidade: Transferência Entre Contas Internas
**User Story:** US12  
**Requisitos:** RF17, RF20, RF21

#### Contexto:
```gherkin
Dado que sou um cliente autenticado
E possuo uma conta corrente com saldo de R$ 1.000,00
```

#### Cenário: Transferência interna bem-sucedida

```gherkin
Cenário: Transferência interna bem-sucedida
  Quando acesso a opção "Transferir"
  E seleciono "Para conta do AndreasBankV2"
  E informo os dados da conta destino:
    | Agência | 0001      |
    | Conta   | 54321-9   |
  E informo o valor de R$ 200,00
  E confirmo a transferência
  Então o sistema deve validar a conta destino
  E deve validar o saldo disponível
  E deve debitar R$ 200,00 da minha conta
  E deve creditar R$ 200,00 na conta destino
  E meu saldo deve ser atualizado para R$ 800,00
  E a transferência deve ser instantânea
  E não deve cobrar taxa
  E deve gerar comprovante com dados completos
  E deve notificar ambos os envolvidos
```

#### Cenário: Tentativa de transferência para conta inexistente

```gherkin
Cenário: Tentativa de transferência para conta inexistente
  Quando acesso a opção "Transferir"
  E informo uma conta destino inexistente:
    | Agência | 9999      |
    | Conta   | 99999-9   |
  E informo o valor de R$ 100,00
  E confirmo a transferência
  Então o sistema deve exibir a mensagem "Conta destino não encontrada"
  E não deve processar a transferência
```

#### Cenário: Tentativa de transferência com saldo insuficiente

```gherkin
Cenário: Tentativa de transferência com saldo insuficiente
  Dado que minha conta possui saldo de R$ 100,00
  Quando tento transferir R$ 200,00
  Então o sistema deve exibir a mensagem "Saldo insuficiente para transferência"
  E não deve processar a transferência
```

#### Cenário: Tentativa de transferência acima do limite

```gherkin
Cenário: Tentativa de transferência acima do limite
  Dado que minha conta possui saldo de R$ 10.000,00
  Quando tento transferir R$ 6.000,00 para terceiros
  Então o sistema deve exibir a mensagem "Valor máximo para transferência a terceiros é R$ 5.000,00"
  E não deve processar a transferência
```

---

### Funcionalidade: Realizar TED
**User Story:** US13  
**Requisitos:** RF18, RF20, RF21

#### Contexto:
```gherkin
Dado que sou um cliente autenticado
E possuo uma conta corrente com saldo de R$ 1.000,00
E são 14:00 de um dia útil
```

#### Cenário: TED bem-sucedida em horário comercial

```gherkin
Cenário: TED bem-sucedida em horário comercial
  Quando acesso a opção "Transferir"
  E seleciono "TED - Outro Banco"
  E informo os dados do favorecido:
    | Banco       | 237 - Bradesco       |
    | Agência     | 1234                 |
    | Conta       | 98765-4              |
    | CPF         | 987.654.321-00       |
    | Nome        | Maria Santos         |
  E informo o valor de R$ 500,00
  E confirmo a transferência
  Então o sistema deve exibir a taxa: "R$ 8,50"
  E deve validar saldo disponível para R$ 508,50
  E deve debitar R$ 508,50 da minha conta (R$ 500,00 + R$ 8,50)
  E deve processar a TED para o mesmo dia
  E deve gerar comprovante detalhado
  E deve exibir mensagem "TED realizada com sucesso. Processamento no mesmo dia."
```

#### Cenário: TED após horário comercial

```gherkin
Cenário: TED após horário comercial
  Dado que são 18:00 de um dia útil
  Quando realizo uma TED de R$ 300,00
  Então o sistema deve informar "TED será processada no próximo dia útil"
  E deve debitar o valor + taxa da conta
  E deve registrar a TED como agendada
```

#### Cenário: TED com saldo insuficiente (considerando taxa)

```gherkin
Cenário: TED com saldo insuficiente (considerando taxa)
  Dado que minha conta possui saldo de R$ 100,00
  Quando tento fazer uma TED de R$ 95,00
  Então o sistema deve exibir a mensagem "Saldo insuficiente. Valor necessário: R$ 103,50 (R$ 95,00 + taxa de R$ 8,50)"
  E não deve processar a TED
```

---

### Funcionalidade: Realizar DOC
**User Story:** US14  
**Requisitos:** RF19, RF20, RF21

#### Cenário: DOC bem-sucedida

```gherkin
Cenário: DOC bem-sucedida
  Dado que sou um cliente autenticado
  E possuo uma conta com saldo de R$ 1.000,00
  Quando acesso a opção "Transferir"
  E seleciono "DOC - Outro Banco"
  E informo os dados do favorecido e valor de R$ 400,00
  E confirmo a transferência
  Então o sistema deve exibir a taxa: "R$ 5,00"
  E deve debitar R$ 405,00 da minha conta
  E deve informar "DOC será processada em D+1 (próximo dia útil)"
  E deve gerar comprovante
  E deve exibir mensagem "DOC registrada com sucesso"
```

---

## 6. Consultas

### Funcionalidade: Consultar Saldo
**User Story:** US10  
**Requisitos:** RF22

#### Cenário: Consulta de saldo em conta corrente

```gherkin
Cenário: Consulta de saldo em conta corrente
  Dado que sou um cliente autenticado
  E possuo uma conta corrente
  Quando acesso a opção "Consultar Saldo"
  E seleciono minha conta corrente
  Então o sistema deve exibir:
    | Campo               | Valor        |
    | Saldo Disponível    | R$ 1.500,00  |
    | Saldo Bloqueado     | R$ 0,00      |
    | Limite Especial     | R$ 500,00    |
    | Limite Disponível   | R$ 500,00    |
    | Última Atualização  | <data/hora>  |
```

#### Cenário: Consulta de saldo em conta poupança

```gherkin
Cenário: Consulta de saldo em conta poupança
  Dado que sou um cliente autenticado
  E possuo uma conta poupança
  Quando consulto o saldo da poupança
  Então o sistema deve exibir:
    | Campo               | Valor       |
    | Saldo Disponível    | R$ 2.000,00 |
    | Saldo Bloqueado     | R$ 0,00     |
    | Próximo Aniversário | 15/03/2026  |
    | Rendimento Mensal   | 0,5% + TR   |
```

---

### Funcionalidade: Consultar Extrato
**User Story:** US11  
**Requisitos:** RF23, RF24, RF25, RF26

#### Contexto:
```gherkin
Dado que sou um cliente autenticado
E possuo uma conta com as seguintes transações:
  | Data       | Tipo                 | Descrição           | Valor       | Saldo     |
  | 01/02/2026 | Depósito             | Depósito em dinheiro| +R$ 1000,00 | 1000,00   |
  | 05/02/2026 | Saque                | Saque no caixa      | -R$ 200,00  | 800,00    |
  | 10/02/2026 | Transferência Enviada| TED para Maria      | -R$ 150,00  | 650,00    |
  | 12/02/2026 | Transferência Recebida| De João           | +R$ 300,00  | 950,00    |
```

#### Cenário: Consulta de extrato por período

```gherkin
Cenário: Consulta de extrato por período
  Quando acesso a opção "Extrato"
  E seleciono o período de "01/02/2026" até "15/02/2026"
  E clico em "Consultar"
  Então o sistema deve exibir todas as 4 transações do período
  E deve ordenar por data (mais recente primeiro)
  E cada transação deve mostrar: data, tipo, descrição, valor, saldo resultante
```

#### Cenário: Filtro de extrato por tipo de transação

```gherkin
Cenário: Filtro de extrato por tipo de transação
  Quando acesso o extrato
  E seleciono o filtro "Transferências Recebidas"
  Então o sistema deve exibir apenas 1 transação
  E deve ser a transferência recebida de João no valor de R$ 300,00
```

#### Cenário: Exportação de extrato em PDF

```gherkin
Cenário: Exportação de extrato em PDF
  Quando acesso o extrato do período "01/02/2026" a "15/02/2026"
  E clico em "Exportar PDF"
  Então o sistema deve gerar um arquivo PDF
  E o PDF deve conter:
    | Elemento                | Conteúdo                    |
    | Cabeçalho               | Nome do cliente             |
    | Dados da Conta          | Agência, conta, tipo        |
    | Período                 | 01/02/2026 a 15/02/2026     |
    | Listagem de Transações  | Todas as 4 transações       |
    | Saldo Inicial           | R$ 0,00                     |
    | Saldo Final             | R$ 950,00                   |
  E deve iniciar o download do arquivo
```

#### Cenário: Tentativa de consulta de período superior a 90 dias

```gherkin
Cenário: Tentativa de consulta de período superior a 90 dias
  Quando acesso a opção "Extrato"
  E seleciono o período de "01/11/2025" até "15/02/2026"
  E clico em "Consultar"
  Então o sistema deve exibir a mensagem "Período máximo para consulta é de 90 dias"
  E deve sugerir ajustar as datas
  E não deve exibir o extrato
```

---

## 7. Autenticação

### Funcionalidade: Login no Sistema
**User Story:** US15  
**Requisitos:** RF27

#### Cenário: Login bem-sucedido

```gherkin
Cenário: Login bem-sucedido
  Dado que sou um cliente cadastrado
  E minhas credenciais são:
    | CPF   | 123.456.789-09 |
    | Senha | Senha@123      |
  Quando acesso a página de login
  E informo o CPF "123.456.789-09"
  E informo a senha "Senha@123"
  E clico em "Entrar"
  Então o sistema deve validar as credenciais
  E deve criar uma sessão válida por 15 minutos
  E deve registrar o login no log de auditoria
  E deve exibir a data/hora do último acesso
  E devo ser redirecionado para a página inicial logada
```

#### Cenário: Login com senha incorreta

```gherkin
Cenário: Login com senha incorreta
  Dado que sou um cliente cadastrado
  Quando tento fazer login com senha incorreta
  Então o sistema deve exibir a mensagem "CPF ou senha incorretos"
  E deve registrar a tentativa falha
  E não deve criar sessão
  E deve permanecer na página de login
```

#### Cenário: Bloqueio após 3 tentativas incorretas

```gherkin
Cenário: Bloqueio após 3 tentativas incorretas
  Dado que já tentei fazer login 2 vezes com senha incorreta
  Quando tento fazer login com senha incorreta pela 3ª vez
  Então o sistema deve bloquear minha conta temporariamente
  E deve exibir a mensagem "Conta bloqueada após 3 tentativas incorretas. Desbloqueie via email ou contate o gerente."
  E deve enviar email sobre o bloqueio
  E não deve permitir novas tentativas de login
```

#### Cenário: Expiração de sessão por inatividade

```gherkin
Cenário: Expiração de sessão por inatividade
  Dado que estou logado no sistema
  E fiquei inativo por 15 minutos
  Quando tento acessar qualquer página do sistema
  Então o sistema deve invalidar minha sessão
  E deve redirecionar para a página de login
  E deve exibir a mensagem "Sua sessão expirou por inatividade"
```

---

### Funcionalidade: Logout do Sistema
**User Story:** US16  
**Requisitos:** RF28

#### Cenário: Logout bem-sucedido

```gherkin
Cenário: Logout bem-sucedido
  Dado que estou autenticado no sistema
  Quando clico no botão "Sair"
  Então o sistema deve invalidar minha sessão imediatamente
  E deve registrar o logout no log de auditoria
  E deve redirecionar para a página de login
  E deve exibir a mensagem "Logout realizado com sucesso"
```

#### Cenário: Tentativa de acesso após logout

```gherkin
Cenário: Tentativa de acesso após logout
  Dado que fiz logout do sistema
  Quando tento acessar uma página protegida diretamente pela URL
  Então o sistema deve redirecionar para a página de login
  E deve exibir a mensagem "Você precisa estar autenticado para acessar esta página"
```

---

### Funcionalidade: Recuperar Senha
**User Story:** US17  
**Requisitos:** RF29, RF30

#### Cenário: Solicitação de recuperação de senha

```gherkin
Cenário: Solicitação de recuperação de senha
  Dado que sou um cliente cadastrado com email "joao@email.com"
  E estou na página de login
  Quando clico em "Esqueci minha senha"
  E informo meu CPF "123.456.789-09"
  E informo meu email "joao@email.com"
  E clico em "Recuperar Senha"
  Então o sistema deve validar CPF e email
  E deve gerar um token temporário válido por 30 minutos
  E deve enviar email com link de recuperação
  E deve exibir a mensagem "Email enviado com instruções para recuperação de senha"
```

#### Cenário: Redefinição de senha com token válido

```gherkin
Cenário: Redefinição de senha com token válido
  Dado que solicitei recuperação de senha
  E recebi um token válido por email
  Quando acesso o link de recuperação
  E informo nova senha "NovaSenha@456"
  E confirmo a nova senha "NovaSenha@456"
  E clico em "Alterar Senha"
  Então o sistema deve validar a nova senha conforme política
  E deve validar que não é uma das últimas 3 senhas
  E deve atualizar a senha
  E deve invalidar o token
  E deve exibir a mensagem "Senha alterada com sucesso"
  E devo poder fazer login com a nova senha
```

#### Cenário: Tentativa de redefinição com senha que não atende política

```gherkin
Cenário: Tentativa de redefinição com senha que não atende política
  Dado que estou no fluxo de recuperação de senha
  Quando informo nova senha "123456"
  E tento confirmar a alteração
  Então o sistema deve exibir mensagem "Senha deve ter no mínimo 8 caracteres com letras e números"
  E não deve alterar a senha
```

#### Cenário: Tentativa de uso de token expirado

```gherkin
Cenário: Tentativa de uso de token expirado
  Dado que solicitei recuperação de senha há 35 minutos
  Quando tento acessar o link de recuperação
  Então o sistema deve exibir a mensagem "Token expirado. Solicite nova recuperação de senha."
  E deve redirecionar para a página de recuperação
```

#### Cenário: Tentativa de reutilização de senha antiga

```gherkin
Cenário: Tentativa de reutilização de senha antiga
  Dado que estou alterando minha senha
  E minhas últimas senhas foram "Senha@123", "Senha@456", "Senha@789"
  Quando informo nova senha "Senha@456"
  Então o sistema deve exibir a mensagem "Senha já utilizada recentemente. Escolha uma senha diferente."
  E não deve alterar a senha
```

---

## Resumo de Cobertura

### Estatísticas dos Cenários BDD

| Funcionalidade               | Cenários | Esquemas | Total |
|------------------------------|----------|----------|-------|
| Cadastro de Cliente          | 4        | 1        | 5     |
| Atualização de Dados         | 3        | 0        | 3     |
| Abertura de Conta Corrente   | 3        | 0        | 3     |
| Abertura de Conta Poupança   | 1        | 0        | 1     |
| Encerramento de Conta        | 3        | 0        | 3     |
| Consulta de Dados da Conta   | 2        | 0        | 2     |
| Realizar Depósito            | 3        | 1        | 4     |
| Realizar Saque               | 5        | 0        | 5     |
| Transferência Interna        | 4        | 0        | 4     |
| TED                          | 3        | 0        | 3     |
| DOC                          | 1        | 0        | 1     |
| Consultar Saldo              | 2        | 0        | 2     |
| Consultar Extrato            | 4        | 0        | 4     |
| Login                        | 4        | 0        | 4     |
| Logout                       | 2        | 0        | 2     |
| Recuperar Senha              | 5        | 0        | 5     |
| **TOTAL**                    | **49**   | **2**    | **51**|

### Tipos de Cenários

- **Happy Path (Fluxo Principal)**: 17 cenários
- **Validações e Exceções**: 27 cenários
- **Regras de Negócio**: 7 cenários
- **Esquemas Parametrizados**: 2 esquemas

### Cobertura por User Story

Todas as 17 User Stories possuem cenários BDD correspondentes, garantindo cobertura completa dos requisitos funcionais.

---

**Última Atualização**: Fevereiro de 2026  
**Versão**: 1.0  
**Total de Cenários**: 51  
**Framework Compatível**: Cucumber, Behave, SpecFlow
