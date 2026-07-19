# 03 - Arquitectura de Software y Diseño Técnico

Este documento detalla el diseño de arquitectura del proyecto, la inyección de dependencias y cómo se aplican los principios SOLID de forma desacoplada para consumir la API de movimientos sobre red.

## Arquitectura y Capas de la Solución

El proyecto está diseñado bajo principios de **Clean Architecture** (Arquitectura Limpia) y desacoplamiento de servicios. Consta de dos proyectos principales que se ejecutan de manera independiente:

```
[ Cliente / Navegador ]
         │
         ▼
 ┌─────────────────────────────────────────────────────────────┐
 │                 ZiurSoftwareChallenge (Blazor)              │
 │                                                             │
 │   ┌─────────────────┐       ┌───────────────────────────┐   │
 │   │    Component    │ ────> │    IMovimientoService     │   │
 │   │ (Movimientos)   │       │       (Abstracción)       │   │
 │   └─────────────────┘       └───────────────────────────┘   │
 │                                           ▲                 │
 │                                           │                 │
 │                                     ┌─────┴──────┐          │
 │                                     │ ApiService │          │
 │                                     │(Http REST) │          │
 │                                     └────────────┘          │
 └───────────────────────────────────────────┼─────────────────┘
                                             │  Petición HTTP
                                             ▼
 ┌─────────────────────────────────────────────────────────────┐
 │                ZiurSoftwareChallenge.Api (Minimal API)      │
 │                                                             │
 │   [ GET /api/movimientos ] ──> Retorna List<Movimiento>     │
 └─────────────────────────────────────────────────────────────┘
```

### 1. Capa de Presentación (Frontend Blazor Web App)
* **Páginas e Imports (`Components/Pages/` y `_Imports.razor`)**: Define las páginas de la aplicación. [Movimientos.razor](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Components/Pages/Movimientos.razor) es la grilla que renderiza visualmente los datos. Esta página **no sabe** de dónde provienen los datos, simplemente inyecta e invoca al contrato `IMovimientoService`.
* **Modelos (`Models/`)**: Contiene el modelo de dominio [Movimiento.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Models/Movimiento.cs) que representa la estructura de datos: `Codigo`, `Descripcion` y `VActiva`.

### 2. Capa de Servicios (`Services/`)
* **`IMovimientoService`**: Interfaz abstracta que define el contrato `Task<List<Movimiento>> ObtenerMovimientosAsync()`.
* **`ApiMovimientoService`**: Implementación cliente HTTP que consume los datos serializados en JSON mediante llamadas REST HTTP (`HttpClient.GetFromJsonAsync`) a la Minimal API.

### 3. Capa de Backend (Minimal API)
* **`ZiurSoftwareChallenge.Api`**: Proyecto backend minimalista e independiente. Expone los recursos HTTP REST sin la sobrecarga de controladores tradicionales usando Minimal APIs en `.NET 9`.

---

## Inyección de Dependencias

En [Program.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Program.cs) del frontend, el registro del servicio asocia de forma directa la interfaz con el cliente HTTP correspondiente:

```csharp
var apiConfig = builder.Configuration.GetSection("Api");

builder.Services.AddHttpClient<IMovimientoService, ApiMovimientoService>(client =>
{
    client.BaseAddress = new Uri(apiConfig["BaseUrl"] ?? "http://localhost");
});
```

Esto asegura que la aplicación consuma dinámicamente la dirección URL especificada por la configuración del cliente sin acoplamiento a implementaciones en memoria.

---

## Flujo Completo de la Aplicación

1. **Inicialización**: El usuario ingresa a la ruta `/movimientos`.
2. **OnInitializedAsync**: El ciclo de vida de Blazor ejecuta el método de inicialización del componente llamando a `Servicio.ObtenerMovimientosAsync()`.
3. **Resolución de Dependencia**: Se instancia `ApiMovimientoService` con un `HttpClient` configurado con la URL base de la Minimal API.
4. **Comunicación HTTP**:
   * El `HttpClient` ejecuta un request `GET` a la ruta relativa `api/movimientos`.
   * La **Minimal API** recibe la petición, serializa la lista de movimientos con formato PascalCase en el cuerpo del JSON, y retorna un código de estado `200 OK`.
   * El `HttpClient` recibe el JSON y lo deserializa de vuelta a un objeto fuertemente tipado `List<Movimiento>`.
5. **Renderizado**: El componente Blazor recibe la lista y realiza un ciclo `foreach` en HTML para pintar la tabla en pantalla.

---

## Principios SOLID Aplicados

* **Single Responsibility Principle (SRP) / Principio de Responsabilidad Única**:
  * La página `Movimientos.razor` es únicamente responsable de la interfaz gráfica y de pintar la lista de movimientos. No tiene lógica sobre llamadas HTTP o estructuración de endpoints.
  * El servicio `ApiMovimientoService` tiene la única responsabilidad de consumir y deserializar la API.
  * La Minimal API tiene la única responsabilidad de exponer y simular el endpoint REST de movimientos de negocio.
* **Open/Closed Principle (OCP) / Principio de Abierto/Cerrado**:
  * La arquitectura está abierta a la extensión pero cerrada a la modificación. Si en el futuro decidimos consumir los movimientos desde una base de datos local SQLite o una cola gRPC, simplemente creamos una nueva clase que implemente `IMovimientoService` y la registramos en `Program.cs`. No es necesario alterar el componente de presentación de la interfaz.
* **Dependency Inversion Principle (DIP) / Principio de Inversión de Dependencia**:
  * Los módulos de alto nivel (como la página web `Movimientos.razor`) no dependen de módulos de bajo nivel (como `ApiMovimientoService`). Ambos dependen de la abstracción `IMovimientoService`.
