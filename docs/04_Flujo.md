# 04 - Diagrama de Flujo de Datos

Este documento describe de manera visual mediante diagramas ASCII el flujo de datos tanto para el caso exitoso de consulta a la API real, como para el flujo de contingencia (Fallback al Mock) ante errores de red o credenciales.

---

## 1. Flujo Exitoso: Consumo Directo de la API Real

Cuando las credenciales son correctas y el servidor de Ziur responde exitosamente.

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
                           │ 2. Llama a IMovimientoService
                           ▼
+--------------------------------------------------------+
|          FallbackMovimientoService (Wrapper)           |
+--------------------------------------------------------+
                           │
                           │ 3. Invoca a ApiMovimientoService
                           ▼
+--------------------------------------------------------+
|          ApiMovimientoService (HTTP Client)            |
+--------------------------------------------------------+
                           │
                           │ 4. Lee Token de User Secrets / Entorno
                           │ 5. GET /api/DocumentosFillsCombos con Token
                           ▼
+========================================================+
|                    API Real de Ziur                    |
|             (mainserver.ziursoftware.com)              |
+========================================================+
                           │
                           │ 6. Valida Token y retorna JSON
                           ▼
+--------------------------------------------------------+
|              HTTP 200 OK con JSON real                 |
+--------------------------------------------------------+
                           │
                           │ 7. Retorna la lista deserializada
                           ▼
+--------------------------------------------------------+
|          FallbackMovimientoService (Wrapper)           |
|            (Retorna datos reales a la UI)              |
+--------------------------------------------------------+
                           │
                           │ 8. Renderiza en pantalla
                           ▼
+--------------------------------------------------------+
|                     Tabla HTML                         |
+--------------------------------------------------------+
```

---

## 2. Flujo de Contingencia: Fallback Automático a Datos Mock

Cuando la API falla (ejemplo: HTTP 401 Unauthorized, timeout de red o DNS inaccesible).

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
                           │ 2. Llama a IMovimientoService
                           ▼
+--------------------------------------------------------+
|          FallbackMovimientoService (Wrapper)           |
+--------------------------------------------------------+
                           │
                           │ 3. Invoca a ApiMovimientoService
                           ▼
+--------------------------------------------------------+
|          ApiMovimientoService (HTTP Client)            |
+--------------------------------------------------------+
                           │
                           │ 4. Intenta GET a API de Ziur
                           ▼
+========================================================+
|               API Real (Caída o Sin Auth)              |
+========================================================+
                           │
                           │ 5. Retorna HTTP 401 / Exception de red
                           ▼
+--------------------------------------------------------+
|          ApiMovimientoService (HTTP Client)            |
|              (Arroja Exception hacia arriba)           |
+--------------------------------------------------------+
                           │
                           │ 6. Captura Excepción en try-catch
                           │ 7. Llama a MockMovimientoService (Fallback)
                           ▼
+--------------------------------------------------------+
|          MockMovimientoService (En memoria)            |
|          (Retorna lista predeterminada 29, 51, 17)     |
+--------------------------------------------------------+
                           │
                           │ 8. Retorna datos seguros a la UI
                           ▼
+--------------------------------------------------------+
|                 Movimientos.razor                      |
|                  (Pinta los datos)                     |
+--------------------------------------------------------+
```
