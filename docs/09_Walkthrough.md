# 09 - Walkthrough Chronological (Bitácora de Desarrollo)

Este documento registra la secuencia histórica de pasos y decisiones de ingeniería seguidas para completar este reto técnico de forma limpia, robusta e incremental.

---

## 1. Verificación del Entorno Inicial e Instalación del SDK

* **Paso**: Validación de la instalación de .NET en el equipo del desarrollador.
* **Comando ejecutado**:
  ```bash
  dotnet --info
  ```
* **Resultado**: Confirmación de la instalación de .NET Core SDK 9.0 con runtimes habilitados para ASP.NET Core.

---

## 2. Creación y Estructura Base de la Solución

* **Paso**: Inicialización del archivo de solución principal y organización en carpetas físicas.
* **Resultado**:
  * Archivo de solución creado: `ZiurSoftwareChallenge.sln`.
  * Proyecto Blazor Server creado en `src/ZiurSoftwareChallenge/`.
  * Proyecto Minimal API creado en `src/ZiurSoftwareChallenge.Api/`.
  * Ambos proyectos enlazados al archivo de solución mediante comandos `dotnet sln add`.

---

## 3. Confianza de Certificados HTTPS locales

* **Paso**: Trust del certificado de desarrollo para evitar errores de red local en llamadas asíncronas bajo HTTPS.
* **Comando ejecutado**:
  ```bash
  dotnet dev-certs https --trust
  ```
* **Resultado**: Certificado autofirmado de desarrollo registrado y de confianza en el almacén de certificados de Windows.

---

## 4. Implementación Inicial del Servicio Mock en el Frontend

* **Paso**: Creación del contrato `IMovimientoService` y la clase `MockMovimientoService` en el directorio de servicios.
* **Propósito**: Permitir el diseño e implementación de la grilla en Blazor de forma aislada y ágil sin requerir dependencias de red en fases tempranas.

---

## 5. Diseño de la Interfaz Web y Grilla (Blazor Components)

* **Paso**: Creación de la vista principal de la grilla en la ruta `/movimientos` (`Movimientos.razor`).
* **Estilado**: Integración de Bootstrap CSS con clases estilizadas (`table`, `table-striped`, `table-hover`, `badge`) y manejo de estados visuales (Loading y Empty).

---

## 6. Creación y Configuración del Backend Minimal API

* **Paso**: Desarrollo del servidor REST HTTP ligero en `src/ZiurSoftwareChallenge.Api`.
* **Mejoras**:
  1. Configuración de CORS abierta (`AllowAll`) para eliminar problemas de bloqueo de origen en navegadores.
  2. Desactivación de la política camelCase global en JSON (`PropertyNamingPolicy = null`) para cumplir con la especificación exacta de retornar claves en PascalCase (`Codigo`, `Descripcion`, `VActiva`).
  3. Registro del endpoint `GET /api/movimientos` retornando el payload estático final.
  4. Ejecución exitosa de `dotnet build` con cero advertencias y cero errores.

---

## 7. Integración de HttpClient y Consumo Real HTTP

* **Paso**: Habilitación del servicio de red `ApiMovimientoService` en el frontend, apuntando en `appsettings.json` a `http://localhost:5199/`.

---

## 8. Creación de Pruebas de API Automatizadas (Bruno)

* **Paso**: Creación de los archivos de entorno y requests HTTP en la carpeta de la colección `bruno/`.
* **Pruebas integradas**:
  * Solicitud GET parametrizada a `{{baseUrl}}/api/movimientos`.
  * Pruebas y aserciones automatizadas de código HTTP 200 y valores exactos del JSON.

---

## 9. Depuración: Remoción del Servicio Mock para Integración Pura API

* **Paso**: Refactorización técnica para remover por completo el soporte del Mock en el código fuente.
* **Cambios**:
  * Eliminación de la propiedad `UseMock` en `appsettings.json`.
  * Simplificación de `Program.cs` del frontend para inyectar directamente `ApiMovimientoService` vía `AddHttpClient`.
  * Eliminación física del archivo `MockMovimientoService.cs` de la carpeta de servicios.
  * Recompilación completa de la solución.
* **Resultado**: La aplicación Blazor queda configurada de forma limpia y lista para producción, consumiendo datos únicamente de la API.
