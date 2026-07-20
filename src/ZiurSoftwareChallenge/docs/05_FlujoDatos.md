# 05 - Flujo de datos (API → UI)

Descripción del flujo completo desde la API hasta la interfaz:

1. La página `Movimientos.razor` se inicializa y llama a `IMovimientoService.ObtenerMovimientosAsync()`.

2. Dependiendo de la configuración, la implementación inyectada es `ApiMovimientoService` (producción) o `MockMovimientoService` (desarrollo). En el estado actual productivo `ApiMovimientoService` está registrado.

3. `ApiMovimientoService` construye un `HttpRequestMessage` con método `GET` a `Api:Endpoint` y, si existe, añade la cabecera `Authorization: Bearer <token>` obtenida desde la configuración (`User Secrets` en desarrollo).

4. Se envía la petición usando `HttpClient` registrado con `AddHttpClient` y `BaseAddress` desde `Api:BaseUrl`.

5. La API responde con JSON. El servicio intenta deserializar en `List<Movimiento>`. Si la respuesta contiene un wrapper (`{ "value": [ ... ] }`) el servicio detecta el primer array en el JSON y lo deserializa.

6. El resultado (lista de `Movimiento`) se devuelve a la página Blazor.

7. La página renderiza la tabla HTML con los datos. Si hubo error HTTP o excepción, la página muestra un banner con el error y ofrece reintento.

Observaciones:

- La deserialización es resiliente ante distintos formatos (raíz array o wrapper con propiedad `value`).
- Los tokens deben almacenarse en `dotnet user-secrets` o en variables de entorno; no deben ir en el repositorio.
