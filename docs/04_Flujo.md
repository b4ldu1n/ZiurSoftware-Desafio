# 04 - Diagrama de Flujo de Datos

Este documento describe de manera visual mediante diagramas ASCII el flujo de datos de la solución utilizando la Minimal API HTTP.

---

## Flujo de Datos utilizando la API (HTTP Minimal API)

Dado que la aplicación consume de forma exclusiva los datos contables desde el servidor REST local, el flujo de solicitudes y respuestas de red se detalla a continuación:

```
+--------------------------------------------------------+
|                      Usuario                           |
+--------------------------------------------------------+
                           │
                           │ 1. Navega a /movimientos
                           ▼
+--------------------------------------------------------+
|                 Movimientos.razor                      |
+--------------------------------------------------------+
                           │
                           │ 2. Inicia OnInitializedAsync()
                           ▼
+--------------------------------------------------------+
|          IMovimientoService (Inyección)                |
|           -> Resuelto: ApiMovimientoService            |
+--------------------------------------------------------+
                           │
                           │ 3. Llama a ObtenerMovimientosAsync()
                           ▼
+--------------------------------------------------------+
|                    HttpClient                          |
+--------------------------------------------------------+
                           │
                           │ 4. GET /api/movimientos (por puerto 5199)
                           ▼
+========================================================+
|                 Minimal API (Backend)                  |
|               (ZiurSoftwareChallenge.Api)              |
+========================================================+
                           │
                           │ 5. Procesa el GET y genera List
                           │ 6. Serializa a JSON (PascalCase)
                           ▼
+--------------------------------------------------------+
|                   Respuesta JSON                       |
|           HTTP 200 OK con payload exacto               |
+--------------------------------------------------------+
                           │
                           │ 7. Retorna por canal de red
                           ▼
+--------------------------------------------------------+
|                    HttpClient                          |
|         (Deserializa a List<Movimiento>)               |
+--------------------------------------------------------+
                           │
                           │ 8. Retorna la lista deserializada
                           ▼
+--------------------------------------------------------+
|                 Movimientos.razor                      |
|           (Pinta la tabla con @foreach)                |
+--------------------------------------------------------+
                           │
                           │ 9. Renderiza HTML en pantalla
                           ▼
+--------------------------------------------------------+
|                     Tabla HTML                         |
+--------------------------------------------------------+
```
