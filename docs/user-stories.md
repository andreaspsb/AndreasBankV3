# User Stories - AndreasBankV2

## Índice

- [Épico 1: Cadastro de Clientes](#épico-1-cadastro-de-clientes)
- [Épico 2: Gestão de Contas](#épico-2-gestão-de-contas)
- [Épico 3: Operações Financeiras](#épico-3-operações-financeiras)
- [Épico 4: Segurança](#épico-4-segurança)
- [Resumo de Prioridades](#resumo-de-prioridades)

---

## Épico 1: Cadastro de Clientes

**Descrição**: Funcionalidades relacionadas ao cadastro e gestão de dados dos clientes pessoas físicas.

**Objetivo de Negócio**: Permitir que novos clientes se cadastrem no banco e mantenham seus dados atualizados.

---

### US01 - Cadastro de Novo Cliente

**Como** uma pessoa física  
**Eu quero** me cadastrar no AndreasBankV2  
**Para** poder utilizar os serviços bancários

**Requisitos relacionados**: RF01, RF02  
**Prioridade**: Alta  
**Estimativa**: 8 pontos

#### Critérios de Aceitação

1. Sistema deve solicitar CPF, nome completo, data de nascimento, email e telefone como campos obrigatórios
2. Sistema deve validar formato do CPF e dígitos verificadores
3. Sistema não deve permitir cadastro duplicado do mesmo CPF
4. Cliente deve ser maior de 16 anos
5. Sistema deve validar formato de email e telefone
6. Sistema deve enviar email de confirmação após cadastro bem-sucedido
7. Campos opcionais (endereço, nome da mãe, renda) podem ser preenchidos posteriormente

#### Regras de Negócio Aplicáveis

- RN12: Documentação obrigatória
- RN11: Idade mínima (16 anos para cadastro inicial)

---

### US02 - Atualização de Dados Cadastrais

**Como** um cliente cadastrado  
**Eu quero** atualizar meus dados pessoais  
**Para** manter minhas informações sempre corretas

**Requisitos relacionados**: RF03  
**Prioridade**: Média  
**Estimativa**: 5 pontos

#### Critérios de Aceitação

1. Cliente autenticado pode acessar página de atualização de dados
2. Campos editáveis: nome, email, telefone, endereço, nome da mãe, renda
3. Campos não editáveis: CPF, data de nascimento, data de cadastro
4. Sistema deve validar novos dados antes de salvar
5. Sistema deve solicitar confirmação antes de salvar alterações
6. Sistema deve exibir mensagem de sucesso após atualização
7. Sistema deve enviar email notificando sobre alteração dos dados

#### Regras de Negócio Aplicáveis

- RN12: Documentação obrigatória

---

### US03 - Consulta de Informações Pessoais

**Como** um cliente  
**Eu quero** consultar meus dados cadastrais  
**Para** verificar se estão corretos

**Requisitos relacionados**: RF04, RF05  
**Prioridade**: Média  
**Estimativa**: 3 pontos

#### Critérios de Aceitação

1. Cliente autenticado pode visualizar todos seus dados cadastrais
2. Dados sensíveis (CPF) devem ser parcialmente mascarados (***. 123.456-**)
3. Sistema deve exibir data do último cadastro/atualização
4. Sistema deve exibir status do cadastro (ativo/inativo)
5. Cliente deve ter opção de editar dados diretamente desta tela

---

## Épico 2: Gestão de Contas

**Descrição**: Funcionalidades para abertura, consulta e encerramento de contas bancárias.

**Objetivo de Negócio**: Permitir que clientes abram e gerenciem suas contas correntes e poupança.

---

### US04 - Abertura de Conta Corrente

**Como** um cliente cadastrado maior de 18 anos  
**Eu quero** abrir uma conta corrente  
**Para** realizar operações bancárias cotidianas

**Requisitos relacionados**: RF06  
**Prioridade**: Alta  
**Estimativa**: 8 pontos

#### Critérios de Aceitação

1. Sistema deve verificar se cliente tem cadastro completo
2. Sistema deve verificar se cliente é maior de 18 anos
3. Sistema deve verificar se cliente já possui conta corrente (máximo 1)
4. Sistema deve gerar número de conta e agência automaticamente
5. Conta criada deve ter tipo "Corrente" e status "Ativa"
6. Sistema deve definir senha de 4 dígitos para operações (opcional)
7. Sistema deve exibir dados completos da conta criada
8. Sistema deve enviar email com dados da nova conta

#### Regras de Negócio Aplicáveis

- RN11: Idade mínima 18 anos para conta corrente
- RN13: Limite de 1 conta corrente por cliente
- RN12: Documentação obrigatória completa

---

### US05 - Abertura de Conta Poupança

**Como** um cliente cadastrado maior de 16 anos  
**Eu quero** abrir uma conta poupança  
**Para** guardar dinheiro e receber rendimentos mensais

**Requisitos relacionados**: RF07  
**Prioridade**: Alta  
**Estimativa**: 8 pontos

#### Critérios de Aceitação

1. Sistema deve verificar se cliente tem cadastro completo
2. Sistema deve verificar se cliente é maior de 16 anos
3. Sistema deve verificar se cliente já possui conta poupança (máximo 1)
4. Sistema deve gerar número de conta e agência automaticamente
5. Conta criada deve ter tipo "Poupança" e status "Ativa"
6. Sistema deve informar taxa de rendimento atual
7. Sistema deve exibir dados completos da conta criada
8. Sistema deve enviar email com dados da nova conta

#### Regras de Negócio Aplicáveis

- RN11: Idade mínima 16 anos para conta poupança
- RN13: Limite de 1 conta poupança por cliente
- RN05: Rendimento da poupança (0,5% ao mês + TR)

---

### US06 - Encerramento de Conta

**Como** um cliente  
**Eu quero** encerrar minha conta bancária  
**Para** deixar de usar os serviços do banco

**Requisitos relacionados**: RF09  
**Prioridade**: Média  
**Estimativa**: 5 pontos

#### Critérios de Aceitação

1. Cliente pode solicitar encerramento de qualquer conta sua
2. Sistema deve verificar se saldo está zerado (R$ 0,00)
3. Sistema deve verificar se não há transações pendentes
4. Sistema deve verificar se não há débitos automáticos ativos
5. Sistema deve solicitar confirmação da operação
6. Sistema deve registrar data e motivo do encerramento
7. Conta encerrada muda status para "Encerrada"
8. Sistema deve enviar email confirmando encerramento

#### Regras de Negócio Aplicáveis

- RN14: Conta só pode ser encerrada com saldo zerado
- RN14: Encerramento definitivo após 30 dias

---

### US07 - Consulta de Dados da Conta

**Como** um cliente  
**Eu quero** consultar os dados das minhas contas  
**Para** verificar número da conta, agência e status

**Requisitos relacionados**: RF08, RF10  
**Prioridade**: Alta  
**Estimativa**: 3 pontos

#### Critérios de Aceitação

1. Sistema deve listar todas as contas do cliente
2. Para cada conta, exibir: número, agência, tipo, status, saldo atual
3. Sistema deve destacar conta principal (se houver)
4. Status possíveis: Ativa, Bloqueada, Encerrada, Inativa
5. Cliente pode selecionar uma conta para ver mais detalhes
6. Detalhes incluem: data de abertura, último acesso, limite disponível (se houver)

#### Regras de Negócio Aplicáveis

- RN13: Máximo de 2 contas por cliente
- RN15: Definição de conta inativa

---

## Épico 3: Operações Financeiras

**Descrição**: Funcionalidades para realizar operações financeiras como depósitos, saques, transferências e consultas.

**Objetivo de Negócio**: Permitir que clientes movimentem seu dinheiro de forma segura e prática.

---

### US08 - Realizar Depósito

**Como** um cliente  
**Eu quero** realizar depósito na minha conta  
**Para** aumentar meu saldo disponível

**Requisitos relacionados**: RF11, RF12, RF13  
**Prioridade**: Alta  
**Estimativa**: 5 pontos

#### Critérios de Aceitação

1. Sistema deve permitir depósito em dinheiro ou cheque
2. Para depósito em dinheiro: informar valor (mínimo R$ 1,00)
3. Para depósito em cheque: informar número do cheque, banco e valor
4. Sistema deve gerar número de comprovante único
5. Depósito em dinheiro credita imediatamente
6. Depósito em cheque tem prazo de compensação
7. Sistema deve exibir comprovante com data, hora, valor e número de autenticação
8. Cliente pode imprimir ou enviar comprovante por email

#### Regras de Negócio Aplicáveis

- RN08: Depósito mínimo R$ 1,00
- RN09: Tempo de compensação (dinheiro imediato, cheque 1-2 dias úteis)

---

### US09 - Realizar Saque

**Como** um cliente  
**Eu quero** realizar saque da minha conta  
**Para** retirar dinheiro

**Requisitos relacionados**: RF14, RF15, RF16  
**Prioridade**: Alta  
**Estimativa**: 5 pontos

#### Critérios de Aceitação

1. Cliente seleciona conta para saque
2. Sistema exibe saldo disponível atual
3. Cliente informa valor do saque (mínimo R$ 10,00, máximo R$ 1.000,00)
4. Sistema valida se há saldo suficiente
5. Sistema verifica limite diário de saque (R$ 1.000,00)
6. Sistema debita valor imediatamente
7. Sistema gera comprovante de saque
8. Sistema atualiza saldo em tempo real

#### Regras de Negócio Aplicáveis

- RN08: Saque mínimo R$ 10,00, máximo R$ 1.000,00 por operação
- RN04: Limite diário de saque
- RN01: Validação de saldo mínimo

---

### US10 - Consultar Saldo

**Como** um cliente  
**Eu quero** consultar o saldo da minha conta  
**Para** saber quanto dinheiro tenho disponível

**Requisitos relacionados**: RF22  
**Prioridade**: Alta  
**Estimativa**: 2 pontos

#### Critérios de Aceitação

1. Cliente seleciona conta para consulta
2. Sistema exibe saldo atualizado em tempo real
3. Sistema diferencia: saldo disponível, saldo bloqueado
4. Para conta corrente: exibir limite de cheque especial disponível
5. Sistema exibe data e hora da última atualização
6. Consulta não deve ter taxa

#### Regras de Negócio Aplicáveis

- RN06: Cheque especial disponível para conta corrente

---

### US11 - Consultar Extrato

**Como** um cliente  
**Eu quero** consultar o extrato da minha conta  
**Para** ver o histórico de transações

**Requisitos relacionados**: RF23, RF24, RF25, RF26  
**Prioridade**: Alta  
**Estimativa**: 5 pontos

#### Critérios de Aceitação

1. Cliente seleciona conta e período desejado (máximo 90 dias)
2. Sistema lista todas as transações do período em ordem cronológica
3. Para cada transação exibir: data, hora, tipo, descrição, valor, saldo resultante
4. Cliente pode filtrar por tipo de transação
5. Cliente pode solicitar extrato completo (desde abertura da conta)
6. Cliente pode exportar extrato em PDF
7. PDF deve conter cabeçalho com dados do cliente, conta e período

#### Regras de Negócio Aplicáveis

- RN23: Rastreabilidade de operações

---

### US12 - Realizar Transferência Entre Contas Internas

**Como** um cliente  
**Eu quero** transferir dinheiro para outra conta do AndreasBankV2  
**Para** enviar dinheiro para terceiros sem taxa

**Requisitos relacionados**: RF17, RF20, RF21  
**Prioridade**: Alta  
**Estimativa**: 8 pontos

#### Critérios de Aceitação

1. Cliente informa: conta origem, conta destino (agência e número), valor
2. Sistema valida se conta destino existe
3. Sistema valida se há saldo suficiente
4. Sistema não cobra taxa para transferências internas
5. Transferência é processada instantaneamente
6. Sistema atualiza saldo de ambas as contas imediatamente
7. Sistema gera comprovante com dados completos
8. Ambos os envolvidos recebem notificação da transferência

#### Regras de Negócio Aplicáveis

- RN02: Transferência interna gratuita
- RN04: Limite de R$ 5.000,00 por transação para terceiros
- RN09: Transferências internas são imediatas

---

### US13 - Realizar TED

**Como** um cliente  
**Eu quero** realizar transferência TED para outro banco  
**Para** enviar dinheiro para contas de outros bancos no mesmo dia

**Requisitos relacionados**: RF18, RF20, RF21  
**Prioridade**: Alta  
**Estimativa**: 8 pontos

#### Critérios de Aceitação

1. Cliente informa: banco destino, agência, conta, CPF/CNPJ favorecido, valor
2. Sistema exibe taxa de TED (R$ 8,50)
3. Sistema valida se há saldo suficiente (valor + taxa)
4. Sistema verifica horário (até 17h para mesmo dia)
5. TED após 17h é agendada para próximo dia útil
6. Sistema debita valor + taxa da conta origem
7. Sistema gera comprovante detalhado
8. Cliente recebe notificação quando TED for processada

#### Regras de Negócio Aplicáveis

- RN02: Taxa de TED R$ 8,50
- RN03: TED até 17h é processada no mesmo dia
- RN04: Limite de R$ 5.000,00 por transação

---

### US14 - Realizar DOC

**Como** um cliente  
**Eu quero** realizar transferência DOC para outro banco  
**Para** enviar dinheiro pagando taxa menor

**Requisitos relacionados**: RF19, RF20, RF21  
**Prioridade**: Média  
**Estimativa**: 8 pontos

#### Critérios de Aceitação

1. Cliente informa: banco destino, agência, conta, CPF/CNPJ favorecido, valor
2. Sistema exibe taxa de DOC (R$ 5,00)
3. Sistema valida se há saldo suficiente (valor + taxa)
4. Sistema informa que DOC será processada em D+1
5. Sistema debita valor + taxa da conta origem
6. Sistema gera comprovante detalhado
7. Cliente recebe notificação quando DOC for compensada

#### Regras de Negócio Aplicáveis

- RN02: Taxa de DOC R$ 5,00
- RN03: DOC sempre processada em D+1
- RN04: Limite de R$ 5.000,00 por transação

---

## Épico 4: Segurança

**Descrição**: Funcionalidades relacionadas à autenticação, autorização e segurança do sistema.

**Objetivo de Negócio**: Garantir que apenas usuários autorizados acessem o sistema e que os dados estejam protegidos.

---

### US15 - Login no Sistema

**Como** um cliente cadastrado  
**Eu quero** fazer login no sistema  
**Para** acessar minha conta bancária

**Requisitos relacionados**: RF27  
**Prioridade**: Alta  
**Estimativa**: 5 pontos

#### Critérios de Aceitação

1. Cliente informa CPF e senha
2. Sistema valida credenciais
3. Sistema registra tentativa de login (sucesso ou falha)
4. Após 3 tentativas incorretas, conta é bloqueada temporariamente
5. Login bem-sucedido cria sessão válida por 15 minutos de inatividade
6. Sistema exibe data e hora do último acesso
7. Sistema envia notificação de novo acesso por email

#### Regras de Negócio Aplicáveis

- RN17: Bloqueio após 3 tentativas incorretas
- RN18: Token de sessão válido por 15 minutos de inatividade

---

### US16 - Logout do Sistema

**Como** um cliente autenticado  
**Eu quero** fazer logout do sistema  
**Para** encerrar minha sessão com segurança

**Requisitos relacionados**: RF28  
**Prioridade**: Alta  
**Estimativa**: 2 pontos

#### Critérios de Aceitação

1. Cliente clica em botão de logout
2. Sistema invalida sessão imediatamente
3. Sistema redireciona para página de login
4. Tentativa de acessar página protegida após logout deve redirecionar para login
5. Sistema registra logout em log de auditoria

#### Regras de Negócio Aplicáveis

- RN18: Token de sessão deve ser invalidado

---

### US17 - Recuperar Senha

**Como** um cliente que esqueceu a senha  
**Eu quero** recuperar minha senha  
**Para** conseguir acessar minha conta novamente

**Requisitos relacionados**: RF29, RF30  
**Prioridade**: Alta  
**Estimativa**: 5 pontos

#### Critérios de Aceitação

1. Cliente clica em "Esqueci minha senha" na tela de login
2. Sistema solicita CPF e email cadastrado
3. Sistema valida se CPF e email correspondem a um cadastro
4. Sistema envia email com token temporário (válido por 30 minutos)
5. Cliente acessa link do email e informa nova senha
6. Nova senha deve atender política de senhas
7. Nova senha não pode ser igual às últimas 3 senhas
8. Sistema confirma alteração e permite login com nova senha

#### Regras de Negócio Aplicáveis

- RN16: Política de senhas (mínimo 8 caracteres, letras e números)
- RN16: Não reutilizar últimas 3 senhas
- RN18: Token de recuperação válido por 30 minutos

---

## Resumo de Prioridades

### Alta Prioridade (13 User Stories)
- US01: Cadastro de Novo Cliente
- US04: Abertura de Conta Corrente
- US05: Abertura de Conta Poupança
- US07: Consulta de Dados da Conta
- US08: Realizar Depósito
- US09: Realizar Saque
- US10: Consultar Saldo
- US11: Consultar Extrato
- US12: Transferência Entre Contas Internas
- US13: Realizar TED
- US15: Login no Sistema
- US16: Logout do Sistema
- US17: Recuperar Senha

### Média Prioridade (4 User Stories)
- US02: Atualização de Dados Cadastrais
- US03: Consulta de Informações Pessoais
- US06: Encerramento de Conta
- US14: Realizar DOC

---

## Matriz de Rastreabilidade User Stories → Requisitos

| User Story | Requisitos Funcionais | Regras de Negócio | Épico |
|------------|-----------------------|-------------------|-------|
| US01 | RF01, RF02 | RN11, RN12 | Cadastro de Clientes |
| US02 | RF03 | RN12 | Cadastro de Clientes |
| US03 | RF04, RF05 | - | Cadastro de Clientes |
| US04 | RF06 | RN11, RN12, RN13 | Gestão de Contas |
| US05 | RF07 | RN05, RN11, RN13 | Gestão de Contas |
| US06 | RF09 | RN14 | Gestão de Contas |
| US07 | RF08, RF10 | RN13, RN15 | Gestão de Contas |
| US08 | RF11, RF12, RF13 | RN08, RN09 | Operações Financeiras |
| US09 | RF14, RF15, RF16 | RN01, RN04, RN08 | Operações Financeiras |
| US10 | RF22 | RN06 | Operações Financeiras |
| US11 | RF23, RF24, RF25, RF26 | RN23 | Operações Financeiras |
| US12 | RF17, RF20, RF21 | RN02, RN04, RN09 | Operações Financeiras |
| US13 | RF18, RF20, RF21 | RN02, RN03, RN04 | Operações Financeiras |
| US14 | RF19, RF20, RF21 | RN02, RN03, RN04 | Operações Financeiras |
| US15 | RF27 | RN17, RN18 | Segurança |
| US16 | RF28 | RN18 | Segurança |
| US17 | RF29, RF30 | RN16, RN18 | Segurança |

---

**Última Atualização**: Fevereiro de 2026  
**Versão**: 1.0  
**Total de User Stories**: 17  
**Total de Pontos Estimados**: 89 pontos
