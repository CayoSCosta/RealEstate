# Imobi – Configuração de Banco de Dados (PostgreSQL)

Este documento descreve a configuração do PostgreSQL utilizado pelo projeto **Imobi**, incluindo acesso local, remoto, aplicação ASP.NET Core e ferramentas de gerenciamento.

---

## 📌 Visão Geral

- **Servidor:** VPS Ubuntu
- **IP Público:** `187.111.186.35`
- **PostgreSQL:** 16
- **Porta:** `5432`
- **SSL:** Habilitado automaticamente

---

## 🗄️ Banco de Dados

- **Database:** `imobidb`
- **Owner:** `imobiuser`
- **Encoding:** UTF8
- **Locale:** `en_US.UTF-8`

---

## 👤 Usuários

### Usuário da aplicação (recomendado)
- **Username:** `imobiuser`
- **Password:** `imobi123`
- **Permissões:** ALL PRIVILEGES no banco `imobidb`
- **Uso:** API ASP.NET Core, DBeaver, psql remoto

### Usuário administrador
- **Username:** `postgres`
- **Acesso remoto:** ❌ Bloqueado
- **Uso:** Apenas manutenção local na VPS

---

## 🔌 Connection Strings

### ASP.NET Core (API rodando na VPS)
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=imobidb;Username=imobiuser;Password=imobi123"
}
