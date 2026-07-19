# 08 - Configuración del Cliente HTTP y Estructura de Integración

Este documento detalla los archivos del frontend que controlan el consumo de la API, explicando la estructura implementada tras la remoción definitiva de las dependencias locales en memoria (Mock).

---

## Retiro del Servicio Mock en Memoria

Para garantizar el cumplimiento de un entorno cliente-servidor real HTTP, el soporte para `MockMovimientoService` ha sido retirado de la base de código. La aplicación ahora está configurada de forma estricta para resolver las llamadas de movimientos hacia la Minimal API simuladora.

Los 4 archivos que estructuran esta integración son:

1. **`appsettings.json`** (Configuración de red)
2. **`Program.cs`** (Inyección de dependencias)
3. **`ApiMovimientoService.cs`** (Cliente HTTP REST)
4. **`Movimiento.cs`** (Modelo de datos compartido)

---

## 1. Configuración de Red (`appsettings.json`)

El archivo [appsettings.json](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/appsettings.json) especifica la dirección base de la API mediante la llave `"BaseUrl"`:

```json
{
  "Api": {
    "BaseUrl": "http://localhost:5199/"
  }
}
```

* **`BaseUrl`**: Si la API cambia de puerto o es subida a un entorno corporativo de pruebas (ej: Azure o AWS), simplemente se modifica esta línea sin alterar el código C#.
  * Ejemplo para Pruebas en Staging:
    ```json
    "BaseUrl": "https://api-staging.ziur.com/"
    ```

---

## 2. Inyección del Cliente HTTP (`Program.cs`)

El archivo [Program.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Program.cs) lee la configuración al arrancar el host web y registra la clase `ApiMovimientoService` como la única implementación para `IMovimientoService`:

```csharp
// Registrar el servicio de movimientos dinámicamente conectando siempre con la API
var apiConfig = builder.Configuration.GetSection("Api");

builder.Services.AddHttpClient<IMovimientoService, ApiMovimientoService>(client =>
{
    client.BaseAddress = new Uri(apiConfig["BaseUrl"] ?? "http://localhost");
});
```

---

## 3. Consumo REST asíncrono (`ApiMovimientoService.cs`)

El cliente HTTP tipado realiza la solicitud asíncrona a la Minimal API en [ApiMovimientoService.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Services/ApiMovimientoService.cs):

```csharp
public class ApiMovimientoService : IMovimientoService
{
    private readonly HttpClient _http;
    private readonly ILogger<ApiMovimientoService> _logger;

    public ApiMovimientoService(HttpClient http, ILogger<ApiMovimientoService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<List<Movimiento>> ObtenerMovimientosAsync()
    {
        try
        {
            _logger.LogInformation("Obteniendo movimientos desde la API...");
            
            // Consumo de la ruta relativa expuesta en la Minimal API
            var resultado = await _http.GetFromJsonAsync<List<Movimiento>>("api/movimientos")
                ?? new List<Movimiento>();
            
            return resultado;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Error al conectar con la API: {ex.Message}");
            return new List<Movimiento>();
        }
    }
}
```

---

## 4. El Modelo de Datos Compartido (`Movimiento.cs`)

El modelo [Movimiento.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Models/Movimiento.cs) encapsula la estructura del JSON y es mapeado automáticamente por el cliente HTTP:

```csharp
namespace ZiurSoftwareChallenge.Models;

public class Movimiento
{
    public int Codigo { get; set; }
    public string Descripcion { get; set; } = "";
    public bool VActiva { get; set; }
}
```
