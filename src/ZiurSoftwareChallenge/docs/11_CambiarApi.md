# 11 - Cambiar a otra API (Guía paso a paso)

Si necesitas apuntar la aplicación a otra API o a un endpoint distinto, sigue estos pasos:

1) Cambiar `BaseUrl` y `Endpoint` en `appsettings.json` o mediante variables de entorno.

- `appsettings.json`:

```json
"Api": {
  "BaseUrl": "https://nuevo-servidor/api/",
  "Endpoint": "NuevoEndpoint"
}
```

2) Cambiar autenticación

- Si la nueva API necesita otro header o esquema, actualiza `Api:AuthHeaderName` y `Api:AuthHeaderValueFormat` en `appsettings.json`.
- Para credenciales sensibles, usa `dotnet user-secrets` o variables de entorno:

```powershell
dotnet user-secrets set "Api:AuthToken" "<TOKEN>"
```

3) Adaptar `Movimiento.cs` si cambia el JSON

- Si la nueva API devuelve campos distintos, actualiza `Models/Movimiento.cs` para reflejar las nuevas propiedades y tipos.
- Agrega atributos `[JsonPropertyName("nombreJson")]` si los nombres difieren.

4) Adaptar `ApiMovimientoService` si cambia la estructura

- Si la API devuelve un wrapper distinto, ajusta la lógica de deserialización (actualmente busca el primer array en el JSON).
- Si la API devuelve un objeto por entidad en vez de lista, actualiza la llamada y el contrato de la interfaz.

5) Probar y validar

- Ejecutar `dotnet build`, `dotnet run`, y probar con `curl` o Postman.
