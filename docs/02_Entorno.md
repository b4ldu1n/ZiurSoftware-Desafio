# 02 - Configuración del Entorno de Desarrollo

Este documento detalla paso a paso los requisitos previos de software, instalación de herramientas, inicialización de la solución y preparación del entorno HTTPS en la máquina local.

## Requisitos de Software y Enlaces de Descarga

Para desarrollar, ejecutar y probar este proyecto se requiere instalar las siguientes herramientas:

1. **.NET SDK 9.0**:
   * Descarga la última versión del SDK de .NET 9 desde el sitio oficial de Microsoft.
   * [Descargar .NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
2. **Visual Studio Code**:
   * Editor liviano multiplataforma recomendado para el desarrollo de la solución.
   * [Descargar VS Code](https://code.visualstudio.com/)
3. **Extensiones necesarias para VS Code**:
   * Abre VS Code, presiona `Ctrl+Shift+X` e instala las siguientes extensiones:
     * **C# Dev Kit**: Soporte oficial de Microsoft para proyectos .NET (compilación, ejecución, testing, formateo).
     * **C#**: Extensión base para lenguajes C#.
4. **Git**:
   * Sistema de control de versiones distribuido.
   * [Descargar Git](https://git-scm.com/)
5. **Bruno**:
   * Cliente HTTP ligero y Open Source para importar la colección de pruebas ubicada en la carpeta `bruno` del repositorio y realizar las comprobaciones.
   * [Descargar Bruno](https://www.usebruno.com/)

---

## Verificación de la Instalación de .NET

Una vez completada la instalación del SDK de .NET 9, abre una terminal (PowerShell o CMD en Windows) y ejecuta el siguiente comando para verificar que esté correctamente instalado en el PATH global:

```bash
dotnet --info
```

Deberías ver una salida detallada confirmando la versión `9.0.x` instalada y los runtimes activos para ASP.NET Core y .NET Desktop.

---

## Configuración y Confianza de Certificados HTTPS locales

Para habilitar el protocolo HTTPS en las ejecuciones locales de ASP.NET Core y evitar errores de certificado inseguro (SSL handshake error) en el navegador o en Bruno, ejecuta el siguiente comando en la terminal:

```bash
dotnet dev-certs https --trust
```

> [!NOTE]
> En Windows, este comando abrirá una ventana emergente del sistema preguntándote si deseas confiar en el certificado de desarrollo autofirmado de IIS/ASP.NET Core. Haz clic en **Sí** para confirmar.

---

## Estructura de Carpetas de la Solución

El proyecto sigue una organización estándar para soluciones de Clean Architecture y Minimal API:

* **`/`** (Raíz): Contiene el archivo de solución `ZiurSoftwareChallenge.sln`, archivos de Git, el README.md principal y la carpeta de tareas.
* **`/src`**: Directorio con el código fuente de los proyectos.
  * **`src/ZiurSoftwareChallenge`**: Frontend Blazor Web App (Interactive Server render mode).
  * **`src/ZiurSoftwareChallenge.Api`**: Backend Minimal API en ASP.NET Core que simula los datos HTTP.
* **`/bruno`**: Archivos de la colección de pruebas locales de Bruno (`.bru` y configuraciones del entorno).
* **`/docs`**: Documentación técnica detallada en formato Markdown.
* **`/screenshots`**: Capturas de pantalla que demuestran el funcionamiento y validación de la grilla.

---

## Comandos Iniciales de Inicialización (Referencia Técnica)

Si se requiriera recrear la estructura del proyecto desde cero, el procedimiento estándar utilizando el CLI de .NET es:

1. **Crear carpeta de solución e inicializar**:
   ```bash
   mkdir ZiurSoftwareChallenge
   cd ZiurSoftwareChallenge
   dotnet new sln
   ```
2. **Crear proyectos en la carpeta `src`**:
   ```bash
   mkdir src
   cd src
   dotnet new blazor -o ZiurSoftwareChallenge
   dotnet new webapi -o ZiurSoftwareChallenge.Api --use-program-main false
   ```
3. **Agregar proyectos a la solución**:
   ```bash
   cd ..
   dotnet sln add src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
   dotnet sln add src/ZiurSoftwareChallenge.Api/ZiurSoftwareChallenge.Api.csproj
   ```
4. **Inicializar repositorio Git**:
   ```bash
   git init
   git add .
   git commit -m "Initial commit"
   ```

---

## Compilación y Primera Ejecución

Para compilar toda la solución, ejecuta en la raíz del proyecto:

```bash
dotnet build
```

Esto compilará tanto la Minimal API como el Frontend Blazor de forma simultánea. Para levantar cada proyecto por separado, revisa las instrucciones específicas del archivo [07_Ejecucion.md](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/07_Ejecucion.md).
