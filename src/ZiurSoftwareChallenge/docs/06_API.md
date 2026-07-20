# 06 - API (Ziur) - detalles de integración

Endpoint utilizado en configuración actual:

- BaseUrl: `https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/`
- Endpoint: `DocumentosFillsCombos`

URL completa de ejemplo:

```
GET https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/DocumentosFillsCombos
```

Autenticación:

- Se espera token tipo Bearer en la cabecera `Authorization`.
- En desarrollo y pruebas locales, el token se configura mediante `dotnet user-secrets set "Api:AuthToken" "<token>"`.

Formato esperado de respuesta (observado):

```json
{
  "value": [ { "Codigo": 29, "Descripcion": "Ajuste...", "VActiva": false }, ... ],
  "Count": 39
}
```

Notas técnicas:

- `ApiMovimientoService` maneja ambos formatos: array directo o wrapper con `value`.
- La aplicación usa `response.EnsureSuccessStatusCode()` para propagar errores HTTP y que la UI los muestre.
