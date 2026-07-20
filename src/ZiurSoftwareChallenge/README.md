# ZiurSoftwareChallenge - Aplicación Blazor Web App

## Resumen ejecutivo

Este repositorio contiene una aplicación Blazor Web App (.NET 9) desarrollada como respuesta a un reto técnico. La aplicación muestra una grilla de "movimientos" consumiendo una API REST. La solución fue diseñada y desarrollada siguiendo buenas prácticas: separación de responsabilidades, inyección de dependencias, uso de `HttpClientFactory`, configuración segura de tokens (User Secrets) y un modo de desarrollo con datos simulados (mock) para mayor productividad.

La documentación en la carpeta `docs/` ofrece detalles técnicos, guías de ejecución, pruebas y mantenimiento.

---

## Contenido principal

- `Components/` - componentes y páginas Blazor.
- `Models/` - modelos de datos, incluyendo `Movimiento.cs`.
- `Services/` - implementaciones de acceso a datos (`IMovimientoService`, `ApiMovimientoService`).
- `appsettings.json` - configuración de la API (BaseUrl, Endpoint, headers).
- `Program.cs` - entrada de la aplicación y registro de servicios.

---

## Enlaces rápidos a la documentación

- docs/01_Arquitectura.md
- docs/02_EstructuraProyecto.md
- docs/03_ComponentesPlantillaBlazor.md
- docs/04_ComponentesDesarrollados.md
- docs/05_FlujoDatos.md
- docs/06_API.md
- docs/07_Mock.md
- docs/08_Ejecucion.md
- docs/09_Pruebas.md
- docs/10_Mantenimiento.md
- docs/11_CambiarApi.md
- docs/12_PreguntasEntrevista.md

---

## Estado actual

- La aplicación funciona tanto en modo mock como consumiendo la API real (cuando se suministra `Api:AuthToken`).
- La deserialización es robusta ante respuestas envueltas (por ejemplo `{ "value": [...] }`).
- Se agregó en la UI indicador de estado de conexión y control para reintento, además de ordenamiento de la grilla.

---

## Soporte y contacto

Si necesitas ayuda para adaptar la API, agregar pruebas automatizadas o desplegar la aplicación, abre un issue o contacta al autor del repositorio.


1. Cuando recibas la URL de la API, actualiza `appsettings.json`
2. Descomenta las líneas en `Program.cs` para activar `ApiMovimientoService`
3. Prueba la conexión
4. ¡Listo para demostración técnica y futuras extensiones o escalamiento.!

---

**Desarrollado con profesionalismo y escalabilidad en mente.**
