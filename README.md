# Ziur Software Challenge - Solución Completa (.NET 9)

¡Bienvenido al repositorio de la solución del desafío técnico de selección de **Ziur Software**! Este proyecto implementa una grilla interactiva en **Blazor Web App (.NET 9)** que consume datos contables y de inventario de forma asíncrona, conectándose mediante un cliente HTTP a una simulación de API REST local desarrollada con **ASP.NET Core Minimal API**.

---

## Índice de Documentación Detallada

Para una revisión arquitectónica profunda, consulte los archivos individuales ubicados en la carpeta [docs/](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/):

1. [**01 - Objetivo del Proyecto**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/01_Objetivo.md): Objetivos del reto, alcances y especificación del JSON.
2. [**02 - Configuración del Entorno**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/02_Entorno.md): Descarga de SDKs, IDEs, certificados SSL y herramientas de desarrollo.
3. [**03 - Arquitectura y Diseño Técnico**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/03_Arquitectura.md): Inyección de dependencias, Clean Architecture y principios SOLID.
4. [**04 - Flujo de Datos**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/04_Flujo.md): Diagrama de secuencia ASCII del flujo de datos HTTP.
5. [**05 - Especificación de la Minimal API**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/05_API.md): Endpoints expuestos, puertos de red locales y personalización de JSON.
6. [**06 - Pruebas con Bruno**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/06_Bruno.md): Guía paso a paso de importación de colecciones de Bruno y ejecución de aserciones.
7. [**07 - Guía de Ejecución desde Terminal**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/07_Ejecucion.md): Todos los comandos de CLI detallados para clonar, restaurar, compilar y ejecutar.
8. [**08 - Configuración del Cliente HTTP**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/08_CambiarMockPorAPI.md): Detalles técnicos y archivos involucrados en la integración con la API.
9. [**09 - Walkthrough Chronological**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/09_Walkthrough.md): Bitácora cronológica del desarrollo incremental del reto.

---

## 1. Objetivo del Reto

El reto consiste en rellenar y consumir una grilla contable en Blazor que muestre tres movimientos específicos:
* **Ajuste al Inventario (Código 29)**: Utilizado para regularizar stock físico.
* **Avance Producción (Código 51)**: Utilizado en órdenes de fabricación y taller.
* **Balance Inicial (Código 17)**: Utilizado al iniciar operaciones comerciales.

La conexión se realiza directamente a través de HTTP contra una **Minimal API** que emula de forma real el comportamiento del backend esperado en producción.

---

## 2. Tecnologías Utilizadas

* **Backend**: ASP.NET Core Minimal API (.NET 9)
* **Frontend**: Blazor Web App (Interactive Server render mode, .NET 9)
* **Estilado**: Bootstrap 5 (CSS Vanilla e integrado en Blazor)
* **IDE**: Visual Studio Code / Visual Studio 2022
* **Control de Versiones**: Git
* **Cliente HTTP de Pruebas**: Bruno (con aserciones automáticas en archivos `.bru`)

---

## 3. Arquitectura del Proyecto

El proyecto está diseñado bajo principios de acoplamiento débil (Loose Coupling):
* **Capa de Negocio (Abstracción)**: Define la interfaz `IMovimientoService` con el contrato de datos.
* **Capa de Datos (Red)**: Implementa la interfaz a través de `ApiMovimientoService` ejecutando peticiones asíncronas REST.
* **Capa de API (Backend)**: Minimal API en un puerto de red local independiente (`http://localhost:7000`) configurado globalmente para retornar JSON con claves en **PascalCase** tal como lo pide Ziur.

---

## 4. Instalación y Configuración

### Prerrequisitos
Instala [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) en tu sistema operativo.

### Configuración del Certificado SSL
Ejecuta en tu consola para habilitar la confianza del protocolo HTTPS localmente:
```bash
dotnet dev-certs https --trust
```

### Configuración del Archivo `appsettings.json` del Frontend
El archivo [appsettings.json](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/appsettings.json) especifica si usar simulación o API, y la dirección base de la API:
```json
"Api": {
  "UseMock": false,
  "BaseUrl": "http://localhost:7000"
}
```
- `UseMock: false` → Usa `ApiMovimientoService` para consumir HTTP
- `UseMock: true` → Usa `MockMovimientoService` (datos en memoria)

---

## 5. Cómo Ejecutar el Proyecto

Abra una o dos terminales en la raíz del repositorio y ejecute los siguientes comandos:

### Paso 1: Restaurar dependencias con NuGet.org explícito
```bash
dotnet restore --source https://api.nuget.org/v3/index.json
```

### Paso 2: Compilar toda la solución
```bash
dotnet build
```

### Paso 3: Iniciar la Minimal API (Backend)
```bash
dotnet run --project src/ZiurSoftwareChallenge.Api/ZiurSoftwareChallenge.Api.csproj
```
* La API estará escuchando en: **`http://localhost:7000/api/movimientos`**

### Paso 4: Iniciar la Aplicación Blazor (Frontend)
```bash
dotnet run --project src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
```
* La aplicación estará disponible en: **`http://localhost:5255`** o **`https://localhost:7156`**
* Navega a la sección de **Movimientos** desde el menú o directamente a la ruta: **`/movimientos`**

---

## 6. Cómo Probar la Aplicación

### Comprobación en Navegador
1. Levanta ambos proyectos.
2. Abre la URL `http://localhost:7000/api/movimientos` en el navegador y verifica que responda con la grilla en formato JSON exacta.
3. Abre `http://localhost:5255/movimientos` (o puerto https: `https://localhost:7156/movimientos`) en el navegador y verifica que la grilla dibuje la tabla con los tres registros contables.

---

## 7. Pruebas Automáticas con Bruno

1. Instala e inicia la herramienta **Bruno**.
2. Presiona **Open Collection** y selecciona la carpeta **`bruno`** localizada en la raíz de la solución.
3. En la esquina superior derecha, selecciona el ambiente **Development**.
4. Selecciona la petición GET `Obtener Movimientos` y presiona **Send**.
5. Las aserciones integradas validarán automáticamente:
   * Código de estado HTTP igual a `200`.
   * Estructura del cuerpo como un arreglo (`isArray`).
   * Validación exacta del orden y valores de las claves del JSON (`Codigo` = 29, 51, 17).

---

## 8. Capturas del Funcionamiento

### Vista de la Grilla Responsiva en Blazor
*(Cargados los datos desde el servidor API local)*

![Grilla de Movimientos](screenshots/grid_screenshot.png)

### Validación del Endpoint JSON en la API
*(Devolviendo los datos en PascalCase)*

![Minimal API JSON](screenshots/api_screenshot.png)

### Pruebas de Integración con Bruno
*(Verificación de aserciones exitosas)*

![Pruebas en Bruno](screenshots/bruno_screenshot.png)

---

## 9. Posibles Mejoras

1. **Paginación y Filtrado del lado del Servidor**: Si la lista de movimientos escala a miles de registros, implementar parámetros en el endpoint `GET /api/movimientos?page=1&pageSize=10` para optimizar el rendimiento de la grilla.
2. **Implementación de Base de Datos Real**: Conectar la Minimal API con un motor liviano como SQLite usando Entity Framework Core para hacer persistente la información de los movimientos contables.
3. **Mecanismo de Reintento con Polly**: En el servicio `ApiMovimientoService`, integrar políticas de resiliencia y reintentos automáticos para mitigar caídas de red transitorias hacia la API.
4. **Contenedorización con Docker**: Configurar archivos `Dockerfile` y un `docker-compose.yml` para levantar todo el stack técnico (Frontend, API) con un solo comando `docker-compose up`.
