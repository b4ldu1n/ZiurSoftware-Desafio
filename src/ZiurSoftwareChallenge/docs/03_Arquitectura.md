# 03 - Arquitectura detallada

Este documento amplía la arquitectura y explica la interacción entre componentes.

Componentes clave:

- `Components/Pages/Movimientos.razor`: UI, control de estados (loading, success, error), ordenamiento y reintento.
- `Services/ApiMovimientoService.cs`: construcción de request, aplicación de headers de autenticación y deserialización robusta.
- `Models/Movimiento.cs`: modelo POCO.
- `Program.cs`: composición y `AddHttpClient`.

Notas:

- El proyecto evita embutir lógica de negocio en la UI; la deserialización y la lógica de integración residen en el servicio.
- Para cambios en el modelo o en la estructura JSON, el único lugar que requiere edición es `Services/ApiMovimientoService.cs` y `Models/Movimiento.cs`.
