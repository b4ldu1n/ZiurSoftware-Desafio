# 06 - Guía de Puesta en Marcha y Despliegue Local (Deployment)

Este documento detalla las instrucciones paso a paso para descargar, compilar y ejecutar la aplicación **ZiurSoftwareChallenge** en cualquier computadora de desarrollo que cuente con .NET 9.

---

## 1. Requisitos Previos
* Tener instalado el SDK de **.NET 9.0** o superior.
* Tener instalado **Git**.

---

## 2. Instrucciones de Ejecución Paso a Paso

Siga estos comandos en su terminal (PowerShell, Bash o CMD) para poner en marcha el proyecto:

### Paso A: Clonar el Repositorio
```bash
git clone https://github.com/tu-usuario/ZiurSoftwareChallenge.git
cd ZiurSoftwareChallenge
```

### Paso B: Restaurar Dependencias NuGet
Descarga las librerías necesarias del proyecto (incluyendo librerías de ASP.NET Core e inyección):
```bash
dotnet restore
```

### Paso C: Compilar la Solución
Verifica que todo el código fuente se compile de manera correcta antes del despliegue:
```bash
dotnet build
```

### Paso D: Ejecutar el Servidor Web
Inicia la aplicación en modo servidor local auto-hospedado (Kestrel):
```bash
dotnet run --project src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
```

---

## 3. Acceso a la Aplicación

Una vez que la consola muestre que el servidor está escuchando peticiones, abra su navegador e ingrese a las siguientes rutas:

* **Dirección Base de la Aplicación**:
  * Servidor seguro (Recomendado): `https://localhost:7196`
  * Servidor estándar: `http://localhost:5250`
* **Acceso Directo a la Grilla de Movimientos**:
  * Ruta: `https://localhost:7196/movimientos`

---

## 4. Limpieza de Puertos y Ejecuciones en Windows

Si al ejecutar el comando de build o run obtiene un mensaje de error que indica que los archivos binarios están bloqueados:
```text
The process cannot access the file '...ZiurSoftwareChallenge.exe' because it is being used by another process.
```
Esto ocurre si una ejecución previa de la aplicación no se detuvo correctamente. Puede liberar el puerto y el archivo corriendo el siguiente comando en PowerShell:
```powershell
taskkill /F /IM ZiurSoftwareChallenge.exe
```
Posteriormente, intente compilar (`dotnet build`) o ejecutar (`dotnet run`) de nuevo de forma segura.
