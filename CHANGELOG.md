# CHANGELOG - ZiurSoftwareChallenge

Todos los cambios notables en este proyecto serán documentados en este archivo.

El formato está basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/) y este proyecto se adhiere a [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [1.0.0] - 2026-07-18

### Añadido
* **Estructura del Proyecto**: Creación de la solución Blazor Web App con .NET 9 en el modo de renderizado `Interactive Server`.
* **Modelado de Datos**: Implementación de la entidad de intercambio [Movimiento.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Models/Movimiento.cs) mapeada con las propiedades `Codigo`, `Descripcion` y `VActiva`.
* **Abstracción de Lógica**: Creación del contrato [IMovimientoService.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Services/IMovimientoService.cs) para la inyección de dependencias desacoplada.
* **Proveedor Simulado (Mock)**: Creación de [MockMovimientoService.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Services/MockMovimientoService.cs) que emula 500 ms de latencia de red y retorna 3 movimientos del reto de inventario/producción contable.
* **Proveedor Real (API Client)**: Creación de [ApiMovimientoService.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Services/ApiMovimientoService.cs) con cliente HTTP para conexión futura.
* **Navegación e Interfaz**: Incorporación del enlace a `/movimientos` en el menú lateral [NavMenu.razor](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Components/Layout/NavMenu.razor) y maquetación de la grilla responsiva con Bootstrap en [Movimientos.razor](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Components/Pages/Movimientos.razor).
* **Mecanismo de Alternancia Dinámica**: Incorporación del parámetro `"Api:UseMock"` en [appsettings.json](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/appsettings.json) y lógica condicional de IoC en [Program.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Program.cs).
* **Documentación de Proyecto**: Suite completa de documentación en `docs/` detallando entorno, arquitectura, configuración, pruebas y despliegue local.
