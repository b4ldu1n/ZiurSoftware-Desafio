# 02 - Architecture

Arquitectura de la aplicación (actual):

```
Usuario
  ↓
Blazor (Server / Interactive)
  ↓
Página Razor: Components/Pages/Movimientos.razor
  ↓
Servicio: IMovimientoService -> ApiMovimientoService
  ↓
HttpClient (AddHttpClient)
  ↓
API REST (Ziur)
  ↓
JSON -> Modelo `Movimiento` -> Render en tabla
```

Responsabilidades:

- La página solo orquesta UI y demanda datos al servicio.
- `ApiMovimientoService` es responsable de la comunicación HTTP, headers y deserialización.
- `Program.cs` registra servicios y configura `HttpClient`.
