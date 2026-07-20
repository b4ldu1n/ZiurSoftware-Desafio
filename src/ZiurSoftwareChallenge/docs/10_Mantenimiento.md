# 10 - Mantenimiento

Recomendaciones para mantener el proyecto en buen estado:

- Mantener `dotnet` actualizado (versión objetivo: .NET 9 SDK).
- Conservar tokens y secretos fuera del repositorio usando `dotnet user-secrets` o un vault (Azure Key Vault, HashiCorp Vault).
- Mantener `appsettings.json` con valores por defecto y usar `appsettings.Development.json` para ajustes locales no sensibles.
- Agregar tests unitarios para `ApiMovimientoService` simulando respuestas JSON (raíz array y wrapper). Esto previene regresiones.
- Añadir CI que ejecute `dotnet build` y (si agrega tests) `dotnet test` en cada PR.

Prácticas de logging y monitoreo:

- Registrar eventos clave con `ILogger` (inicio de petición, errores deserialización, conteo de elementos).
- Evitar logs que expongan tokens o datos sensibles.

Actualizaciones y despliegue:

- Antes de desplegar, asegúrate de que `Api:BaseUrl` apunte al entorno correcto (staging/production) y que las credenciales en el entorno estén configuradas.
