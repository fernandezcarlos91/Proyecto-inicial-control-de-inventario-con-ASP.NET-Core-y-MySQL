# 📦 Inventario .NET
 
Sistema de control de inventario construido con **ASP.NET Core MVC** y
**Entity Framework Core**, con una API REST paralela documentada con Swagger.
Proyecto de práctica para migrar mis conocimientos de Flask a .NET.
 
## Funcionalidades
 
- CRUD completo de productos (crear, listar, editar, eliminar)
- Filtro por categoría y búsqueda por nombre
- API REST (`/api/productosapi`, `/api/categoriasapi`)
- Endpoint de resumen con estadísticas del inventario
- Documentación interactiva con Swagger (`/swagger`)
- Panel de inicio con indicadores clave
 
## Stack técnico
 
- ASP.NET Core 8 (MVC + Web API)
- Entity Framework Core + Pomelo (MySQL)
- Bootstrap 5 + Bootstrap Icons
- MySQL / phpMyAdmin (vía XAMPP en desarrollo local)
 
## Cómo ejecutarlo localmente
 
1. Clona el repositorio y entra a la carpeta `InventarioWeb`
2. Configura tu cadena de conexión con User Secrets:
   ```
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:InventarioDb" "tu-cadena-de-conexion"
   ```
3. Aplica las migraciones: `dotnet ef database update`
4. Ejecuta: `dotnet watch run`
5. Abre `http://localhost:5015`
 

## Autor
 
Carlos Fernández
