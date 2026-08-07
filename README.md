# Asisya Technical Test

## Descripción

Este proyecto corresponde al desarrollo de una solución Full Stack basada en ASP.NET Core y React para la gestión de productos y categorías.

La solución implementa autenticación mediante JWT, operaciones CRUD, generación masiva de productos, búsqueda, filtros, paginación y una interfaz web desarrollada con React.

---

# Tecnologías

## Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Swagger

## Frontend

- React
- TypeScript
- Vite
- Tailwind CSS
- Axios

---

# Arquitectura

El backend fue desarrollado siguiendo una arquitectura por capas.

```
Asisya.Api
│
├── Controllers
│
Asisya.Application
│
├── DTOs
├── Interfaces
├── Services
│
Asisya.Domain
│
├── Entities
├── Interfaces
│
Asisya.Infrastructure
│
├── Persistence
├── Repositories
├── DependencyInjection
```

### Responsabilidades

- **Api:** expone los endpoints REST.
- **Application:** contiene la lógica de negocio y los DTOs.
- **Domain:** define las entidades y contratos.
- **Infrastructure:** implementa acceso a datos y repositorios.

Esta separación facilita el mantenimiento, las pruebas y la escalabilidad del sistema.

---

# Funcionalidades

- Login mediante JWT.
- Protección de endpoints.
- CRUD de productos.
- CRUD de categorías.
- Generación masiva de productos.
- Búsqueda por nombre.
- Filtro por categoría.
- Paginación.
- Dashboard desarrollado en React.

---

# Escalabilidad

La solución puede escalar horizontalmente desplegando múltiples instancias de la API detrás de un balanceador de carga.

El uso de JWT permite que cualquier instancia valide el token sin mantener estado.

Para escenarios con alta carga, la generación masiva de productos podría migrarse a un sistema basado en colas como RabbitMQ, Kafka o Azure Service Bus, desacoplando el procesamiento de la petición HTTP.

También podría incorporarse Redis para cachear consultas frecuentes de productos y categorías.

---

# Requisitos

- .NET 8 SDK
- Node.js
- PostgreSQL

---

# Ejecución del Backend

```bash
cd src/Asisya.Api

dotnet restore

dotnet run
```

Swagger:

```
http://localhost:5103/swagger
```

---

# Ejecución del Frontend

```bash
cd frontend

npm install

npm run dev
```

Disponible en:

```
http://localhost:5173
```

---

# Usuario de prueba

Usuario

```
admin
```

Contraseña

```
Admin123*
```

---

# Autor

Harold Quiñones