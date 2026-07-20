# 08 - Configuración de Credenciales y Mecanismos de Fallback

Este documento describe la estructura y configuración de seguridad implementada en el proyecto para integrar de forma segura el endpoint real de la API de Ziur, manteniendo el servicio Mock como mecanismo automático de contingencia (Fallback).

---

## Estructura de Integración y Seguridad

En lugar de alternar de forma manual mediante un interruptor en `appsettings.json`, la aplicación implementa una arquitectura auto-conmutada. Se intenta consumir el servicio de red; en caso de caída o falta de autenticación, el sistema retrocede al Mock local para garantizar la disponibilidad visual de la grilla contable.

Los 5 archivos que estructuran esta integración son:

1.  **`appsettings.json`** (Parámetros base de la API)
2.  **`secrets.json`** (Almacén local seguro del Token)
3.  **`Program.cs`** (Inyección en cadena de dependencias)
4.  **`ApiMovimientoService.cs`** (Cliente HTTP con inyección de cabecera dinámica)
5.  **`FallbackMovimientoService.cs`** (Decorador de contingencia y control de excepciones)

---

## 1. Configuración de Parámetros Públicos (`appsettings.json`)

El archivo [appsettings.json](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/appsettings.json) contiene los parámetros públicos no sensibles necesarios para enrutar el cliente HTTP:

```json
{
  "Api": {
    "BaseUrl": "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/",
    "Endpoint": "DocumentosFillsCombos",
    "AuthHeaderName": "Authorization",
    "AuthHeaderValueFormat": "Bearer {0}",
    "UseFallback": true
  }
}
```

*   **`BaseUrl`**: URL del servidor de la API de Ziur.
*   **`Endpoint`**: Ruta relativa del recurso (`DocumentosFillsCombos`).
*   **`UseFallback`**: Habilita o deshabilita la contingencia del Mock.

---

## 2. Configuración Segura de Credenciales (`secrets.json` - User Secrets)

Para desarrollo local, **nunca** guarde su Token de producción en `appsettings.json` ni lo suba a Git. Use el Administrador de Secretos de .NET.

### Comandos de Configuración desde la Terminal
1.  **Inicializar Secrets** (ya configurado en el proyecto):
    ```bash
    dotnet user-secrets init --project src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
    ```
2.  **Establecer su Token manual**:
    ```bash
    dotnet user-secrets set "Api:AuthToken" "SU_TOKEN_REAL_AQUI" --project src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
    ```

El framework de .NET leerá automáticamente la clave `Api:AuthToken` desde su almacén de secretos local e inyectará el token de forma segura durante la ejecución del programa.

---

## 3. Inyección en Cadena de Dependencias (`Program.cs`)

El archivo [Program.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Program.cs) asocia la resolución del servicio al decorador:

```csharp
// Registrar servicios para el flujo seguro de API y Fallback
var apiConfig = builder.Configuration.GetSection("Api");

// A. Registrar el servicio Mock en memoria para contingencia
builder.Services.AddScoped<MockMovimientoService>();

// B. Registrar el HttpClient concreto asociado a ApiMovimientoService
builder.Services.AddHttpClient<ApiMovimientoService>(client =>
{
    client.BaseAddress = new Uri(apiConfig["BaseUrl"] ?? "http://localhost");
});

// C. Resolver la interfaz a través del intermediario Fallback
builder.Services.AddScoped<IMovimientoService, FallbackMovimientoService>();
```

---

## 4. Lógica del Cliente HTTP (`ApiMovimientoService.cs`)

El cliente HTTP recupera las credenciales dinámicamente y las aplica por petición utilizando `HttpRequestMessage`:

```csharp
public async Task<List<Movimiento>> ObtenerMovimientosAsync()
{
    var endpoint = _configuration["Api:Endpoint"] ?? "DocumentosFillsCombos";
    var token = _configuration["Api:AuthToken"];
    var headerName = _configuration["Api:AuthHeaderName"] ?? "Authorization";
    var headerValueFormat = _configuration["Api:AuthHeaderValueFormat"] ?? "Bearer {0}";

    var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

    if (!string.IsNullOrWhiteSpace(token))
    {
        var headerValue = string.Format(headerValueFormat, token);
        request.Headers.TryAddWithoutValidation(headerName, headerValue);
    }

    var response = await _http.SendAsync(request);
    response.EnsureSuccessStatusCode(); // Lanza excepción si hay error HTTP (ej: 401, 403, 500)

    return await response.Content.ReadFromJsonAsync<List<Movimiento>>()
        ?? new List<Movimiento>();
}
```

---

## 5. Control de Excepciones y Fallback (`FallbackMovimientoService.cs`)

El servicio Wrapper intercepta cualquier error en la petición HTTP anterior y retorna los datos estables de contingencia de forma transparente para la grilla Blazor:

```csharp
public async Task<List<Movimiento>> ObtenerMovimientosAsync()
{
    try
    {
        // Intentar obtener de la API real
        var result = await _apiService.ObtenerMovimientosAsync();
        if (result != null && result.Count > 0)
        {
            return result;
        }
    }
    catch (Exception ex)
    {
        _logger.LogError($"Fallo al conectar con la API real: {ex.Message}");
    }

    // Activar contingencia si está configurada
    if (_useFallback)
    {
        _logger.LogWarning("Redirigiendo flujo hacia Mock de respaldo contable.");
        return await _mockService.ObtenerMovimientosAsync();
    }

    return new List<Movimiento>();
}
```
