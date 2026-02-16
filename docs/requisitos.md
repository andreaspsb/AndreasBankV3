# Requisitos do Sistema - AndreasBankV2

## Índice

- [1. Requisitos Funcionais](#1-requisitos-funcionais)
  - [1.1 Cadastro e Gestão de Clientes](#11-cadastro-e-gestão-de-clientes)
  - [1.2 Gestão de Contas Bancárias](#12-gestão-de-contas-bancárias)
  - [1.3 Operações de Depósito](#13-operações-de-depósito)
  - [1.4 Operações de Saque](#14-operações-de-saque)
  - [1.5 Transferências](#15-transferências)
  - [1.6 Consultas e Extratos](#16-consultas-e-extratos)
  - [1.7 Autenticação e Autorização](#17-autenticação-e-autorização)
- [2. Requisitos Não-Funcionais](#2-requisitos-não-funcionais)
  - [2.1 Segurança](#21-segurança)
  - [2.2 Performance](#22-performance)
  - [2.3 Usabilidade](#23-usabilidade)
  - [2.4 Confiabilidade](#24-confiabilidade)
  - [2.5 Auditoria](#25-auditoria)
- [3. Regras de Negócio](#3-regras-de-negócio)
  - [3.1 Regras Financeiras](#31-regras-financeiras)
  - [3.2 Regras de Conta](#32-regras-de-conta)
  - [3.3 Regras de Segurança](#33-regras-de-segurança)

---

## 1. Requisitos Funcionais

### 1.1 Cadastro e Gestão de Clientes

**RF01 - Cadastrar Cliente Pessoa Física**
- O sistema deve permitir o cadastro de novos clientes pessoas físicas
- Campos obrigatórios: CPF, nome completo, data de nascimento, email, telefone
- Campos opcionais: endereço completo, nome da mãe, renda mensal
- **Prioridade**: Alta
- **User Stories relacionadas**: US01

**RF02 - Validar CPF**
- O sistema deve validar o formato e dígitos verificadores do CPF informado
- O sistema não deve permitir cadastro duplicado de CPF
- **Prioridade**: Alta
- **User Stories relacionadas**: US01

**RF03 - Atualizar Dados Cadastrais**
- O sistema deve permitir que clientes atualizem seus dados cadastrais
- Campos não editáveis: CPF, data de abertura de cadastro
- Campos editáveis: nome, email, telefone, endereço
- **Prioridade**: Média
- **User Stories relacionadas**: US02

**RF04 - Consultar Dados do Cliente**
- O sistema deve permitir que o cliente consulte seus próprios dados cadastrais
- O sistema deve permitir que gerentes consultem dados de qualquer cliente
- **Prioridade**: Média
- **User Stories relacionadas**: US03

**RF05 - Inativar Cadastro de Cliente**
- O sistema deve permitir a inativação de cadastro de cliente
- Cliente inativado não pode realizar operações mas mantém histórico
- Apenas gerentes podem inativar clientes
- **Prioridade**: Baixa
- **User Stories relacionadas**: US03

### 1.2 Gestão de Contas Bancárias

**RF06 - Abrir Conta Corrente**
- O sistema deve permitir abertura de conta corrente para clientes cadastrados
- Número da conta e agência devem ser gerados automaticamente
- Cliente deve ser maior de 18 anos para conta corrente
- **Prioridade**: Alta
- **User Stories relacionadas**: US04

**RF07 - Abrir Conta Poupança**
- O sistema deve permitir abertura de conta poupança para clientes cadastrados
- Número da conta e agência devem ser gerados automaticamente
- Cliente deve ser maior de 16 anos para conta poupança
- **Prioridade**: Alta
- **User Stories relacionadas**: US05

**RF08 - Consultar Dados da Conta**
- O sistema deve permitir consulta de dados da conta (número, agência, tipo, status)
- O sistema deve exibir o titular da conta
- **Prioridade**: Alta
- **User Stories relacionadas**: US07

**RF09 - Encerrar Conta**
- O sistema deve permitir encerramento de conta bancária
- Conta só pode ser encerrada com saldo zerado
- Encerramento deve registrar data e motivo
- **Prioridade**: Média
- **User Stories relacionadas**: US06

**RF10 - Listar Contas do Cliente**
- O sistema deve permitir que cliente visualize todas as suas contas
- O sistema deve exibir status de cada conta (ativa, bloqueada, encerrada)
- **Prioridade**: Média
- **User Stories relacionadas**: US07

### 1.3 Operações de Depósito

**RF11 - Realizar Depósito em Dinheiro**
- O sistema deve permitir registro de depósitos em dinheiro
- Depósito deve informar: conta destino, valor, data/hora
- Valor mínimo para depósito: R$ 1,00
- **Prioridade**: Alta
- **User Stories relacionadas**: US08

**RF12 - Realizar Depósito por Cheque**
- O sistema deve permitir registro de depósitos em cheque
- Depósito deve informar: número do cheque, banco, valor
- Depósitos em cheque têm prazo de compensação
- **Prioridade**: Média
- **User Stories relacionadas**: US08

**RF13 - Registrar Comprovante de Depósito**
- O sistema deve gerar comprovante com número único para cada depósito
- Comprovante deve conter: data, hora, valor, conta, número de autenticação
- **Prioridade**: Alta
- **User Stories relacionadas**: US08

### 1.4 Operações de Saque

**RF14 - Realizar Saque**
- O sistema deve permitir saques em contas com saldo suficiente
- Saque deve informar: conta origem, valor, data/hora
- Valor mínimo para saque: R$ 10,00
- **Prioridade**: Alta
- **User Stories relacionadas**: US09

**RF15 - Validar Limite de Saque**
- O sistema deve verificar se há saldo disponível antes de autorizar saque
- O sistema deve considerar cheque especial quando disponível
- Saque não pode deixar saldo negativo (exceto com cheque especial)
- **Prioridade**: Alta
- **User Stories relacionadas**: US09

**RF16 - Aplicar Limite Diário de Saque**
- O sistema deve aplicar limite diário de saque conforme perfil do cliente
- Limite padrão: R$ 1.000,00 por dia
- Gerente pode autorizar saques acima do limite
- **Prioridade**: Média
- **User Stories relacionadas**: US09

### 1.5 Transferências

**RF17 - Transferir Entre Contas do Mesmo Banco**
- O sistema deve permitir transferências entre contas do AndreasBankV2
- Transferência deve ser instantânea e sem taxa
- Campos obrigatórios: conta origem, conta destino, valor
- **Prioridade**: Alta
- **User Stories relacionadas**: US12

**RF18 - Realizar TED**
- O sistema deve permitir transferências TED para outros bancos
- TED deve cobrar taxa conforme tabela do banco
- TED enviada até 17h é processada no mesmo dia
- **Prioridade**: Alta
- **User Stories relacionadas**: US13

**RF19 - Realizar DOC**
- O sistema deve permitir transferências DOC para outros bancos
- DOC deve cobrar taxa conforme tabela do banco
- DOC é processada em D+1 (próximo dia útil)
- **Prioridade**: Média
- **User Stories relacionadas**: US14

**RF20 - Validar Dados da Transferência**
- O sistema deve validar conta destino antes de processar transferência
- O sistema deve validar saldo disponível na conta origem
- O sistema deve validar limite de transferência do cliente
- **Prioridade**: Alta
- **User Stories relacionadas**: US12, US13, US14

**RF21 - Gerar Comprovante de Transferência**
- O sistema deve gerar comprovante para toda transferência realizada
- Comprovante deve conter: dados completos origem/destino, valor, taxa, data/hora
- **Prioridade**: Alta
- **User Stories relacionadas**: US12, US13, US14

### 1.6 Consultas e Extratos

**RF22 - Consultar Saldo**
- O sistema deve permitir consulta de saldo em tempo real
- Saldo deve exibir: saldo disponível, saldo bloqueado, limite disponível
- **Prioridade**: Alta
- **User Stories relacionadas**: US10

**RF23 - Gerar Extrato por Período**
- O sistema deve gerar extratos filtrando por período de datas
- Extrato deve listar todas as transações do período
- Período máximo: 90 dias
- **Prioridade**: Alta
- **User Stories relacionadas**: US11

**RF24 - Gerar Extrato Completo**
- O sistema deve permitir geração de extrato completo da conta
- Extrato completo lista todas as transações desde abertura da conta
- **Prioridade**: Média
- **User Stories relacionadas**: US11

**RF25 - Filtrar Transações por Tipo**
- O sistema deve permitir filtrar extrato por tipo de transação
- Tipos: depósito, saque, transferência enviada, transferência recebida
- **Prioridade**: Baixa
- **User Stories relacionadas**: US11

**RF26 - Exportar Extrato**
- O sistema deve permitir exportação de extratos em formato PDF
- Extrato exportado deve conter cabeçalho com dados do cliente e período
- **Prioridade**: Baixa
- **User Stories relacionadas**: US11

### 1.7 Autenticação e Autorização

**RF27 - Realizar Login**
- O sistema deve autenticar usuários via CPF e senha
- Sistema deve bloquear conta após 3 tentativas incorretas
- Sessão expira após 15 minutos de inatividade
- **Prioridade**: Alta
- **User Stories relacionadas**: US15

**RF28 - Realizar Logout**
- O sistema deve permitir logout manual do usuário
- Logout deve invalidar a sessão imediatamente
- **Prioridade**: Alta
- **User Stories relacionadas**: US16

**RF29 - Recuperar Senha**
- O sistema deve permitir recuperação de senha via email
- Sistema deve enviar token temporário válido por 30 minutos
- **Prioridade**: Alta
- **User Stories relacionadas**: US17

**RF30 - Alterar Senha**
- O sistema deve permitir alteração de senha pelo próprio usuário
- Senha deve ter no mínimo 8 caracteres com letras e números
- Sistema não deve permitir reutilização das últimas 3 senhas
- **Prioridade**: Alta
- **User Stories relacionadas**: US17

---

## 2. Requisitos Não-Funcionais

### 2.1 Segurança

**RNF01 - Criptografia de Dados Sensíveis**
- Senhas devem ser armazenadas com hash usando algoritmo bcrypt ou superior
- Dados sensíveis (CPF, contas) devem ser criptografados em repouso
- Comunicação deve usar HTTPS/TLS 1.3 ou superior
- **Categoria**: Segurança
- **Prioridade**: Alta

**RNF02 - Proteção contra OWASP Top 10**
- Sistema deve implementar proteções contra as vulnerabilidades do OWASP Top 10
- Incluindo: SQL Injection, XSS, CSRF, autenticação quebrada
- **Categoria**: Segurança
- **Prioridade**: Alta

**RNF03 - Conformidade com LGPD**
- Sistema deve estar em conformidade com a Lei Geral de Proteção de Dados
- Implementar funcionalidades de consentimento, portabilidade e exclusão de dados
- **Categoria**: Segurança/Legal
- **Prioridade**: Alta

**RNF04 - Controle de Acesso Baseado em Papéis (RBAC)**
- Sistema deve implementar controle de acesso com papéis: Cliente, Gerente, Administrador
- Cada papel tem permissões específicas
- **Categoria**: Segurança
- **Prioridade**: Alta

**RNF05 - Gestão de Sessões Segura**
- Tokens de sessão devem ser únicos e não previsíveis
- Sessões devem expirar automaticamente
- Sistema deve detectar tentativas de sequestro de sessão
- **Categoria**: Segurança
- **Prioridade**: Alta

### 2.2 Performance

**RNF06 - Tempo de Resposta para Consultas**
- Consultas simples (saldo, dados da conta) devem responder em até 500ms
- 95% das requisições devem ser atendidas neste tempo
- **Categoria**: Performance
- **Prioridade**: Alta

**RNF07 - Tempo de Resposta para Transações**
- Transações (depósito, saque, transferência) devem processar em até 2 segundos
- Transferências entre contas do mesmo banco devem ser instantâneas
- **Categoria**: Performance
- **Prioridade**: Alta

**RNF08 - Capacidade de Processamento**
- Sistema deve suportar mínimo de 1.000 transações simultâneas
- Sistema deve suportar mínimo de 10.000 usuários ativos
- **Categoria**: Performance/Escalabilidade
- **Prioridade**: Média

**RNF09 - Otimização de Banco de Dados**
- Consultas ao banco de dados devem usar índices apropriados
- Queries complexas não devem exceder 1 segundo
- **Categoria**: Performance
- **Prioridade**: Média

**RNF10 - Cache de Dados Frequentes**
- Dados frequentemente acessados devem ser cacheados
- Saldo e dados da conta podem ter cache de até 1 minuto
- **Categoria**: Performance
- **Prioridade**: Baixa

### 2.3 Usabilidade

**RNF11 - Interface Intuitiva**
- Interface deve seguir padrões de design conhecidos
- Operações principais devem ser acessíveis em até 3 cliques
- **Categoria**: Usabilidade
- **Prioridade**: Média

**RNF12 - Acessibilidade WCAG 2.1**
- Sistema deve atender nível AA das diretrizes WCAG 2.1
- Suporte a leitores de tela e navegação por teclado
- **Categoria**: Usabilidade/Acessibilidade
- **Prioridade**: Média

**RNF13 - Responsividade**
- Interface deve ser responsiva e funcionar em dispositivos móveis
- Suporte a resoluções desde 320px até 4K
- **Categoria**: Usabilidade
- **Prioridade**: Alta

**RNF14 - Mensagens de Erro Claras**
- Sistema deve exibir mensagens de erro claras e orientativas
- Erros não devem expor detalhes técnicos sensíveis
- **Categoria**: Usabilidade
- **Prioridade**: Média

**RNF15 - Feedback Visual**
- Sistema deve fornecer feedback imediato para ações do usuário
- Loading indicators para operações que levam mais de 500ms
- **Categoria**: Usabilidade
- **Prioridade**: Baixa

### 2.4 Confiabilidade

**RNF16 - Disponibilidade do Sistema**
- Sistema deve ter disponibilidade mínima de 99,5% (SLA)
- Janelas de manutenção devem ser programadas e comunicadas
- **Categoria**: Confiabilidade
- **Prioridade**: Alta

**RNF17 - Recuperação de Falhas**
- Sistema deve se recuperar automaticamente de falhas transientes
- Transações interrompidas devem ser revertidas (rollback)
- **Categoria**: Confiabilidade
- **Prioridade**: Alta

**RNF18 - Backup de Dados**
- Dados devem ter backup automatizado diário
- Backups devem ser testados mensalmente
- Retenção de backups por no mínimo 90 dias
- **Categoria**: Confiabilidade
- **Prioridade**: Alta

**RNF19 - Tolerância a Falhas**
- Sistema deve continuar operando mesmo com falhas parciais
- Componentes críticos devem ter redundância
- **Categoria**: Confiabilidade
- **Prioridade**: Média

**RNF20 - Integridade Transacional**
- Transações financeiras devem seguir propriedades ACID
- Garantia de consistência dos saldos em todas as operações
- **Categoria**: Confiabilidade/Integridade
- **Prioridade**: Alta

### 2.5 Auditoria

**RNF21 - Log de Transações**
- Todas as transações financeiras devem ser registradas em log
- Logs devem conter: usuário, data/hora, operação, valores, IP
- **Categoria**: Auditoria
- **Prioridade**: Alta

**RNF22 - Log de Acessos**
- Todos os acessos ao sistema devem ser registrados
- Incluir tentativas de login (sucesso e falha)
- **Categoria**: Auditoria
- **Prioridade**: Alta

**RNF23 - Rastreabilidade de Operações**
- Toda operação deve ter identificador único rastreável
- Deve ser possível auditar histórico completo de qualquer conta
- **Categoria**: Auditoria
- **Prioridade**: Alta

**RNF24 - Retenção de Logs**
- Logs devem ser retidos por no mínimo 5 anos
- Logs devem ser armazenados de forma imutável
- **Categoria**: Auditoria/Legal
- **Prioridade**: Média

**RNF25 - Monitoramento em Tempo Real**
- Sistema deve ter monitoramento de operações suspeitas
- Alertas automáticos para atividades anormais
- **Categoria**: Auditoria/Segurança
- **Prioridade**: Média

---

## 3. Regras de Negócio

### 3.1 Regras Financeiras

**RN01 - Saldo Mínimo**
- Conta corrente não tem saldo mínimo obrigatório
- Conta poupança deve manter saldo mínimo de R$ 1,00
- Conta com saldo zerado por mais de 90 dias pode ser encerrada automaticamente

**RN02 - Tarifas de Transferência**
- Transferência entre contas do AndreasBankV2: gratuita
- TED: R$ 8,50 por transferência
- DOC: R$ 5,00 por transferência
- Tarifas são debitadas da conta origem no momento da transferência

**RN03 - Horário de Funcionamento**
- Operações online disponíveis 24/7
- TED enviada após 17h em dia útil é processada no próximo dia útil
- DOC é sempre processada em D+1
- Finais de semana e feriados não contam como dias úteis

**RN04 - Limite de Transferência**
- Transferências entre contas próprias: sem limite
- Transferências para terceiros no mesmo banco: R$ 5.000,00 por transação
- TED/DOC: R$ 5.000,00 por transação
- Limite diário total: R$ 10.000,00
- Gerente pode autorizar valores superiores

**RN05 - Rendimento da Poupança**
- Conta poupança rende 0,5% ao mês + TR
- Rendimento calculado mensalmente no aniversário da conta
- Saque antes do aniversário não recebe rendimento do mês

**RN06 - Cheque Especial**
- Cheque especial disponível apenas para conta corrente
- Limite inicial: R$ 500,00
- Juros: 8,5% ao mês sobre saldo devedor
- Cliente deve ter conta há pelo menos 6 meses

**RN07 - Taxa de Manutenção**
- Conta corrente: R$ 12,00 por mês (isento com saldo médio > R$ 1.000,00)
- Conta poupança: gratuita
- Taxa debitada automaticamente no primeiro dia útil do mês

**RN08 - Valores Mínimos e Máximos**
- Depósito mínimo: R$ 1,00
- Saque mínimo: R$ 10,00
- Saque máximo: R$ 1.000,00 por operação
- Transferência mínima: R$ 0,01
- Transferência máxima: conforme RN04

**RN09 - Tempo de Compensação**
- Depósito em dinheiro: disponível imediatamente
- Depósito em cheque mesmo banco: 1 dia útil
- Depósito em cheque outro banco: 2 dias úteis
- Transferências internas: imediatas

**RN10 - Estorno de Transações**
- Cliente pode solicitar estorno em até 24 horas
- Análise de estorno leva até 5 dias úteis
- Estornos aprovados são processados em até 2 dias úteis
- TED/DOC só podem ser estornadas em caso de fraude comprovada

### 3.2 Regras de Conta

**RN11 - Idade Mínima**
- Conta corrente: 18 anos completos
- Conta poupança: 16 anos completos
- Menores de 18 anos precisam de responsável legal

**RN12 - Documentação Obrigatória**
- CPF válido e regular na Receita Federal
- Documento de identidade com foto (RG, CNH, RNE)
- Comprovante de residência com data inferior a 90 dias
- Email ativo e telefone celular

**RN13 - Limite de Contas por Cliente**
- Máximo de 1 conta corrente por cliente
- Máximo de 1 conta poupança por cliente
- Total máximo de 2 contas por cliente

**RN14 - Encerramento de Conta**
- Conta só pode ser encerrada com saldo zerado
- Conta com transações pendentes não pode ser encerrada
- Conta com empréstimos ativos não pode ser encerrada
- Encerramento definitivo após 30 dias da solicitação

**RN15 - Conta Inativa**
- Conta sem movimentação por 6 meses é considerada inativa
- Conta inativa por 12 meses pode ter taxa de manutenção dobrada
- Cliente deve ser notificado antes da inativação

### 3.3 Regras de Segurança

**RN16 - Política de Senhas**
- Senha deve ter mínimo 8 caracteres
- Deve conter letras maiúsculas, minúsculas e números
- Não pode conter sequências óbvias (123456, abcdef)
- Não pode ser igual ao CPF ou data de nascimento
- Deve ser alterada a cada 90 dias

**RN17 - Bloqueio de Conta**
- 3 tentativas incorretas de senha bloqueia o acesso
- Desbloqueio via email ou contato com gerente
- Movimentação suspeita pode resultar em bloqueio preventivo

**RN18 - Token de Autenticação**
- Token de sessão válido por 15 minutos de inatividade
- Token de recuperação de senha válido por 30 minutos
- Tokens são de uso único e não reutilizáveis

**RN19 - Detecção de Fraude**
- Múltiplas transferências em curto período disparam alerta
- Acesso de IP estrangeiro pela primeira vez requer confirmação
- Transferências acima de R$ 1.000,00 podem requerer confirmação adicional
- Operações fora do padrão do cliente são monitoradas

**RN20 - Privacidade de Dados**
- Cliente pode solicitar exportação de seus dados
- Cliente pode solicitar exclusão de dados (direito ao esquecimento)
- Dados financeiros devem ser mantidos por 5 anos (obrigação legal)
- Dados pessoais não podem ser compartilhados sem consentimento

---

## Matriz de Rastreabilidade

| Requisito | User Story | Prioridade | Épico |
|-----------|------------|------------|-------|
| RF01-RF05 | US01-US03 | Alta/Média | Cadastro de Clientes |
| RF06-RF10 | US04-US07 | Alta/Média | Gestão de Contas |
| RF11-RF13 | US08 | Alta | Operações Financeiras |
| RF14-RF16 | US09 | Alta | Operações Financeiras |
| RF17-RF21 | US12-US14 | Alta | Operações Financeiras |
| RF22-RF26 | US10-US11 | Alta/Média | Operações Financeiras |
| RF27-RF30 | US15-US17 | Alta | Segurança |

---

**Última Atualização**: Fevereiro de 2026  
**Versão**: 1.0  
**Status**: Documentação Completa
