# TP-Videojuegos-Minimal-APIs-NET10

## De qué trata
API REST desarrollada con Minimal APIs de .NET 10 para gestionar una biblioteca de préstamos de videojuegos. Permite administrar videojuegos, copias físicas y préstamos, con autenticación de usuarios mediante JWT.

Funcionalidades principales:
- CRUD de Videojuegos, Copias y Préstamos.
- Búsqueda paginada de videojuegos.
- Registro y login de usuarios (Identity + JWT).
- Endpoints protegidos con autenticación.
- Documentación interactiva con Swagger UI.
- Endpoint de `/health` para verificar el estado de la API.

Trabajo práctico para mostrar los conocimientos aprendidos durante el bootcamp de CodigoFacilito - Minimal APIs .NET 10.

## Cómo correrlo

### Requisitos previos
- .NET 10 SDK
- Visual Studio 2022/2026 (o VS Code)

### Pasos
1. Cloná el repositorio:
   ```bash
   git clone https://github.com/Daianaeds/TP-VideoJuegos-Minimal-APIs-NET10.git
   ```
2. Abrí la solución `BibliotecaVideojuegosApi.slnx` en Visual Studio.
3. Ejecutá el proyecto **BibliotecaVideojuegosApi** (F5 o Ctrl+F5), seleccionando el perfil `https`.
   - La primera vez se aplican las migraciones automáticamente y se crea la base de datos con datos de ejemplo.
4. Una vez levantada la API, podés:
   - Ver la documentación en Swagger UI: `https://localhost:7167/swagger`
   - Verificar el estado de la API: `https://localhost:7167/health`
   - Probar los endpoints con el archivo `BibliotecaVideojuegosApi.http` incluido en el proyecto.

### Probar la API (flujo básico)
1. Registrar un usuario: `POST /auth/registro`
2. Iniciar sesión: `POST /auth/login` (devuelve un token JWT)
3. Usar el token en el header `Authorization: Bearer {token}` para acceder a los endpoints protegidos (crear videojuegos, copias, préstamos).
