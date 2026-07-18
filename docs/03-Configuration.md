# 03 - Guía de Configuración del Sistema (Configuration)

Este documento detalla la estructura y parámetros de configuración del proyecto en el archivo [appsettings.json](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/appsettings.json) para entornos de desarrollo y producción.

---

## 1. Archivo de Configuración Principal (`appsettings.json`)

El control de origen de datos se encuentra estructurado en el nodo `"Api"`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Api": {
    "UseMock": true,
    "BaseUrl": "https://pendiente-de-ziur.com/"
  }
}
```

---

## 2. Parámetros Disponibles

| Parámetro | Tipo | Valor por Defecto | Descripción |
| :--- | :--- | :--- | :--- |
| **`Api:UseMock`** | `Boolean` | `true` | Determina si la aplicación utiliza datos de simulación (`MockMovimientoService`) o invoca a un servicio externo por red. |
| **`Api:BaseUrl`** | `String` | `"https://pendiente-de-ziur.com/"` | URL base del servidor de API REST externa. Se inyecta en el cliente HTTP de `ApiMovimientoService`. |
| **`Api:Endpoint`** | `String` | `"api/movimientos"` | Ruta interna del recurso REST (fijado internamente dentro del servicio en el método `GetFromJsonAsync`). |

---

## 3. Comportamiento del Motor de Inyección de Dependencias

El motor lee la configuración al arrancar el host en [Program.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Program.cs):

```csharp
var apiConfig = builder.Configuration.GetSection("Api");
var useMock = apiConfig.GetValue<bool>("UseMock", defaultValue: true);

if (useMock)
{
    // Carga local simulada (Desarrollo y demos locales sin conexión)
    builder.Services.AddScoped<IMovimientoService, MockMovimientoService>();
}
else
{
    // Cliente HTTP tipado (Integración real con el backend de Ziur)
    builder.Services.AddHttpClient<IMovimientoService, ApiMovimientoService>(client =>
    {
        client.BaseAddress = new Uri(apiConfig["BaseUrl"] ?? "http://localhost");
    });
}
```

---

## 4. Configuración para Producción (CI/CD)

En entornos de producción (IIS, Azure, AWS, Docker, Kubernetes), es una buena práctica no modificar el archivo `appsettings.json` directamente. En su lugar, se pueden sobrescribir estas propiedades utilizando **Variables de Entorno** del sistema operativo o contenedores:

* `Api__UseMock` = `false`
* `Api__BaseUrl` = `https://api.ziursoftware.com/prod/`
