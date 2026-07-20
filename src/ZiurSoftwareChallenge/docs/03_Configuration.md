# 03 - Configuration

Archivo principal: `appsettings.json` (raíz del proyecto).

Clave `Api` esperada:

- `BaseUrl` - URL base de la API (ej: `https://mainserver.../api/`).
- `Endpoint` - segmento de endpoint (ej: `DocumentosFillsCombos`).
- `AuthHeaderName` - nombre del header de autenticación (`Authorization`).
- `AuthHeaderValueFormat` - formato del valor del header (`Bearer {0}`).

Secretos locales:

- `Api:AuthToken` - configurado con `dotnet user-secrets`.

Ejemplo:

```json
"Api": {
  "BaseUrl": "https://mainserver.../api/",
  "Endpoint": "DocumentosFillsCombos",
  "AuthHeaderName": "Authorization",
  "AuthHeaderValueFormat": "Bearer {0}"
}
```
