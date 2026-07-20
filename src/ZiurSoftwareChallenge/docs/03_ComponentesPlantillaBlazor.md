# 03 - Componentes creados por la plantilla oficial de .NET (blazor)

La plantilla oficial `dotnet new blazor` crea una estructura mínima que incluye recursos y páginas de ejemplo. A continuación se describen los archivos y su propósito, y si fueron modificados en este proyecto.

Lista de archivos y carpetas generadas por la plantilla:

- `App.razor` — punto de montaje de la app Blazor. (GENERADO, no modificado)
  - Contiene el enrutador y referencia a `MainLayout`.

- `Routes.razor` (puede corresponder a `App.razor`/enrutamiento en algunas plantillas) — define rutas. (GENERADO)

- `_Imports.razor` — importa nombres de espacios y directivas comunes para los componentes. (GENERADO)

- `MainLayout.razor` — layout principal con `NavMenu` y `Body`. (GENERADO, puede haber ajustes mínimos de estilo)

- `NavMenu.razor` — menú de navegación lateral con enlaces. (GENERADO, se actualizó para incluir `Movimientos`)

- `Pages/Home.razor` — página de inicio de ejemplo. (GENERADO)

- `Pages/Counter.razor` — ejemplo de contador. (GENERADO)

- `Pages/Weather.razor` — ejemplo de consumo de servicio (clima). (GENERADO)

- `Pages/Error.razor` — página de error por defecto. (GENERADO)

- `Program.cs` — bootstrap de la aplicación. (GENERADO por plantilla, MODIFICADO para registrar servicios y `HttpClient`)

- `appsettings.json` — configuración; la plantilla incluye valores por defecto. (GENERADO, MODIFICADO para agregar bloque `Api`)

- `launchSettings.json` (en `Properties/`) — configuración del perfil de lanzamiento (puertos locales). (GENERADO)

- `wwwroot/` — archivos estáticos (GENERADO)

Archivos que no se modificaron: `App.razor`, `_Imports.razor`, `Pages/Counter.razor` (salvo estilos menores), `Pages/Home.razor`.

Archivos modificados: `Program.cs` (registro de servicios y `HttpClient`), `NavMenu.razor` (enlace a Movimientos), `appsettings.json` (configuración de la API), y adiciones en `Components/Pages/Movimientos.razor`.
# 03 - Componentes Generados por la Plantilla Oficial de Blazor

Este proyecto se generó inicialmente con la plantilla oficial `dotnet new blazor` (Interactive Server Components). A continuación se describen los archivos y su responsabilidad.

- `App.razor`:
  - Punto de entrada de los componentes de la aplicación Blazor.
  - Normalmente contiene el Router y la configuración básica de la aplicación.
  - En este proyecto no fue modificado de forma crítica.

- `Routes.razor` / `Router`:
  - Define las rutas principales y fallback de navegación.
  - Es la pieza que mapea URLs a componentes Razor.

- `_Imports.razor`:
  - Centraliza las directivas `@using` y `@namespace` para los componentes. Evita repetición.

- `MainLayout.razor`:
  - Layout principal que define la estructura del documento (barra lateral, header, body).
  - Incluye `NavMenu.razor`.

- `NavMenu.razor`:
  - Menú de navegación lateral con enlaces a `Home`, `Counter`, `Weather`, `Movimientos`.

- `Pages/Home.razor`:
  - Página de inicio generada por la plantilla.

- `Pages/Counter.razor`:
  - Página de ejemplo con contador; útil para comprobar renderizado.

- `Pages/Weather.razor`:
  - Página de ejemplo que consume un servicio de clima (plantilla). No se usa en el reto, pero se mantiene.

- `Pages/Error.razor`:
  - Manejo básico de errores en la navegación.

- `Program.cs` (generado por plantilla, luego modificado):
  - Punto de configuración del host y DI. La plantilla crea una versión base; en este proyecto se actualizó para registrar `ApiMovimientoService` y servicios relacionados.

- `appsettings.json`, `launchSettings.json`, `wwwroot`, `Properties`, `bin`, `obj`:
  - Archivos y carpetas estándar creados por la plantilla y el entorno de .NET.

Cuáles nunca fueron modificados

- Archivos estáticos en `wwwroot/` (excepto incorporar bootstrap si fue actualizado automáticamente).

Cuáles sí fueron modificados

- `Program.cs` (registro de servicios y configuración de `HttpClient`).
- Se agregaron páginas y componentes propios en `Components/Pages` para el reto.
