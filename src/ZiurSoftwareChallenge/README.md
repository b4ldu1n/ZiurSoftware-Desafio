# ZiurSoftwareChallenge - Aplicación Blazor Web App

## 📋 Descripción

Aplicación Blazor Web App que consume una API REST para mostrar datos de movimientos en una grilla interactiva. La solución está arquitectada para permitir una transición sin código del servicio simulado al servicio real cuando el endpoint esté disponible.

## 🏗️ Arquitectura

```
ZiurSoftwareChallenge/
│
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   ├── NavMenu.razor         ← Navegación con enlace a Movimientos
│   │   └── ...
│   │
│   └── Pages/
│       ├── Home.razor
│       ├── Movimientos.razor      ← Página principal (tabla de datos)
│       └── ...
│
├── Models/
│   └── Movimiento.cs             ← Modelo de datos
│
├── Services/
│   ├── IMovimientoService.cs      ← Interfaz (contrato)
│   ├── MockMovimientoService.cs   ← Implementación simulada (HOY)
│   └── ApiMovimientoService.cs    ← Implementación real (FUTURO)
│
├── appsettings.json              ← Configuración de la API
├── Program.cs                    ← Inyección de dependencias
└── README.md                     ← Esta documentación
```

## 🔄 Patrón de Inyección de Dependencias

La aplicación sigue el patrón de inyección de dependencias para desacoplar las páginas del servicio específico:

```
Página Blazor (Movimientos.razor)
         ↓
IMovimientoService (Interfaz)
         ↓
[MockMovimientoService HOY | ApiMovimientoService DESPUÉS]
         ↓
[JSON Mock | HttpClient → API REST]
```

**Ventaja**: La página nunca cambia. Solo necesitamos reemplazar el servicio en `Program.cs`.

## 📝 Modelo de Datos

**Archivo**: [Models/Movimiento.cs](Models/Movimiento.cs)

```csharp
public class Movimiento
{
    public int Codigo { get; set; }
    public string Descripcion { get; set; }
    public bool VActiva { get; set; }
}
```

Estructura esperada desde la API REST:

```json
[
    { "Codigo": 29, "Descripcion": "Ajuste al Inventario", "VActiva": false },
    { "Codigo": 51, "Descripcion": "Avance Produccion", "VActiva": false },
    { "Codigo": 17, "Descripcion": "Balance Inicial", "VActiva": false }
]
```

## 🔧 Configuración Actual (HOY)

**Archivo**: [Program.cs](Program.cs)

```csharp
// Registrar el servicio de movimientos
builder.Services.AddScoped<IMovimientoService, MockMovimientoService>();
```

**Archivo**: [appsettings.json](appsettings.json)

```json
{
  "Api": {
    "BaseUrl": "https://pendiente-de-ziur.com/"
  }
}
```

El `MockMovimientoService` devuelve los datos de prueba con un retraso simulado de 500ms para imitar latencia de red.

## 🚀 Migración a Servicio Real (CUANDO RECIBAS LA API)

### Paso 1: Actualizar la URL en `appsettings.json`

```json
{
  "Api": {
    "BaseUrl": "https://api.empresa-ziur.com/"
  }
}
```

### Paso 2: Activar el servicio real en `Program.cs`

Reemplaza esta línea:

```csharp
builder.Services.AddScoped<IMovimientoService, MockMovimientoService>();
```

Por estas líneas:

```csharp
builder.Services.AddHttpClient<IMovimientoService, ApiMovimientoService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"] ?? "");
});
```

### Paso 3: Verificar el endpoint

Asegúrate de que la API devuelve datos en formato JSON con la estructura del modelo `Movimiento` en el endpoint `/api/movimientos`.

**¡Listo!** La aplicación seguirá funcionando sin cambios en las páginas Blazor.

## 🎨 Características de la Interfaz

- **Tabla Bootstrap**: Diseño limpio y responsivo
- **Indicador de carga**: Spinner mientras se cargan los datos
- **Badges**: Códigos y estados visuales
- **Manejo de errores**: Mensajes amigables si no hay datos
- **Navegación**: Menú actualizado con enlace a Movimientos

## 📊 Página de Movimientos

**Archivo**: [Components/Pages/Movimientos.razor](Components/Pages/Movimientos.razor)

Acceso: `https://localhost:xxxxx/movimientos`

Características:
- ✅ Carga automática de datos en `OnInitializedAsync()`
- ✅ Inyección del servicio `IMovimientoService`
- ✅ Tabla con columnas: Código, Descripción, Activa
- ✅ Manejo de estados (cargando, vacío, con datos)
- ✅ Badges visuales para mejor UX
- ✅ Try/catch para manejo de excepciones

## 🔍 Servicios

### `IMovimientoService`

Contrato que define las operaciones disponibles:

```csharp
public interface IMovimientoService
{
    Task<List<Movimiento>> ObtenerMovimientosAsync();
}
```

### `MockMovimientoService`

Implementación simulada (en uso actualmente):

```csharp
public class MockMovimientoService : IMovimientoService
{
    public async Task<List<Movimiento>> ObtenerMovimientosAsync()
    {
        await Task.Delay(500); // Simular latencia
        return new List<Movimiento> { ... };
    }
}
```

### `ApiMovimientoService`

Implementación real (lista para activar):

```csharp
public class ApiMovimientoService : IMovimientoService
{
    private readonly HttpClient _http;

    public async Task<List<Movimiento>> ObtenerMovimientosAsync()
    {
        return await _http.GetFromJsonAsync<List<Movimiento>>("api/movimientos")
            ?? new List<Movimiento>();
    }
}
```

## 📦 Requisitos

- **.NET 9.0** o superior
- **Blazor Web App** (InteractiveServer)
- **Bootstrap 5** (ya incluido)

## 🏃 Ejecución

```bash
dotnet run
```

Acceso: `https://localhost:7xxx`

## 📝 Bitácora de Cambios

### v1.0 - Estado Inicial (Con MockService)

- ✅ Modelo `Movimiento` creado
- ✅ Interfaz `IMovimientoService` definida
- ✅ `MockMovimientoService` implementado con datos de prueba
- ✅ `ApiMovimientoService` preparado (sin datos reales aún)
- ✅ Página `Movimientos.razor` con tabla Bootstrap
- ✅ Inyección de dependencias configurada
- ✅ Navegación actualizada
- ✅ Documentación completa

### v2.0 - Integración Real (FUTURO)

- ⏳ Activar `ApiMovimientoService`
- ⏳ Actualizar URL en `appsettings.json`
- ⏳ Pruebas con API real
- ⏳ Manejo avanzado de errores si es necesario

## 💡 Decisiones de Diseño

### ¿Por qué usar una interfaz?

Permite cambiar entre servicios sin tocar el código de las páginas. Esto es un principio fundamental de SOLID (**Dependency Inversion**).

### ¿Por qué `MockMovimientoService`?

Permite desarrollar y probar sin estar bloqueado esperando la API. Es una buena práctica en equipos distribuidos.

### ¿Por qué Bootstrap?

Proporciona estilos profesionales listos para producción sin necesidad de CSS personalizado.

### ¿Por qué logging en `ApiMovimientoService`?

Facilita debugging y monitoreo en producción. `ILogger` es la manera .NET de registrar eventos.

## 🤝 Próximos Pasos

1. Cuando recibas la URL de la API, actualiza `appsettings.json`
2. Descomenta las líneas en `Program.cs` para activar `ApiMovimientoService`
3. Prueba la conexión
4. ¡Listo para demostración técnica y futuras extensiones o escalamiento.!

---

**Desarrollado con profesionalismo y escalabilidad en mente.**
