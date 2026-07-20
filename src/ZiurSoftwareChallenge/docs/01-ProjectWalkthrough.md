# 01 - Project Walkthrough

Resumen rápido de la aplicación (presente en el repositorio):

- Página principal relevante: `Components/Pages/Movimientos.razor` — muestra la grilla de movimientos.
- Modelo: `Models/Movimiento.cs` — propiedades `Codigo`, `Descripcion`, `VActiva`.
- Servicio de integración: `Services/ApiMovimientoService.cs` — realiza llamadas HTTP a la API configurada.
- Composición: `Program.cs` — registra `ApiMovimientoService` con `AddHttpClient` y expone `IMovimientoService`.
- Configuración: `appsettings.json` contiene `Api:BaseUrl` y `Api:Endpoint`.

Flujo de interacción (alto nivel):

1. Usuario abre `/movimientos`.
2. `Movimientos.razor` solicita datos a `IMovimientoService`.
3. Inyección resuelve `ApiMovimientoService`.
4. `ApiMovimientoService` llama la API con token en cabecera.
5. Respuesta JSON → deserialización a `List<Movimiento>` → renderizado en tabla.
