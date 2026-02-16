# Modelo de Dados - AndreasBankV2

## Índice

- [1. Visão Geral](#1-visão-geral)
- [2. Diagrama Entidade-Relacionamento](#2-diagrama-entidade-relacionamento)
- [3. Entidades e Atributos](#3-entidades-e-atributos)
- [4. Relacionamentos](#4-relacionamentos)
- [5. Regras de Integridade](#5-regras-de-integridade)
- [6. Índices e Otimizações](#6-índices-e-otimizações)

---

## 1. Visão Geral

Este documento apresenta o **modelo conceitual de dados** do sistema AndreasBankV2. O modelo é agnóstico de tecnologia e pode ser implementado em diferentes SGBDs (MySQL, PostgreSQL, SQL Server, MongoDB, etc.).

### Princípios de Modelagem

- **Normalização**: Modelo segue a 3ª Forma Normal (3FN) para evitar redundância
- **Integridade Referencial**: Relacionamentos garantidos por chaves estrangeiras
- **Auditoria**: Campos de timestamp em todas as entidades principais
- **Rastreabilidade**: Todas as transações mantêm registro completo
- **Soft Delete**: Dados não são fisicamente excluídos, apenas marcados como inativos

### Entidades Principais

1. **Cliente** - Dados cadastrais das pessoas físicas
2. **Endereco** - Informações de endereço dos clientes
3. **Conta** - Contas bancárias (corrente e poupança)
4. **Transacao** - Registro de todas as movimentações financeiras
5. **Usuario** - Credenciais de acesso ao sistema
6. **AuditoriaLog** - Registro de todas as ações no sistema

---

## 2. Diagrama Entidade-Relacionamento

```mermaid
erDiagram
    CLIENTE ||--o{ CONTA : possui
    CLIENTE ||--o| ENDERECO : tem
    CLIENTE ||--|| USUARIO : acessa_com
    CONTA ||--o{ TRANSACAO : registra
    TRANSACAO }o--|| CONTA : origem
    TRANSACAO }o--o| CONTA : destino
    USUARIO ||--o{ AUDITORIA_LOG : gera
    CONTA ||--o{ AUDITORIA_LOG : relacionada_a

    CLIENTE {
        bigint id_cliente PK
        string cpf UK "NOT NULL, 11 dígitos"
        string nome_completo "NOT NULL"
        date data_nascimento "NOT NULL"
        string email UK "NOT NULL"
        string telefone "NOT NULL"
        string nome_mae
        decimal renda_mensal
        string status "Ativo, Inativo"
        timestamp data_cadastro
        timestamp data_atualizacao
    }

    ENDERECO {
        bigint id_endereco PK
        bigint id_cliente FK
        string cep
        string logradouro
        string numero
        string complemento
        string bairro
        string cidade
        string estado "UF, 2 caracteres"
        string pais "Default: Brasil"
        timestamp data_cadastro
        timestamp data_atualizacao
    }

    CONTA {
        bigint id_conta PK
        bigint id_cliente FK "NOT NULL"
        string numero_conta UK "NOT NULL, gerado automaticamente"
        string numero_agencia "NOT NULL"
        string tipo "Corrente, Poupança"
        string status "Ativa, Bloqueada, Inativa, Encerrada"
        decimal saldo_disponivel "Default: 0.00"
        decimal saldo_bloqueado "Default: 0.00"
        decimal limite_cheque_especial "Default: 0.00"
        date data_abertura
        date data_encerramento
        string motivo_encerramento
        timestamp data_cadastro
        timestamp data_atualizacao
    }

    TRANSACAO {
        bigint id_transacao PK
        bigint id_conta_origem FK
        bigint id_conta_destino FK "Nullable"
        string tipo "Depósito, Saque, Transferência, TED, DOC"
        string subtipo "Dinheiro, Cheque, Interna, etc"
        decimal valor "NOT NULL"
        decimal taxa "Default: 0.00"
        string status "Processada, Pendente, Cancelada, Estornada"
        string descricao
        string numero_autenticacao UK "Gerado automaticamente"
        string numero_cheque "Para depósitos em cheque"
        string banco_destino "Para TED/DOC"
        string agencia_destino "Para TED/DOC"
        string conta_destino "Para TED/DOC"
        string cpf_favorecido "Para TED/DOC"
        string nome_favorecido "Para TED/DOC"
        date data_processamento
        date data_compensacao "Para cheques"
        timestamp data_criacao
        timestamp data_atualizacao
    }

    USUARIO {
        bigint id_usuario PK
        bigint id_cliente FK UK "NOT NULL"
        string senha_hash "NOT NULL, bcrypt"
        string papel "Cliente, Gerente, Administrador"
        string status "Ativo, Bloqueado"
        int tentativas_login_falhas "Default: 0"
        timestamp ultimo_acesso
        timestamp data_bloqueio
        string token_recuperacao
        timestamp token_expiracao
        timestamp data_criacao
        timestamp data_atualizacao
        string historico_senhas "JSON array das últimas 3 senhas"
        date data_expiracao_senha "Senha expira a cada 90 dias"
    }

    AUDITORIA_LOG {
        bigint id_log PK
        bigint id_usuario FK
        bigint id_conta FK "Nullable"
        bigint id_transacao FK "Nullable"
        string acao "Login, Logout, Transferência, etc"
        string descricao
        string ip_origem
        string user_agent
        string dados_antes "JSON"
        string dados_depois "JSON"
        string resultado "Sucesso, Falha"
        timestamp data_hora
    }
```

---

## 3. Entidades e Atributos

### 3.1 CLIENTE

Armazena dados cadastrais dos clientes pessoas físicas.

| Atributo | Tipo | Restrições | Descrição |
|----------|------|------------|-----------|
| **id_cliente** | BIGINT | PK, AUTO_INCREMENT | Identificador único do cliente |
| **cpf** | VARCHAR(11) | NOT NULL, UNIQUE | CPF sem formatação (apenas dígitos) |
| **nome_completo** | VARCHAR(200) | NOT NULL | Nome completo do cliente |
| **data_nascimento** | DATE | NOT NULL | Data de nascimento (validar idade mínima) |
| **email** | VARCHAR(100) | NOT NULL, UNIQUE | Email do cliente |
| **telefone** | VARCHAR(15) | NOT NULL | Telefone com DDD (apenas dígitos) |
| **nome_mae** | VARCHAR(200) | NULL | Nome da mãe do cliente |
| **renda_mensal** | DECIMAL(12,2) | NULL | Renda mensal declarada |
| **status** | ENUM | NOT NULL, DEFAULT 'Ativo' | Status: Ativo, Inativo |
| **data_cadastro** | TIMESTAMP | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data de criação do registro |
| **data_atualizacao** | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP | Data da última atualização |

**Regras de Validação:**
- CPF deve ser válido (validar dígitos verificadores)
- Data de nascimento: cliente deve ter mínimo 16 anos
- Email deve ter formato válido
- Telefone deve ter 10 ou 11 dígitos

---

### 3.2 ENDERECO

Armazena endereço completo dos clientes.

| Atributo | Tipo | Restrições | Descrição |
|----------|------|------------|-----------|
| **id_endereco** | BIGINT | PK, AUTO_INCREMENT | Identificador único do endereço |
| **id_cliente** | BIGINT | FK → CLIENTE, NOT NULL, UNIQUE | Referência ao cliente (1:1) |
| **cep** | VARCHAR(8) | NULL | CEP sem formatação |
| **logradouro** | VARCHAR(200) | NULL | Rua, avenida, etc. |
| **numero** | VARCHAR(20) | NULL | Número do imóvel |
| **complemento** | VARCHAR(100) | NULL | Complemento (apto, bloco, etc.) |
| **bairro** | VARCHAR(100) | NULL | Bairro |
| **cidade** | VARCHAR(100) | NULL | Cidade |
| **estado** | VARCHAR(2) | NULL | UF (sigla do estado) |
| **pais** | VARCHAR(50) | DEFAULT 'Brasil' | País |
| **data_cadastro** | TIMESTAMP | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data de criação |
| **data_atualizacao** | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP | Data de atualização |

**Observações:**
- Endereço é opcional no cadastro inicial
- Um cliente pode ter apenas um endereço cadastrado
- Todos os campos são opcionais (NULL) exceto id_cliente

---

### 3.3 CONTA

Armazena informações das contas bancárias (corrente e poupança).

| Atributo | Tipo | Restrições | Descrição |
|----------|------|------------|-----------|
| **id_conta** | BIGINT | PK, AUTO_INCREMENT | Identificador único da conta |
| **id_cliente** | BIGINT | FK → CLIENTE, NOT NULL | Referência ao titular da conta |
| **numero_conta** | VARCHAR(20) | NOT NULL, UNIQUE | Número da conta (gerado automaticamente) |
| **numero_agencia** | VARCHAR(4) | NOT NULL, DEFAULT '0001' | Número da agência |
| **tipo** | ENUM | NOT NULL | Tipo: Corrente, Poupança |
| **status** | ENUM | NOT NULL, DEFAULT 'Ativa' | Status: Ativa, Bloqueada, Inativa, Encerrada |
| **saldo_disponivel** | DECIMAL(15,2) | NOT NULL, DEFAULT 0.00 | Saldo disponível para uso |
| **saldo_bloqueado** | DECIMAL(15,2) | NOT NULL, DEFAULT 0.00 | Saldo temporariamente bloqueado |
| **limite_cheque_especial** | DECIMAL(10,2) | DEFAULT 0.00 | Limite de cheque especial (apenas corrente) |
| **data_abertura** | DATE | NOT NULL, DEFAULT CURRENT_DATE | Data de abertura da conta |
| **data_encerramento** | DATE | NULL | Data de encerramento |
| **motivo_encerramento** | TEXT | NULL | Motivo do encerramento |
| **data_cadastro** | TIMESTAMP | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data de criação do registro |
| **data_atualizacao** | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP | Data de atualização |

**Regras de Negócio:**
- Cliente pode ter no máximo 1 conta corrente
- Cliente pode ter no máximo 1 conta poupança
- Número da conta formato: XXXXXXX-D (7 dígitos + dígito verificador)
- Cheque especial só para conta corrente
- Saldo total = saldo_disponivel + saldo_bloqueado

**Geração de Número de Conta:**
- Sequencial de 7 dígitos
- Dígito verificador calculado por algoritmo
- Exemplo: 0000001-5, 0000002-3, etc.

---

### 3.4 TRANSACAO

Registra todas as movimentações financeiras realizadas nas contas.

| Atributo | Tipo | Restrições | Descrição |
|----------|------|------------|-----------|
| **id_transacao** | BIGINT | PK, AUTO_INCREMENT | Identificador único da transação |
| **id_conta_origem** | BIGINT | FK → CONTA, NOT NULL | Conta de origem da transação |
| **id_conta_destino** | BIGINT | FK → CONTA, NULL | Conta de destino (se aplicável) |
| **tipo** | ENUM | NOT NULL | Depósito, Saque, Transferência, TED, DOC |
| **subtipo** | VARCHAR(50) | NULL | Dinheiro, Cheque, Interna, etc. |
| **valor** | DECIMAL(15,2) | NOT NULL, CHECK > 0 | Valor da transação |
| **taxa** | DECIMAL(10,2) | DEFAULT 0.00 | Taxa cobrada pela operação |
| **status** | ENUM | NOT NULL, DEFAULT 'Processada' | Processada, Pendente, Cancelada, Estornada |
| **descricao** | TEXT | NULL | Descrição detalhada da transação |
| **numero_autenticacao** | VARCHAR(50) | NOT NULL, UNIQUE | Número único de autenticação |
| **numero_cheque** | VARCHAR(20) | NULL | Número do cheque (para depósitos) |
| **banco_destino** | VARCHAR(10) | NULL | Código do banco destino (TED/DOC) |
| **agencia_destino** | VARCHAR(10) | NULL | Agência destino (TED/DOC) |
| **conta_destino** | VARCHAR(20) | NULL | Conta destino (TED/DOC) |
| **cpf_favorecido** | VARCHAR(11) | NULL | CPF do favorecido (TED/DOC) |
| **nome_favorecido** | VARCHAR(200) | NULL | Nome do favorecido (TED/DOC) |
| **data_processamento** | DATE | NOT NULL | Data de processamento |
| **data_compensacao** | DATE | NULL | Data de compensação (cheques) |
| **data_criacao** | TIMESTAMP | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data de criação |
| **data_atualizacao** | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP | Data de atualização |

**Tipos de Transação:**

1. **Depósito**
   - id_conta_destino = id_conta_origem
   - subtipo: Dinheiro, Cheque
   - taxa = 0.00

2. **Saque**
   - id_conta_destino = NULL
   - taxa = 0.00

3. **Transferência**
   - id_conta_origem e id_conta_destino preenchidos (ambas do AndreasBankV2)
   - taxa = 0.00

4. **TED**
   - id_conta_destino = NULL
   - Campos de banco_destino, agencia_destino, conta_destino, cpf_favorecido preenchidos
   - taxa = 8.50

5. **DOC**
   - id_conta_destino = NULL
   - Mesmos campos que TED
   - taxa = 5.00

**Número de Autenticação:**
- Formato: AAAAMMDDHHMMSS + 6 dígitos aleatórios
- Exemplo: 20260215143025ABC123

---

### 3.5 USUARIO

Armazena credenciais e dados de acesso ao sistema.

| Atributo | Tipo | Restrições | Descrição |
|----------|------|------------|-----------|
| **id_usuario** | BIGINT | PK, AUTO_INCREMENT | Identificador único do usuário |
| **id_cliente** | BIGINT | FK → CLIENTE, NOT NULL, UNIQUE | Referência ao cliente (1:1) |
| **senha_hash** | VARCHAR(255) | NOT NULL | Hash da senha (bcrypt) |
| **papel** | ENUM | NOT NULL, DEFAULT 'Cliente' | Cliente, Gerente, Administrador |
| **status** | ENUM | NOT NULL, DEFAULT 'Ativo' | Ativo, Bloqueado |
| **tentativas_login_falhas** | INT | DEFAULT 0 | Contador de tentativas incorretas |
| **ultimo_acesso** | TIMESTAMP | NULL | Data/hora do último login |
| **data_bloqueio** | TIMESTAMP | NULL | Data/hora do bloqueio |
| **token_recuperacao** | VARCHAR(255) | NULL | Token para recuperação de senha |
| **token_expiracao** | TIMESTAMP | NULL | Expiração do token (30 minutos) |
| **data_criacao** | TIMESTAMP | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data de criação |
| **data_atualizacao** | TIMESTAMP | ON UPDATE CURRENT_TIMESTAMP | Data de atualização |
| **historico_senhas** | TEXT | NULL | JSON array das últimas 3 senhas (hashes) |
| **data_expiracao_senha** | DATE | NULL | Data para obrigar alteração (90 dias) |

**Regras de Segurança:**
- Senha armazenada usando bcrypt (custo mínimo: 12)
- Bloqueia após 3 tentativas_login_falhas
- Token de recuperação expira em 30 minutos
- Senhas expiram a cada 90 dias
- Não reutilizar últimas 3 senhas (verificar historico_senhas)

**Papéis (RBAC):**
- **Cliente**: Acesso apenas às próprias contas
- **Gerente**: Acesso a dados de clientes + aprovações
- **Administrador**: Acesso total ao sistema

---

### 3.6 AUDITORIA_LOG

Registra todas as ações realizadas no sistema para fins de auditoria.

| Atributo | Tipo | Restrições | Descrição |
|----------|------|------------|-----------|
| **id_log** | BIGINT | PK, AUTO_INCREMENT | Identificador único do log |
| **id_usuario** | BIGINT | FK → USUARIO, NULL | Usuário que executou a ação |
| **id_conta** | BIGINT | FK → CONTA, NULL | Conta relacionada (se aplicável) |
| **id_transacao** | BIGINT | FK → TRANSACAO, NULL | Transação relacionada (se aplicável) |
| **acao** | VARCHAR(100) | NOT NULL | Tipo de ação executada |
| **descricao** | TEXT | NULL | Descrição detalhada da ação |
| **ip_origem** | VARCHAR(45) | NULL | IP de origem (suporta IPv6) |
| **user_agent** | TEXT | NULL | Navegador/dispositivo utilizado |
| **dados_antes** | TEXT | NULL | Estado anterior (JSON) |
| **dados_depois** | TEXT | NULL | Estado posterior (JSON) |
| **resultado** | ENUM | NOT NULL | Sucesso, Falha |
| **data_hora** | TIMESTAMP | NOT NULL, DEFAULT CURRENT_TIMESTAMP | Data/hora da ação |

**Tipos de Ação Auditadas:**
- Login, Logout, LoginFalhado
- CadastroCliente, AtualizacaoCliente
- AberturaConta, EncerramentoConta
- Deposito, Saque, Transferencia, TED, DOC
- AlteracaoSenha, RecuperacaoSenha
- BloqueioUsuario, DesbloqueioUsuario

**Retenção:**
- Logs devem ser mantidos por no mínimo 5 anos
- Logs são imutáveis (apenas INSERT, sem UPDATE/DELETE)

---

## 4. Relacionamentos

### 4.1 CLIENTE ↔ ENDERECO (1:1)

```
CLIENTE (1) ─────────── (0..1) ENDERECO
```

- Um cliente pode ter zero ou um endereço
- Um endereço pertence a exatamente um cliente
- **Chave Estrangeira**: ENDERECO.id_cliente → CLIENTE.id_cliente
- **Deleção**: ON DELETE CASCADE (se cliente é deletado, endereço também)

---

### 4.2 CLIENTE ↔ CONTA (1:N)

```
CLIENTE (1) ─────────── (0..2) CONTA
```

- Um cliente pode ter zero, uma ou duas contas (máx: 1 corrente + 1 poupança)
- Uma conta pertence a exatamente um cliente
- **Chave Estrangeira**: CONTA.id_cliente → CLIENTE.id_cliente
- **Deleção**: ON DELETE RESTRICT (não permite deletar cliente com contas ativas)

---

### 4.3 CLIENTE ↔ USUARIO (1:1)

```
CLIENTE (1) ─────────── (1) USUARIO
```

- Um cliente tem exatamente um usuário para acesso
- Um usuário pertence a exatamente um cliente
- **Chave Estrangeira**: USUARIO.id_cliente → CLIENTE.id_cliente (UNIQUE)
- **Deleção**: ON DELETE CASCADE

---

### 4.4 CONTA ↔ TRANSACAO (1:N)

```
CONTA (1) ─────────── (0..N) TRANSACAO
        └─────────── (0..N) TRANSACAO (destino)
```

- Uma conta pode ter zero ou muitas transações
- Uma transação tem uma conta de origem obrigatória
- Uma transação pode ter uma conta de destino opcional
- **Chaves Estrangeiras**:
  - TRANSACAO.id_conta_origem → CONTA.id_conta
  - TRANSACAO.id_conta_destino → CONTA.id_conta (NULLABLE)
- **Deleção**: ON DELETE RESTRICT (não permite deletar conta com transações)

---

### 4.5 USUARIO ↔ AUDITORIA_LOG (1:N)

```
USUARIO (1) ─────────── (0..N) AUDITORIA_LOG
```

- Um usuário pode gerar zero ou muitos logs
- Um log pertence a zero ou um usuário (pode ser ação do sistema)
- **Chave Estrangeira**: AUDITORIA_LOG.id_usuario → USUARIO.id_usuario (NULLABLE)
- **Deleção**: ON DELETE SET NULL

---

## 5. Regras de Integridade

### 5.1 Integridade de Domínio

**CLIENTE**
- `cpf` deve ter exatamente 11 dígitos numéricos
- `email` deve seguir formato válido de email
- `telefone` deve ter 10 ou 11 dígitos
- `data_nascimento` deve resultar em idade ≥ 16 anos

**CONTA**
- `numero_conta` deve ser único no sistema
- `tipo = 'Corrente'` → cliente pode ter no máximo 1
- `tipo = 'Poupança'` → cliente pode ter no máximo 1
- `saldo_disponivel + saldo_bloqueado ≥ 0` (não negativar, exceto com cheque especial)
- `limite_cheque_especial > 0` somente se `tipo = 'Corrente'`

**TRANSACAO**
- `valor > 0` checkconstraint
- `taxa ≥ 0`
- Se `tipo = 'Transferência'` então `id_conta_destino NOT NULL`
- Se `tipo IN ('TED', 'DOC')` então campos de favorecido devem estar preenchidos

**USUARIO**
- `senha_hash` deve ter comprimento mínimo (bcrypt gera ~60 caracteres)
- `tentativas_login_falhas` entre 0 e 3
- Se `status = 'Bloqueado'` então `data_bloqueio NOT NULL`

### 5.2 Integridade Referencial

Todas as chaves estrangeiras devem ser garantidas pelo banco de dados:

```sql
-- Exemplo de constraints
ALTER TABLE ENDERECO
  ADD CONSTRAINT fk_endereco_cliente 
  FOREIGN KEY (id_cliente) REFERENCES CLIENTE(id_cliente) 
  ON DELETE CASCADE;

ALTER TABLE CONTA
  ADD CONSTRAINT fk_conta_cliente 
  FOREIGN KEY (id_cliente) REFERENCES CLIENTE(id_cliente) 
  ON DELETE RESTRICT;

ALTER TABLE TRANSACAO
  ADD CONSTRAINT fk_transacao_conta_origem 
  FOREIGN KEY (id_conta_origem) REFERENCES CONTA(id_conta) 
  ON DELETE RESTRICT;

ALTER TABLE TRANSACAO
  ADD CONSTRAINT fk_transacao_conta_destino 
  FOREIGN KEY (id_conta_destino) REFERENCES CONTA(id_conta) 
  ON DELETE RESTRICT;

ALTER TABLE USUARIO
  ADD CONSTRAINT fk_usuario_cliente 
  FOREIGN KEY (id_cliente) REFERENCES CLIENTE(id_cliente) 
  ON DELETE CASCADE;
```

### 5.3 Triggers e Validações

**Trigger: Validar Limite de Contas**
```sql
-- Antes de inserir uma nova conta, validar se cliente já tem conta do mesmo tipo
BEFORE INSERT ON CONTA
  IF (SELECT COUNT(*) FROM CONTA 
      WHERE id_cliente = NEW.id_cliente AND tipo = NEW.tipo) >= 1
  THEN RAISE_ERROR('Cliente já possui conta deste tipo');
```

**Trigger: Atualizar Saldo**
```sql
-- Após inserir transação processada, atualizar saldos
AFTER INSERT ON TRANSACAO
  IF NEW.status = 'Processada' THEN
    -- Debitar conta origem
    UPDATE CONTA SET saldo_disponivel = saldo_disponivel - (NEW.valor + NEW.taxa)
    WHERE id_conta = NEW.id_conta_origem;
    
    -- Creditar conta destino (se houver)
    IF NEW.id_conta_destino IS NOT NULL THEN
      UPDATE CONTA SET saldo_disponivel = saldo_disponivel + NEW.valor
      WHERE id_conta = NEW.id_conta_destino;
    END IF;
  END IF;
```

**Trigger: Log de Auditoria Automático**
```sql
-- Após qualquer operação em tabelas críticas, criar log
AFTER INSERT/UPDATE/DELETE ON CONTA, TRANSACAO, USUARIO
  INSERT INTO AUDITORIA_LOG (...) VALUES (...);
```

---

## 6. Índices e Otimizações

### 6.1 Índices Recomendados

**CLIENTE**
```sql
CREATE UNIQUE INDEX idx_cliente_cpf ON CLIENTE(cpf);
CREATE UNIQUE INDEX idx_cliente_email ON CLIENTE(email);
CREATE INDEX idx_cliente_status ON CLIENTE(status);
CREATE INDEX idx_cliente_data_cadastro ON CLIENTE(data_cadastro);
```

**CONTA**
```sql
CREATE UNIQUE INDEX idx_conta_numero ON CONTA(numero_conta);
CREATE INDEX idx_conta_cliente ON CONTA(id_cliente);
CREATE INDEX idx_conta_status ON CONTA(status);
CREATE INDEX idx_conta_tipo ON CONTA(tipo);
CREATE INDEX idx_conta_cliente_tipo ON CONTA(id_cliente, tipo); -- Composto
```

**TRANSACAO**
```sql
CREATE UNIQUE INDEX idx_transacao_autenticacao ON TRANSACAO(numero_autenticacao);
CREATE INDEX idx_transacao_conta_origem ON TRANSACAO(id_conta_origem);
CREATE INDEX idx_transacao_conta_destino ON TRANSACAO(id_conta_destino);
CREATE INDEX idx_transacao_status ON TRANSACAO(status);
CREATE INDEX idx_transacao_data ON TRANSACAO(data_processamento);
CREATE INDEX idx_transacao_tipo ON TRANSACAO(tipo);
-- Índice composto para consultas de extrato
CREATE INDEX idx_transacao_conta_data ON TRANSACAO(id_conta_origem, data_processamento DESC);
```

**USUARIO**
```sql
CREATE UNIQUE INDEX idx_usuario_cliente ON USUARIO(id_cliente);
CREATE INDEX idx_usuario_status ON USUARIO(status);
```

**AUDITORIA_LOG**
```sql
CREATE INDEX idx_log_usuario ON AUDITORIA_LOG(id_usuario);
CREATE INDEX idx_log_acao ON AUDITORIA_LOG(acao);
CREATE INDEX idx_log_data ON AUDITORIA_LOG(data_hora DESC);
CREATE INDEX idx_log_conta ON AUDITORIA_LOG(id_conta);
```

### 6.2 Particionamento

Para grandes volumes de dados, considerar:

**Tabela TRANSACAO**
- Particionamento por data (mensal ou anual)
- Exemplo: `PARTITION BY RANGE (YEAR(data_processamento))`

**Tabela AUDITORIA_LOG**
- Particionamento por data (mensal)
- Arquivamento de partições antigas

### 6.3 Estratégias de Cache

**Consultas Frequentes:**
- Saldo de conta: cache de 30 segundos
- Dados do cliente: cache de 5 minutos
- Dados de conta: cache de 2 minutos

**Dados Estáticos:**
- Lista de bancos (para TED/DOC): cache indefinido
- Tarifas e taxas: cache de 1 hora

---

## Considerações de Implementação

### Segurança
- Criptografar dados sensíveis em repouso (CPF, números de conta)
- Usar conexões SSL/TLS para banco de dados
- Implementar row-level security para isolamento de dados

### Backup
- Backup completo diário
- Backup incremental a cada 6 horas
- Retenção de 90 dias
- Teste de restauração mensal

### Escalabilidade
- Preparado para sharding por id_cliente (distribuir clientes entre servidores)
- Read replicas para consultas (separar leitura/escrita)
- Connection pooling configurado adequadamente

### Compliance
- Dados mantidos por 5 anos conforme legislação bancária
- Implementar direito ao esquecimento (LGPD) com anonimização
- Logs de acesso imutáveis para auditoria

---

**Última Atualização**: Fevereiro de 2026  
**Versão**: 1.0  
**Status**: Modelo Conceitual Completo
