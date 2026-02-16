# Glossário - AndreasBankV3

## Índice

- [Termos Bancários](#termos-bancários)
- [Tipos de Conta](#tipos-de-conta)
- [Operações Financeiras](#operações-financeiras)
- [Termos Técnicos](#termos-técnicos)
- [Personas do Sistema](#personas-do-sistema)
- [Status e Estados](#status-e-estados)
- [Abreviações](#abreviações)
- [Regras e Políticas](#regras-e-políticas)

---

## Termos Bancários

### Agência
Número identificador da unidade ou ponto de atendimento do banco. No AndreasBankV3, cada cliente é associado a uma agência ao abrir uma conta. Formato típico: 4 dígitos (exemplo: 0001).

### Cheque Especial
Limite de crédito pré-aprovado disponível em conta corrente, permitindo que o cliente utilize valores além do saldo disponível. Taxa de juros aplicada sobre o valor utilizado.

### Compensação
Processo de verificação e aprovação de um depósito em cheque. Durante a compensação, o banco confirma se há fundos na conta de origem do cheque.

### Comprovante
Documento que registra e comprova a realização de uma transação bancária, contendo informações como data, hora, valor, tipo de operação e número de autenticação único.

### CPF (Cadastro de Pessoas Físicas)
Documento de identificação do cidadão perante a Receita Federal brasileira. Utilizado como identificador único de clientes no sistema.

### Conta Bancária
Registro individual que armazena o saldo e histórico de transações de um cliente. Identificada por número de conta, agência e tipo.

### Extrato Bancário
Relatório detalhado listando todas as transações (depósitos, saques, transferências) realizadas em uma conta durante um período específico.

### Saldo
Valor total de dinheiro disponível em uma conta bancária em determinado momento.

### Saldo Bloqueado
Parte do saldo total que não está disponível para uso imediato, geralmente devido a transações pendentes ou restrições temporárias.

### Saldo Disponível
Valor que o cliente pode efetivamente utilizar para saques, transferências e outras operações. Calculado como: (Saldo Total - Saldo Bloqueado + Limite Disponível).

### Taxa Bancária
Valor cobrado pelo banco pela realização de determinados serviços, como transferências TED/DOC ou manutenção de conta.

### Tarifa de Manutenção
Taxa mensal cobrada para manter a conta ativa e oferecer serviços bancários.

---

## Tipos de Conta

### Conta Corrente
Tipo de conta bancária destinada a movimentações financeiras do dia a dia. Características:
- Permite saques, depósitos, transferências ilimitadas
- Pode ter cheque especial
- Sujeita a tarifa de manutenção mensal (R$ 12,00)
- Idade mínima: 18 anos
- Limite: 1 conta corrente por cliente

### Conta Poupança
Tipo de conta bancária destinada à poupança e investimento de médio prazo. Características:
- Rende juros mensais (0,5% + TR)
- Sem tarifa de manutenção
- Saldo mínimo: R$ 1,00
- Rendimento creditado no aniversário mensal da conta
- Idade mínima: 16 anos
- Limite: 1 conta poupança por cliente

---

## Operações Financeiras

### Depósito
Operação de adicionar dinheiro a uma conta bancária. Tipos:
- **Depósito em Dinheiro**: Crédito imediato
- **Depósito em Cheque**: Sujeito a compensação

### Saque
Operação de retirada de dinheiro de uma conta bancária. Limites:
- Mínimo: R$ 10,00
- Máximo por operação: R$ 1.000,00
- Máximo diário: R$ 1.000,00

### Transferência
Operação de movimentação de valores entre contas. Tipos:
- **Transferência Interna**: Entre contas do AndreasBankV3
- **TED**: Transferência Eletrônica Disponível para outros bancos
- **DOC**: Documento de Ordem de Crédito para outros bancos

### TED (Transferência Eletrônica Disponível)
Modalidade de transferência entre bancos diferentes, processada no mesmo dia útil se realizada até 17h. Características:
- Taxa: R$ 8,50
- Limite: R$ 5.000,00 por transação
- Processamento: D+0 (até 17h) ou D+1 (após 17h)

### DOC (Documento de Ordem de Crédito)
Modalidade de transferência entre bancos diferentes, sempre processada no próximo dia útil. Características:
- Taxa: R$ 5,00
- Limite: R$ 5.000,00 por transação
- Processamento: sempre D+1

### Estorno
Reversão de uma transação já realizada, devolvendo o valor à conta de origem. Prazos:
- Solicitação: até 24 horas após a transação
- Análise: até 5 dias úteis
- Processamento: até 2 dias úteis após aprovação

---

## Termos Técnicos

### Autenticação
Processo de validação da identidade de um usuário através de credenciais (CPF e senha).

### Autorização
Processo de verificação se um usuário autenticado tem permissão para realizar determinada ação.

### BDD (Behavior-Driven Development)
Metodologia de desenvolvimento orientada a comportamento, que utiliza linguagem natural (Gherkin) para descrever cenários de teste.

### CRUD
Acrônimo para Create (Criar), Read (Ler), Update (Atualizar), Delete (Deletar). Operações básicas de manipulação de dados.

### Gherkin
Linguagem de especificação de comportamento utilizada em BDD, com palavras-chave como Dado, Quando, Então.

### Hash
Algoritmo de criptografia unidirecional utilizado para armazenar senhas de forma segura.

### LGPD (Lei Geral de Proteção de Dados)
Lei brasileira que regula o tratamento de dados pessoais, garantindo direitos como privacidade e proteção de dados.

### RBAC (Role-Based Access Control)
Controle de acesso baseado em papéis/perfis de usuário.

### Sessão
Período de tempo em que um usuário está autenticado no sistema. No AndreasBankV3, sessões expiram após 15 minutos de inatividade.

### Token
Código único e temporário gerado para autenticação ou autorização de operações específicas.

### WCAG (Web Content Accessibility Guidelines)
Diretrizes internacionais de acessibilidade para conteúdo web.

---

## Personas do Sistema

### Cliente
Pessoa física cadastrada no sistema que utiliza os serviços bancários. Permissões:
- Gerenciar seus próprios dados cadastrais
- Abrir e encerrar suas contas
- Realizar todas as operações financeiras em suas contas
- Consultar saldos e extratos

### Gerente
Funcionário do banco responsável por aprovações e operações especiais. Permissões:
- Todas as permissões de Cliente
- Consultar dados de qualquer cliente
- Aprovar operações acima de limites padrão
- Inativar cadastros de clientes
- Autorizar saques acima do limite
- Desbloquear contas

### Administrador
Responsável técnico pela gestão e configuração do sistema. Permissões:
- Todas as permissões de Gerente
- Configurar parâmetros do sistema
- Gerenciar usuários e permissões
- Acessar logs de auditoria
- Configurar limites e tarifas

---

## Status e Estados

### Status de Conta

**Ativa**
- Conta em pleno funcionamento
- Permite todas as operações normais

**Bloqueada**
- Conta temporariamente impedida de realizar operações
- Motivos: suspeita de fraude, ordem judicial, inadimplência
- Requer ação do gerente para desbloquear

**Inativa**
- Conta sem movimentação por 6 meses
- Pode ter tarifa de manutenção elevada após 12 meses
- Cliente é notificado antes da inativação

**Encerrada**
- Conta fechada por solicitação do cliente
- Não permite nenhuma operação
- Histórico mantido por 5 anos

### Status de Transação

**Processada**
- Transação concluída com sucesso
- Valores já creditados/debitados

**Pendente**
- Transação aguardando processamento
- Exemplo: cheque em compensação, DOC agendada

**Cancelada**
- Transação cancelada antes do processamento
- Valores não foram movimentados

**Estornada**
- Transação revertida após processamento
- Valores devolvidos à origem

### Status de Cadastro

**Ativo**
- Cliente pode utilizar normalmente o sistema

**Inativo**
- Cliente não pode realizar operações
- Pode reativar mediante solicitação

**Bloqueado Temporariamente**
- Acesso bloqueado por tentativas incorretas de login
- Requer desbloqueio via email ou gerente

---

## Abreviações

| Abreviação | Significado |
|------------|-------------|
| ACID | Atomicity, Consistency, Isolation, Durability |
| BDD | Behavior-Driven Development |
| CPF | Cadastro de Pessoas Físicas |
| CSRF | Cross-Site Request Forgery |
| D+0 | Mesmo dia |
| D+1 | Próximo dia útil |
| DOC | Documento de Ordem de Crédito |
| ERD | Entity-Relationship Diagram |
| HTTPS | HyperText Transfer Protocol Secure |
| LGPD | Lei Geral de Proteção de Dados |
| PF | Pessoa Física |
| PJ | Pessoa Jurídica |
| RBAC | Role-Based Access Control |
| RF | Requisito Funcional |
| RN | Regra de Negócio |
| RNF | Requisito Não-Funcional |
| SLA | Service Level Agreement |
| SQL | Structured Query Language |
| TED | Transferência Eletrônica Disponível |
| TLS | Transport Layer Security |
| TR | Taxa Referencial |
| URL | Uniform Resource Locator |
| US | User Story |
| WCAG | Web Content Accessibility Guidelines |
| XSS | Cross-Site Scripting |

---

## Regras e Políticas

### Política de Senhas

Requisitos obrigatórios para senhas no AndreasBankV3:
- **Comprimento mínimo**: 8 caracteres
- **Composição**: Letras maiúsculas, minúsculas e números
- **Restrições**: Não pode conter sequências óbvias (123456, abcdef)
- **Vedações**: Não pode ser igual a CPF ou data de nascimento
- **Histórico**: Não pode reutilizar as últimas 3 senhas
- **Validade**: Deve ser alterada a cada 90 dias

### Política de Bloqueio

Regras de bloqueio de conta por tentativas incorretas:
- **Limite**: 3 tentativas consecutivas incorretas
- **Ação**: Bloqueio temporário imediato
- **Notificação**: Email enviado ao cliente
- **Desbloqueio**: Via email ou contato com gerente

### Política de Privacidade

Direitos do cliente conforme LGPD:
- **Acesso**: Visualizar todos os dados armazenados
- **Retificação**: Corrigir dados incorretos
- **Portabilidade**: Exportar dados em formato estruturado
- **Exclusão**: Exercer direito ao esquecimento (respeitando obrigações legais)
- **Consentimento**: Controlar uso de dados para finalidades específicas
- **Retenção**: Dados financeiros mantidos por 5 anos

### Horários de Funcionamento

**Sistema Online**
- Disponível: 24 horas por dia, 7 dias por semana
- Manutenções programadas: comunicadas com 48h de antecedência

**Processamento de Transferências**
- **TED**: D+0 se enviada até 17h em dia útil, caso contrário D+1
- **DOC**: Sempre D+1
- **Transferências Internas**: Instantâneas (24/7)

**Dias Úteis**
- Segunda a sexta-feira, exceto feriados nacionais
- Finais de semana não contam para processamento

### Limites Operacionais

| Operação | Limite Mínimo | Limite Máximo | Observações |
|----------|---------------|---------------|-------------|
| Depósito | R$ 1,00 | Sem limite | - |
| Saque | R$ 10,00 | R$ 1.000,00/operação | Limite diário: R$ 1.000,00 |
| Transferência Interna (próprias contas) | R$ 0,01 | Sem limite | Sem taxa |
| Transferência Interna (terceiros) | R$ 0,01 | R$ 5.000,00/transação | Sem taxa |
| TED | R$ 0,01 | R$ 5.000,00/transação | Taxa: R$ 8,50 |
| DOC | R$ 0,01 | R$ 5.000,00/transação | Taxa: R$ 5,00 |
| Limite Diário Total | - | R$ 10.000,00 | Soma de todas as transferências |

### Tarifas e Taxas

| Serviço | Valor | Condições |
|---------|-------|-----------|
| Manutenção Conta Corrente | R$ 12,00/mês | Isento com saldo médio > R$ 1.000,00 |
| Manutenção Conta Poupança | Gratuito | - |
| Transferência Interna | Gratuito | - |
| TED | R$ 8,50 | Por transferência |
| DOC | R$ 5,00 | Por transferência |
| 2ª via de Comprovante | R$ 2,00 | Apenas para comprovantes antigos (>90 dias) |

### Rendimentos

**Conta Poupança**
- **Taxa**: 0,5% ao mês + TR (Taxa Referencial)
- **Periodicidade**: Mensal
- **Aniversário**: Data de abertura da conta
- **Regra**: Saque antes do aniversário perde rendimento do mês

**Cheque Especial (Conta Corrente)**
- **Juros**: 8,5% ao mês sobre saldo devedor
- **Limite Inicial**: R$ 500,00
- **Requisito**: Conta com no mínimo 6 meses de existência
- **Cobrança**: Proporcional ao período e valor utilizado

---

## Convenções de Documentação

### Identificadores

**Requisitos**
- RF01-RF30: Requisitos Funcionais
- RNF01-RNF25: Requisitos Não-Funcionais
- RN01-RN20: Regras de Negócio

**User Stories**
- US01-US17: User Stories numeradas sequencialmente

**Prioridades**
- **Alta**: Crítico para funcionamento básico do sistema
- **Média**: Importante mas não crítico
- **Baixa**: Desejável, pode ser implementado posteriormente

### Valores Monetários

Todos os valores monetários são expressos em Real (R$) com duas casas decimais.

**Formato padrão**: R$ 1.234,56

### Datas e Horários

**Formato de Data**: DD/MM/AAAA (exemplo: 15/02/2026)  
**Formato de Hora**: HH:MM (exemplo: 14:30)  
**Fuso Horário**: Horário de Brasília (BRT/BRST)

---

**Última Atualização**: Fevereiro de 2026  
**Versão**: 1.0  
**Total de Termos Definidos**: 60+
