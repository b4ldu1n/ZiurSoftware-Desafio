# 04 - Testing

Pruebas manuales relevantes para el proyecto actual:

1. Verificar build y ejecución

```powershell
dotnet restore
dotnet build
dotnet run --urls http://localhost:5250
```

2. Verificar consumo de la API real

```powershell
Invoke-RestMethod -Uri 'https://mainserver.ziursoftware.com/.../DocumentosFillsCombos' -Headers @{ Authorization = 'Bearer <TU_TOKEN>' }
```

3. Probar fallo de autenticación

- Remover token:

```powershell
dotnet user-secrets remove "Api:AuthToken"
```

- Iniciar app y confirmar banner de error en UI.

4. Tests automatizados (recomendación)

- Añadir pruebas unitarias para `ApiMovimientoService` con `HttpMessageHandler` simulado que retorne JSON de ejemplo (raíz array y wrapper).
