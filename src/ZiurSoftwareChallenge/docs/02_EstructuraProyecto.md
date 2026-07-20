# 02 - Estructura del proyecto

Raíz del proyecto:

- `Components/` - Contiene los componentes Razor y las páginas:
  - `Pages/` - `Home.razor`, `Counter.razor`, `Weather.razor`, `Movimientos.razor`.
  - `Layout/` - `MainLayout.razor`, `NavMenu.razor`.
- `Models/` - Clases POCO que representan los datos (`Movimiento.cs`).
- `Services/` - Interfaz e implementaciones de acceso a datos:
  - `IMovimientoService.cs` - contrato.
  - `ApiMovimientoService.cs` - cliente real usando `HttpClient`.
  - `MockMovimientoService.cs` (removido en la transición) - servicio simulado.
  - `FallbackMovimientoService.cs` (removido) - decorador de fallback (antes disponible).
- `appsettings.json` - configuración de la API (BaseUrl, Endpoint, headers).
- `Program.cs` - punto de entrada y registro de servicios.
- `wwwroot/` - activos estáticos (CSS, JS, imágenes).
- `docs/` - documentación técnica generada (esta carpeta).

Archivos generados por la plantilla y su estado:

- `App.razor` - generado por plantilla.
- `_Imports.razor` - generado por plantilla.
- `Program.cs` - modificado para registrar `ApiMovimientoService`.
- `appsettings.json` - modificado para incluir `Api` settings.
