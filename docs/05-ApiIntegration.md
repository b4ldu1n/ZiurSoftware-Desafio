# 05 - Plan de Integración de la API Real (Api Integration)

Este documento describe la estrategia de transición para conectar la aplicación web a la API REST real de Ziur Software cuando sea suministrada, analizando la tolerancia a fallos y cambios futuros en los contratos de datos.

---

## 1. Funcionamiento del Mock Actual

En la fase actual de desarrollo, la aplicación web opera de manera aislada consumiendo datos simulados de la siguiente forma:

```
[Movimientos.razor] (Página UI)
       │
       ▼ (Resuelve)
[IMovimientoService] (Abstracción)
       │
       ▼ (Implementación activa)
[MockMovimientoService]
       │
       ▼ (Retorna)
  List<Movimiento> (3 Registros Estáticos)
```

---

## 2. Transición al Endpoint Real de Ziur

Cuando Ziur Software suministre la URL de la API real, la puesta en marcha requerirá **exclusivamente** dos modificaciones de configuración, sin afectar al código ejecutable:

1. **Editar `appsettings.json`**:
   * Cambiar el parámetro `"UseMock": true` a `"UseMock": false`.
   * Sustituir la `"BaseUrl"` temporal por la URL real del servidor de Ziur.
   ```json
   "Api": {
     "UseMock": false,
     "BaseUrl": "https://api-real-ziur.com/"
   }
   ```
2. **Reinicio de la aplicación**: El host de ASP.NET Core registrará automáticamente `ApiMovimientoService` en lugar del Mock.

### Componentes que NO requieren modificación:
* ❌ La interfaz gráfica (`Movimientos.razor`).
* ❌ El modelo de intercambio de datos (`Movimiento.cs`).
* ❌ La navegación y maquetación global (`NavMenu.razor`, `MainLayout.razor`).
* ❌ La lógica de presentación.

---

## 3. Plan de Contingencia ante Cambios de Contrato de Datos

En el desarrollo de software real, los endpoints de API pueden sufrir modificaciones no planificadas en sus estructuras JSON. Nuestra arquitectura basada en el principio SOLID de inversión de dependencias está preparada para mitigar este riesgo:

> [!TIP]
> **Escenario A: El contrato JSON se mantiene idéntico**
> * *Estructura*: `[ { "Codigo": 29, "Descripcion": "...", "VActiva": false } ]`.
> * *Acción*: Solamente se realiza el cambio de la URL base y bandera del Mock en la configuración.

> [!WARNING]
> **Escenario B: El contrato JSON cambia de nombres de campos**
> * *Ejemplo de estructura modificada*: `[ { "id_movimiento": 29, "nombre": "...", "activo": 0 } ]`.
> * *Acción de Mitigación*: 
>   * Gracias al desacoplamiento, no es necesario tocar la vista.
>   * Se actualiza la clase [Movimiento.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Models/Movimiento.cs) utilizando los atributos de mapeo de nombres de JSON de System.Text.Json (`[JsonPropertyName("id_movimiento")]`) en las propiedades correspondientes:
>     ```csharp
>     [JsonPropertyName("id_movimiento")]
>     public int Codigo { get; set; }
>     ```
>   * De este modo, el frontend de Blazor sigue consumiendo la propiedad `Codigo` de forma transparente y sin enterarse de que el origen JSON se modificó.

> [!CAUTION]
> **Escenario C: El contrato JSON cambia de tipos o lógica compleja**
> * *Ejemplo*: La API ya no devuelve una lista plana, sino un objeto paginado ` { "total": 3, "data": [...] }`.
> * *Acción de Mitigación*:
>   * Se modifica únicamente el método `ObtenerMovimientosAsync()` dentro de [ApiMovimientoService.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Services/ApiMovimientoService.cs) para extraer la lista interna antes de retornarla.
>   * El frontend sigue recibiendo su contrato `Task<List<Movimiento>>` sin sufrir ninguna alteración, aislando el impacto del cambio al 100%.
