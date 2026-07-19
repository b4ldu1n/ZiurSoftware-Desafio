# 07 - Guía de Ejecución desde la Terminal

Este documento describe detalladamente los comandos necesarios para clonar, compilar y ejecutar todo el sistema (tanto el backend Minimal API como el frontend Blazor) directamente utilizando una terminal (CLI), ideal para flujos de integración continua o despliegue rápido.

---

## Flujo de Comandos Paso a Paso

Si te encuentras en una máquina limpia e instalas los requisitos de software del sistema, el flujo de ejecución completo en la terminal es:

```bash
# 1. Clonar el repositorio desde GitHub
git clone https://github.com/tu-usuario/ZiurSoftwareChallenge.git
cd ZiurSoftwareChallenge

# 2. Restaurar dependencias del proyecto especificando nuget.org si es necesario
dotnet restore --source https://api.nuget.org/v3/index.json

# 3. Compilar la solución entera
dotnet build

# 4. Iniciar la Minimal API (Backend) en segundo plano o en otra pestaña de la terminal
dotnet run --project src/ZiurSoftwareChallenge.Api

# 5. Iniciar la aplicación Web Blazor (Frontend) en otra terminal
dotnet run --project src/ZiurSoftwareChallenge
```

---

## Explicación Detallada de cada Comando

### 1. `git clone <url-del-repositorio>`
Descarga una copia completa del repositorio Git a tu directorio local, incluyendo todo el historial de commits, carpetas del backend, frontend, colecciones de Bruno y la documentación.

### 2. `dotnet restore --source <url-origen>`
Este comando descarga e instala todas las dependencias y paquetes NuGet especificados en los archivos de proyecto (`.csproj`) de la solución.
* **¿Por qué especificamos la fuente?**: En algunos entornos de desarrollo corporativos, la fuente oficial de NuGet.org puede estar deshabilitada en la configuración global de la máquina. Agregar `--source https://api.nuget.org/v3/index.json` garantiza que .NET busque e instale siempre los paquetes oficiales desde el repositorio centralizado de NuGet.

### 3. `dotnet build`
Compila el código fuente de toda la solución `ZiurSoftwareChallenge.sln`. Analiza la sintaxis, valida las referencias de proyectos y dependencias de paquetes, y compila los binarios (`.dll` y `.exe`) de depuración en las carpetas `bin/Debug/` correspondientes. Si hay algún error en el código C#, el proceso fallará aquí.

### 4. `dotnet run --project <ruta-al-proyecto>`
Compila (si es necesario) y ejecuta el proyecto .NET especificado de forma local.
* Para el backend: `dotnet run --project src/ZiurSoftwareChallenge.Api` inicia el servidor web embebido Kestrel y expone la API en los puertos configurados (usualmente `http://localhost:5199`).
* Para el frontend: `dotnet run --project src/ZiurSoftwareChallenge` levanta el servidor web del frontend de Blazor en `http://localhost:5250` (o `https://localhost:7121`) y abre el navegador automáticamente para ver la aplicación web.

---

## Ejecución Simultánea (Trucos Útiles para la Consola)

Si quieres ejecutar ambos proyectos simultáneamente usando una sola terminal, puedes abrir múltiples pestañas o usar herramientas en segundo plano en sistemas Unix (`&`) o PowerShell.

En Windows PowerShell, puedes abrir dos ventanas nuevas de ejecución de forma limpia usando el comando `Start-Process`:

```powershell
# Levantar la API en una nueva ventana
Start-Process dotnet -ArgumentList "run --project src/ZiurSoftwareChallenge.Api"

# Levantar el Frontend Blazor en una nueva ventana
Start-Process dotnet -ArgumentList "run --project src/ZiurSoftwareChallenge"
```
Esto mantendrá los logs de depuración organizados en terminales separadas, permitiéndote ver en vivo las solicitudes que realiza el frontend a la API.
