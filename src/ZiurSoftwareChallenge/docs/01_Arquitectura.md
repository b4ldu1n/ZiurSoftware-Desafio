# 01 - Arquitectura

## Visión general

La aplicación sigue una arquitectura en capas ligera adecuada para aplicaciones Blazor Server/Interactive. Se separan claramente la UI (componentes Razor), los modelos (POCOs) y los servicios (acceso a datos y lógica de integración con la API). La inyección de dependencias permite intercambiar implementaciones (mock vs real) sin modificar la UI.

Componentes principales:

- `Components/` - UI y páginas.
- `Services/` - abstracciones e implementaciones de acceso a datos.
- `Models/` - DTOs y modelos de negocio.
- `Program.cs` - composición de la aplicación, registro de `HttpClient` y servicios.

### Patrón de flujo

ASCII diagram:

```
Usuario
  ↓
Blazor (Browser/Server)
  ↓
Página Razor (`Movimientos.razor`)
  ↓
`IMovimientoService` (inyección)
  ↓
`ApiMovimientoService` (HttpClient) OR `MockMovimientoService`
  ↓
HttpClient
  ↓
API REST (Ziur)
  ↓
JSON
  ↓
Modelo `Movimiento`
  ↓
Tabla HTML (grilla)
```

Cada bloque respeta separación de responsabilidades: la página solo solicita datos al servicio, que se encarga de la comunicación con la API y de convertir JSON en objetos.
