# 05 - API

Detalles mínimos necesarios para integración con la API de Ziur.

- BaseUrl y Endpoint se componen en `Api:BaseUrl` + `Api:Endpoint`.
- Se usa header `Authorization: Bearer <token>`.
- Respuesta observada: objeto con `value` (array) y `Count`.

Comportamiento implementado en `ApiMovimientoService`:

- Añade header si hay token.
- Llama `response.EnsureSuccessStatusCode()` para propagar errores.
- Deserializa `List<Movimiento>` directamente o busca el primer array dentro del JSON.
