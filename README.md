<<<<<<< HEAD
# Ziur Software Challenge - Solución Completa (.NET 9)

¡Bienvenido al repositorio de la solución del desafío técnico de selección de **Ziur Software**! Este proyecto implementa una grilla interactiva en **Blazor Web App (.NET 9)** que consume datos contables y de inventario de forma asíncrona, conectándose mediante un cliente HTTP seguro a la API real de Ziur, e implementando un mecanismo automático de redundancia de datos (Fallback).

---

## Índice de Documentación Detallada

Para una revisión arquitectónica profunda, consulte los archivos individuales ubicados en la carpeta [docs/](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/):

1.  [**01 - Objetivo del Proyecto**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/01_Objetivo.md): Objetivos del reto, alcances y especificación del JSON.
2.  [**02 - Configuración del Entorno**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/02_Entorno.md): Descarga de SDKs, IDEs, certificados SSL y herramientas de desarrollo.
3.  [**03 - Arquitectura y Diseño Técnico**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/03_Arquitectura.md): Inyección de dependencias en cadena, Clean Architecture, Decorator Pattern y principios SOLID.
4.  [**04 - Flujo de Datos**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/04_Flujo.md): Diagramas de secuencia ASCII del flujo de datos HTTP y Contingencias.
5.  [**05 - Especificación de la Minimal API**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/05_API.md): Endpoints expuestos, puertos de red locales y personalización de JSON.
6.  [**06 - Pruebas con Bruno**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/06_Bruno.md): Guía paso a paso de importación de colecciones de Bruno y ejecución de aserciones.
7.  [**07 - Guía de Ejecución desde Terminal**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/07_Ejecucion.md): Todos los comandos de CLI detallados para clonar, restaurar, compilar y ejecutar.
8.  [**08 - Configuración del Cliente HTTP y Fallback**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/08_CambiarMockPorAPI.md): Detalles técnicos de User Secrets de .NET y control de fallas en red.
9.  [**09 - Walkthrough Chronological**](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/09_Walkthrough.md): Bitácora cronológica del desarrollo incremental del reto.

---

## 1. Arquitectura y Mecanismo de Fallback (Tolerancia a Fallos)

El proyecto está diseñado bajo principios de acoplamiento débil (Loose Coupling):
*   **IMovimientoService**: Abstracción del servicio.
*   **ApiMovimientoService**: Implementación encargada de conectarse al servidor de Ziur por HTTP y agregar dinámicamente las cabeceras de autenticación parametrizadas.
*   **MockMovimientoService**: Implementación local en memoria que sirve como respaldo.
*   **FallbackMovimientoService (Decorator)**: Clase Wrapper que intenta llamar al servicio HTTP de producción. En caso de fallo (caída de red, token inválido o error HTTP), captura la excepción de forma segura y devuelve de forma automática los datos Mock de respaldo para garantizar la estabilidad de la grilla.

---

## 2. Tecnologías Utilizadas

*   **Backend**: ASP.NET Core Minimal API (.NET 9) (para simulación inicial).
*   **Frontend**: Blazor Web App (Interactive Server render mode, .NET 9) conectado a API real.
*   **Estilado**: Bootstrap 5.
*   **Administrador de Credenciales**: .NET User Secrets.
*   **Cliente HTTP de Pruebas**: Bruno.

---

## 3. Instalación y Configuración Segura de Credenciales

### Prerrequisitos
Instale [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) en su máquina local.

### Paso 1: Configurar Certificado SSL
```bash
dotnet dev-certs https --trust
```

### Paso 2: Configurar URL de la API
El archivo [appsettings.json](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/appsettings.json) contiene los parámetros base de la API real de Ziur:
```json
"Api": {
  "BaseUrl": "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/",
  "Endpoint": "DocumentosFillsCombos",
  "AuthHeaderName": "Authorization",
  "AuthHeaderValueFormat": "Bearer {0}",
  "UseFallback": true
}
```

### Paso 3: Configurar el Token de Autenticación de forma Segura (User Secrets)
Nunca guarde contraseñas ni tokens directamente en archivos de código fuente o en `appsettings.json`. En su consola de comandos, ejecute lo siguiente:
```bash
# Inicializar los secretos en el proyecto
dotnet user-secrets init --project src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj

# Establecer su token manual localmente
dotnet user-secrets set "Api:AuthToken" "SU_TOKEN_MANUAL_AQUI" --project src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
```
El framework de .NET resolverá automáticamente la clave `Api:AuthToken` desde el almacén seguro de secretos local durante la ejecución.

---

## 4. Cómo Ejecutar el Proyecto

Abra la terminal en la raíz del repositorio y ejecute:

### Paso A: Restaurar dependencias con NuGet oficial
```bash
dotnet restore --source https://api.nuget.org/v3/index.json
```

### Paso B: Compilar toda la solución
```bash
dotnet build ZiurSoftwareChallenge.sln
```

### Paso C: Iniciar la Aplicación Blazor (Frontend)
```bash
dotnet run --project src/ZiurSoftwareChallenge
```
*   La aplicación estará disponible en: **`http://localhost:5250`** o **`https://localhost:7121`**
*   Navega a la ruta de la grilla: **`/movimientos`**

---

## 5. Cómo Probar la Aplicación

### 1. Validación de Fallback (API sin autenticar / sin conexión)
1.  Asegúrate de no tener configurado el token de autenticación (o establece un valor erróneo con `user-secrets`).
2.  Inicia el frontend Blazor y navega a `/movimientos`.
3.  *Resultado esperado*: El sistema intentará consumir la API de Ziur, fallará debido a la falta de token válido, y el decorador capturará el error de forma segura en segundo plano, devolviendo la lista de movimientos Mock (Ajuste al Inventario, Avance Producción, Balance Inicial) de forma automática.

### 2. Validación de Conexión Real (API con autenticación)
1.  Establece un Token válido usando el comando `dotnet user-secrets set "Api:AuthToken" "TU_TOKEN"`.
2.  Recarga la página `/movimientos` en el navegador.
3.  *Resultado esperado*: El cliente HTTP inyectará la cabecera `Authorization: Bearer <TU_TOKEN>`, consumirá con éxito el endpoint `DocumentosFillsCombos` del servidor de Ziur y poblará la grilla con los datos reales devueltos.
=======
# ZiurSoftware-Desafio
conexion con API
>>>>>>> 2148827cd94135b81577eac487d524975fe1821e
