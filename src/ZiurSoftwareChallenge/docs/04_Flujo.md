# 04 - Flujo

Detalle del flujo de datos y control en la aplicación actual.

1. `Movimientos.razor` inicia la carga en `OnInitializedAsync`.
2. Llama `IMovimientoService.ObtenerMovimientosAsync()`.
3. Implementación inyectada: `ApiMovimientoService`.
4. `ApiMovimientoService` construye `HttpRequestMessage` hacia `Api:Endpoint`, aplica header `Authorization` si hay token y ejecuta la petición con `HttpClient`.
5. Respuesta JSON es deserializada a `List<Movimiento>` (se detecta array directo o `value` wrapper).
6. `Movimientos.razor` recibe la lista y la muestra, gestionando estados y ordenamiento.
