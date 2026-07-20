# 02 - Entorno

Entorno de desarrollo y dependencias:

- .NET 9 SDK
- Visual Studio Code / Visual Studio / Rider
- Git

Variables y configuración local:

- `appsettings.json` contiene `Api:BaseUrl` y `Api:Endpoint`.
- Token de API debe establecerse con `dotnet user-secrets` bajo la clave `Api:AuthToken`.

Puertos y perfiles

- El proyecto utiliza puertos configurables en `Properties/launchSettings.json`. Por defecto la ejecución de ejemplo usa `http://localhost:5250`.
