# 04 - Componentes desarrollados para el reto

Este documento describe las piezas desarrolladas específicamente para cumplir con el reto técnico.

1) `Models/Movimiento.cs`

- Descripción: POCO que representa un movimiento con propiedades `Codigo`, `Descripcion` y `VActiva`.
- Responsabilidad: definir estructura de datos usada por la UI.

2) `Services/IMovimientoService.cs`

- Descripción: interfaz que define `Task<List<Movimiento>> ObtenerMovimientosAsync()`.
- Responsabilidad: contrato que abstrae la fuente de datos (mock o API real).

3) `Services/MockMovimientoService.cs` (removido después)

- Descripción: implementación en memoria usada durante desarrollo.
- Responsabilidad: permitir desarrollo offline o cuando la API no está disponible.

4) `Services/ApiMovimientoService.cs`

- Descripción: implementación que usa `HttpClient` para consultar la API real.
- Responsabilidad: construir request (headers, token) y deserializar la respuesta JSON a `List<Movimiento>`.
- Notas: incluye lógica robusta para manejar respuestas envueltas (`{ "value": [...] }`).

5) `Components/Pages/Movimientos.razor`

- Descripción: página Blazor que muestra la grilla de movimientos.
- Responsabilidad: solicitar datos al `IMovimientoService`, mostrar estado de carga, mostrar banners de estado (éxito/error), ofrecer `Reintentar Conexión` y soporte de ordenamiento asc/desc en columnas.

6) `Program.cs` (registro de DI)

- Descripción: registra `ApiMovimientoService` con `AddHttpClient` y `IMovimientoService`.
- Responsabilidad: composición y configuración central de la app.
