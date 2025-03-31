# 🛒 ProductsStore - API + Razor Frontend

Este proyecto contiene una **API ASP.NET Core** y un **Frontend en Razor Pages** que simulan una tienda de artículos con autenticación basada en JWT, gestión de artículos y comentarios.

---

## 📦 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [SQL Server Express / LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- Visual Studio 2022 o VS Code con C# Extensions

---

## 🚀 Cómo ejecutar el proyecto

### 🔧 1. Restaurar dependencias

Desde la raíz del proyecto:

```bash
dotnet restore
```

---

### 🗃 2. Aplicar las migraciones

El proyecto usa **Entity Framework Core con Identity**.

```bash
dotnet ef database update --project ProductsStore.Infraestructure
```

> Asegúrate de tener configurada tu cadena de conexión en `appsettings.json`.

---

### ▶️ 3. Ejecutar la API

```bash
dotnet run --project ProductsStoreApi
```

Esto levantará la API en `https://localhost:7149`

---

### 🌐 4. Ejecutar el frontend Razor Pages

Si tienes el proyecto Razor en la misma solución:

```bash
dotnet run --project ProductsStoreWeb
```

Esto abrirá una vista de login y luego permitirá navegar artículos, gestionar artículos y comentar.

---

## 🔐 Autenticación

- Registro e inicio de sesión via JWT
- Token se guarda en `localStorage`
- El token es utilizado por jQuery para realizar llamadas autenticadas

---

## 🧪 Pruebas

### Ejecutar pruebas unitarias:

```bash
dotnet test
```

Incluye pruebas para:

- Controladores (`ArticlesController`)
- Servicios (`AuthService`)
- Handlers de MediatR
- Repositorios con EF In-Memory

---

## 🧰 Funcionalidades principales

- Login / Registro con JWT
- CRUD de artículos (autenticado)
- Comentarios por artículo
- Gestión desde Razor Pages
- Autorización por rol (`Administrator`)
- Middleware de logs y errores
- Clean Architecture (Application, Domain, Infrastructure)

---

## 📂 Estructura del proyecto

```
/ProductsStoreApi         ← API con controladores y servicios
/ProductsStore.Application ← DTOs, Handlers, MediatR
/ProductsStore.Domain      ← Entidades y contratos
/ProductsStore.Infrastructure ← EF Core, Repositorios
/ProductsStoreWeb          ← Razor Pages (frontend)
```
