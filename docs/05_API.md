# 05 - Especificación de la Minimal API

Este documento describe el funcionamiento técnico de la Minimal API (`ZiurSoftwareChallenge.Api`), su configuración, y sirve como una guía de mantenimiento para el equipo de desarrollo.

## ¿Cómo funciona la Minimal API?

Las **Minimal APIs** se introdujeron en .NET 6 como un enfoque simplificado para crear servicios HTTP rápidos con código mínimo en ASP.NET Core. En lugar de estructurar controladores tradicionales complejos (`ControllerBase` y atributos de enrutamiento), los endpoints se declaran directamente en el archivo `Program.cs` del proyecto utilizando métodos de mapeo como `MapGet`, `MapPost`, `MapPut` y `MapDelete`.

Este enfoque optimiza el rendimiento general y reduce drásticamente el código innecesario.

---

## Endpoints Existentes

### 1. Obtener Movimientos
* **Ruta**: `GET /api/movimientos`
* **Código de Estado Exitoso**: `200 OK`
* **Estructura del Cuerpo (JSON)**:
  ```json
  [
    {
      "Codigo": 29,
      "Descripcion": "Ajuste al Inventario",
      "VActiva": false
    },
    {
      "Codigo": 51,
      "Descripcion": "Avance Produccion",
      "VActiva": false
    },
    {
      "Codigo": 17,
      "Descripcion": "Balance Inicial",
      "VActiva": false
    }
  ]
  ```

---

## Configuración y Mantenimiento Técnico

### 1. Cómo cambiar la URL y los Puertos locales
La configuración de puertos por defecto está en el archivo [launchSettings.json](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge.Api/Properties/launchSettings.json) de la API:
* **HTTP**: `http://localhost:5199`
* **HTTPS**: `https://localhost:7092`

Para cambiar estos puertos locales, modifica la propiedad `"applicationUrl"` en los perfiles `http` o `https`.
Por ejemplo, si necesitas cambiar a `http://localhost:8080`, actualiza:
```json
"applicationUrl": "http://localhost:8080"
```

### 2. Cómo agregar nuevos Endpoints
Para registrar un nuevo endpoint HTTP, abre [Program.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge.Api/Program.cs) del API y agrega un nuevo mapeo antes del método `app.Run()`. Por ejemplo:

```csharp
app.MapGet("/api/movimientos/{id:int}", (int id) =>
{
    var movimiento = new Movimiento(id, "Movimiento Simulado", true);
    return Results.Ok(movimiento);
})
.WithName("GetMovimientoById");
```

### 3. Cómo adaptar el modelo si cambia la respuesta JSON
Si el backend cambia las claves del JSON devuelto, debes sincronizar el modelo en el frontend y posiblemente el comportamiento del serializador en el backend.

* **Caso 1: Cambiar a camelCase estándar**:
  Si la API comienza a responder con minúsculas (ej. `codigo`, `descripcion`), puedes eliminar la configuración global del serializador en `Program.cs` del API:
  ```csharp
  // Eliminar o comentar esta línea para que devuelva camelCase estándar de .NET
  // builder.Services.ConfigureHttpJsonOptions(options => { options.SerializerOptions.PropertyNamingPolicy = null; });
  ```
* **Caso 2: Mapear atributos específicos**:
  Si el JSON utiliza nombres diferentes a los nombres de las propiedades en C# (ej: el JSON devuelve `"cod_mov"` pero en C# queremos mantener `Codigo`), utiliza los atributos de serialización en [Movimiento.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Models/Movimiento.cs):
  ```csharp
  using System.Text.Json.Serialization;

  public class Movimiento
  {
      [JsonPropertyName("cod_mov")]
      public int Codigo { get; set; }

      [JsonPropertyName("descripcion_detallada")]
      public string Descripcion { get; set; } = "";

      [JsonPropertyName("v_activa")]
      public bool VActiva { get; set; }
  }
  ```
  De esta forma, el serializador HTTP mapeará automáticamente la propiedad JSON `"cod_mov"` a la propiedad de objeto `Codigo` de forma transparente para los componentes Blazor.
