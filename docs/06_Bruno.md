# 06 - Pruebas con la Colección de Bruno

Este documento describe de manera clara cómo importar la colección de pruebas en **Bruno**, seleccionar el entorno de desarrollo y verificar el comportamiento correcto de la Minimal API mediante aserciones automatizadas.

## ¿Qué es Bruno?

**Bruno** es un cliente HTTP Open Source innovador y liviano (alternativa a Postman o Insomnia). Bruno guarda las colecciones en archivos de texto plano utilizando un lenguaje de marcado propio llamado Bru. Esto permite guardar las peticiones y environments directamente dentro de la carpeta del proyecto y sincronizarlos a través de Git de forma muy limpia.

---

## Importación de la Colección

1. Abre la aplicación de escritorio **Bruno**.
2. En la pantalla de bienvenida, haz clic en la opción **Open Collection** (Abrir Colección).
3. Selecciona la carpeta **`bruno`** localizada en la raíz de este proyecto (`c:\Users\Lenovo\ZiurSoftwareChallenge\bruno`).
4. Verás aparecer en el panel izquierdo la colección cargada con el nombre del proyecto y la petición `Obtener Movimientos`.

---

## Selección del Entorno (Environment)

Para que las variables como `{{baseUrl}}` se resuelvan correctamente hacia la dirección local de la Minimal API:
1. Dirígete a la esquina superior derecha de la interfaz de Bruno.
2. Haz clic en el selector de entornos (por defecto se muestra "No Environment").
3. Selecciona el entorno llamado **Development**.
4. Puedes inspeccionar las variables del entorno haciendo clic en el icono del ojo o configurando los ajustes; allí verás que `baseUrl` apunta a `http://localhost:5199`.

---

## Ejecución del GET y Aserciones Automáticas

1. Selecciona la petición **Obtener Movimientos** en la colección del panel izquierdo.
2. Asegúrate de que la Minimal API esté corriendo en una terminal activa (`dotnet run --project src/ZiurSoftwareChallenge.Api`).
3. Haz clic en el botón de envío **Send** (botón de flecha hacia la derecha).
4. Verás la respuesta en el panel derecho:
   * **Status Code**: `200 OK`.
   * **Tiempo de respuesta**: usualmente menos de 10 ms.
   * **Cuerpo de Respuesta**: la lista exacta de tres movimientos en formato JSON.

---

## Cómo Validar el JSON y el Estado HTTP

En la pestaña **Assert** (Aserciones) de la petición, se configuran las siguientes reglas de pruebas automáticas:

| Expresión de Validación | Operador | Valor Esperado | Propósito |
| :--- | :--- | :--- | :--- |
| `res.status` | `eq` (igual a) | `200` | Asegura que la API responda satisfactoriamente. |
| `res.body` | `isArray` | (Verdadero) | Valida que la respuesta sea un arreglo serializado. |
| `res.body[0].Codigo` | `eq` | `29` | Valida que el primer elemento sea el movimiento 29. |
| `res.body[1].Codigo` | `eq` | `51` | Valida que el segundo elemento sea el movimiento 51. |
| `res.body[2].Codigo` | `eq` | `17` | Valida que el tercer elemento sea el movimiento 17. |

Si las aserciones pasan correctamente, verás un círculo verde al lado de la pestaña **Tests** o **Assert** en Bruno indicando que todas las aserciones han sido exitosas.

---

## Detección y Resolución de Errores Comunes

* **Error: `Connection Refused` o `ECONNREFUSED`**:
  * *Causa*: La Minimal API no se está ejecutando o está en un puerto distinto al configurado.
  * *Solución*: Ejecuta `dotnet run --project src/ZiurSoftwareChallenge.Api` y verifica la consola para confirmar el puerto HTTP activo.
* **Error de Certificado SSL / `depth zero self signed certificate`** (si usas la URL de HTTPS en lugar de HTTP):
  * *Causa*: El cliente Bruno no confía en el certificado local autofirmado de ASP.NET Core.
  * *Solución*:
    1. Ejecuta `dotnet dev-certs https --trust` en la terminal.
    2. O de forma alternativa, desactiva la verificación SSL en Bruno: ve a `Preferences` en Bruno y desmarca la opción **SSL Certificate Verification**.
* **Las Aserciones Fallan (ej. `res.body[0].Codigo` is undefined)**:
  * *Causa*: La API está devolviendo las propiedades con minúsculas (camelCase) como `codigo` en lugar del PascalCase esperado `Codigo`.
  * *Solución*: Asegúrate de que la configuración global de serialización en el backend esté activa en [Program.cs](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge.Api/Program.cs):
    ```csharp
    builder.Services.ConfigureHttpJsonOptions(options => {
        options.SerializerOptions.PropertyNamingPolicy = null;
    });
    ```
