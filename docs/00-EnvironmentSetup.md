# 00 - Configuración del Entorno de Desarrollo (Environment Setup)

Este documento describe la configuración del entorno técnico utilizado para el desarrollo y validación de la aplicación **ZiurSoftwareChallenge**, garantizando un entorno reproducible y profesional.

---

## 1. Control de Versiones (Git)
Para el seguimiento de cambios y control de versiones, se instaló Git.
* **Comprobación de la versión**:
  ```bash
  git --version
  ```
  *Salida obtenida*: `git version 2.45.2.windows.1` (o versión instalada en el sistema de desarrollo).

---

## 2. Kit de Desarrollo .NET (SDK)
Se instaló la versión más reciente de **.NET SDK 9.0**, que incluye el compilador de C#, las herramientas de CLI de .NET y las dependencias de ASP.NET Core Blazor.
* **Comprobación de versión y entorno**:
  ```bash
  dotnet --info
  ```
  *Salida destacada*:
  * SDK de .NET: `9.0.100` (o superior)
  * Entorno de ejecución: `Microsoft.NETCore.App 9.0.0`
  * ASP.NET Core: `Microsoft.AspNetCore.App 9.0.0`

---

## 3. Configuración del Certificado SSL Local (HTTPS)
Para asegurar el canal de comunicación local simulando condiciones de producción en navegadores modernos, se configuró y confió el certificado SSL de desarrollo de .NET:
```bash
dotnet dev-certs https --trust
```
*Esto previene las advertencias de "Conexión no segura" en el navegador local al levantar el servidor de Blazor.*

---

## 4. Herramientas de Desarrollo (IDE)
Se estructuró el espacio de trabajo con los siguientes componentes de desarrollo:

### Visual Studio 2022 / Visual Studio Code
* **Cargas de trabajo instaladas (Workloads)**:
  * **Desarrollo web y de ASP.NET**: Necesario para compilar Blazor Web Apps.
  * **Desarrollo de escritorio de .NET** (opcional, para herramientas auxiliares).
* **Extensiones utilizadas en VS Code**:
  * **C# Dev Kit**: Soporte oficial de Microsoft para C#, depuración y exploración de soluciones.
  * **C#**: Resaltado de sintaxis, OmniSharp e IntelliSense.
  * **Blazor**: Resaltado y completado automático en archivos `.razor`.
  * **GitHub Copilot**: Herramienta de pair programming asistido.
  * **Bruno**: Cliente de API REST para probar los endpoints de desarrollo de forma aislada.

---

## 5. Inicialización de la Estructura del Proyecto
El proyecto se inicializó utilizando la CLI de .NET en el espacio de trabajo actual:
```bash
# Crear directorio principal
mkdir ZiurSoftwareChallenge
cd ZiurSoftwareChallenge

# Inicializar repositorio Git local
git init

# Crear la solución en blanco
dotnet new sln -n ZiurSoftwareChallenge

# Crear aplicación web Blazor (modo interactivo de servidor y renderizado de recursos estáticos habilitados)
dotnet new blazor -o src/ZiurSoftwareChallenge --interactivity Server

# Añadir el proyecto a la solución
dotnet sln add src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
```

**Resultado del Entorno**: El proyecto base quedó estructurado en la carpeta `src/ZiurSoftwareChallenge/` y listo para compilar con la CLI (`dotnet build`).
