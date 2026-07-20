# 00 - Environment Setup

Requisitos mínimos

- .NET 9 SDK instalado (https://dotnet.microsoft.com)
- Git
- Editor (Visual Studio Code, Visual Studio, JetBrains Rider)
- Conexión a la API (opcional para modo real): URL y token Bearer

Entorno recomendado (desarrollo)

- Windows 10/11 o Linux/macOS con .NET 9
- PowerShell o terminal Bash

Configuración de secretos (token de la API)

1. Asegúrate de que el proyecto tenga `UserSecretsId` en el `.csproj` (ya incluido en este proyecto).
2. Desde la carpeta del proyecto (donde está `ZiurSoftwareChallenge.csproj`) ejecuta:

```powershell
dotnet user-secrets init
dotnet user-secrets set "Api:AuthToken" "<TU_TOKEN>"
```

3. Verifica:

```powershell
dotnet user-secrets list
```

Notas de seguridad

- No almacenes tokens en `appsettings.json` ni en el repositorio.
- Para producción, use un vault (Azure Key Vault, HashiCorp Vault, etc.).
