# 03 - Arquitectura de Software y Diseño Técnico

Este documento detalla el diseño de arquitectura del proyecto, la inyección de dependencias y cómo se aplican los principios SOLID para permitir un consumo seguro de la API real de Ziur con tolerancia a fallos mediante un mecanismo de contingencia (Fallback).

## Arquitectura y Capas de la Solución

El proyecto está diseñado bajo principios de **Clean Architecture** (Arquitectura Limpia) y desacoplamiento de servicios mediante el patrón **Decorator/Wrapper** para la resolución de contingencias.

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
 │   └─────────────────┘       └─────────────┬─────────────┘   │
 │                                           │ Inyecta         │
 │                                           ▼                 │
 │                             ┌───────────────────────────┐   │
 │                             │ FallbackMovimientoService │   │
 │                             │   (Wrapper/Contingencia)  │   │
 │                             └──────┬─────────────┬──────┘   │
 │                                    │ Intenta     │ Fallback │
 │                                    ▼             ▼          │
 │                       ┌────────────┴──┐       ┌──┴────────┐ │
 │                       │  ApiService   │       │MockService│ │
 │                       │ (Http Cliente)│       │(En Memoria│ │
 │                       └───────┬───────┘       └───────────┘ │
 └───────────────────────────────┼─────────────────────────────┘
                                 │  Petición HTTP con Token
                                 ▼
 ┌─────────────────────────────────────────────────────────────┐
 │                        API Real de Ziur                     │
 └─────────────────────────────────────────────────────────────┘
```

### 1. Capa de Presentación (Frontend Blazor Web App)
* **Páginas e Imports (`Components/Pages/` y `_Imports.razor`)**: El componente [Movimientos.razor](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Components/Pages/Movimientos.razor) es la grilla que renderiza visualmente los datos. Esta página **no sabe** si los datos provienen de la API real o de la contingencia en memoria, simplemente inyecta e invoca al contrato `IMovimientoService`.
* **Modelos (`Models/`)**: Contiene el modelo de dominio [Movimiento.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Models/Movimiento.cs) que mapea la estructura de datos: `Codigo`, `Descripcion` y `VActiva`.

### 2. Capa de Servicios (`Services/`)
* **`IMovimientoService`**: Interfaz abstracta que define el contrato de negocio.
* **`ApiMovimientoService`**: Implementación de red que realiza peticiones HTTP asíncronas REST a la API de Ziur agregando cabeceras de autorización de forma dinámica.
* **`MockMovimientoService`**: Implementación en memoria que sirve los datos contables simulados de respaldo.
* **`FallbackMovimientoService`**: Servicio Decorador de contingencia. Intenta consumir de `ApiMovimientoService` y, en caso de cualquier error (red, token inválido o error 401/403), redirige de forma transparente al usuario a los datos estables de `MockMovimientoService`.

---

## Inyección de Dependencias (DI)

En [Program.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Program.cs) del frontend, configuramos de forma explícita el acoplamiento seguro de los servicios:

```csharp
// Registrar servicios para el flujo seguro de API y Fallback
var apiConfig = builder.Configuration.GetSection("Api");

// 1. Registrar servicio Mock en memoria de contingencia
builder.Services.AddScoped<MockMovimientoService>();

// 2. Registrar el HttpClient asociado de forma concreta a ApiMovimientoService
builder.Services.AddHttpClient<ApiMovimientoService>(client =>
{
    client.BaseAddress = new Uri(apiConfig["BaseUrl"] ?? "http://localhost");
});

// 3. Registrar el contrato IMovimientoService resuelto mediante FallbackMovimientoService
builder.Services.AddScoped<IMovimientoService, FallbackMovimientoService>();
```

---

## Principios SOLID Aplicados

* **Single Responsibility Principle (SRP) / Principio de Responsabilidad Única**:
  * `ApiMovimientoService` se encarga de la conectividad HTTP y la autorización.
  * `MockMovimientoService` se encarga de la provisión estática en memoria.
  * `FallbackMovimientoService` gestiona el flujo de reintentos y redirección de fallos.
  * Ninguno interfiere en la responsabilidad de los otros.
* **Open/Closed Principle (OCP) / Principio de Abierto/Cerrado**:
  * Añadir el sistema de fallback no requirió modificar la implementación de red de `ApiMovimientoService` ni la lógica gráfica de `Movimientos.razor`. Se extendió el sistema agregando un decorador.
* **Dependency Inversion Principle (DIP) / Principio de Inversión de Dependencia**:
  * Los módulos de alto nivel y la UI dependen únicamente de la abstracción `IMovimientoService`, delegando el control de contingencia a la inyección de dependencias.
