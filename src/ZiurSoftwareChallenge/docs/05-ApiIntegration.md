# 05 - API Integration (how the code integrates with the endpoint)

Files involved:

- `Program.cs` - registers `ApiMovimientoService` with `AddHttpClient` and sets `BaseAddress` from `Api:BaseUrl`.
- `ApiMovimientoService.cs` - compone la petición, aplica header `Authorization`, envía y deserializa la respuesta.
- `Movimientos.razor` - consume `IMovimientoService` y muestra los datos.

Steps executed at runtime:

1. `Program.cs` configures `HttpClient` with `BaseAddress`.
2. `ApiMovimientoService` receives `HttpClient` via DI.
3. When called, it creates `HttpRequestMessage` targeting `Endpoint` and injects `Authorization` header if provided.
4. Executes request and handles response as described in `05_API.md`.
