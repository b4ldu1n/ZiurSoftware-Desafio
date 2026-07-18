# Ziur Software Challenge - Blazor Web App con .NET 9

¡Bienvenido a la solución del desafío técnico de selección para **Ziur Software**!

Este proyecto consiste en una aplicación interactiva desarrollada en **Blazor Web App (.NET 9)** que consume datos de una API REST de movimientos contables y de inventario, presentándolos en una grilla responsiva de alto rendimiento.

---

## 1. Portafolio de Negocio: Mapeo de Movimientos

Los datos expuestos por la aplicación representan transacciones clave dentro del catálogo de productos y soluciones contables de Ziur Software:

* **Balance Inicial (Código 17)**: Habilitado en la versión **STANDARD** (módulo *Registro de Documentos Contables*). Se usa para cargar los saldos de apertura contable y del stock al iniciar operaciones.
* **Ajuste al Inventario (Código 29)**: Parte de la versión **STANDARD** (*Documentos Productos y Servicios*) y **ENTERPRISE** (*Contabilización automática*). Se usa para regularizar discrepancias físicas contra la contabilidad de almacenes.
* **Avance Producción (Código 51)**: Incluido en la versión **ENTERPRISE** (*Interfaz IPS y Taller Automotriz*). Se usa en la manufactura y servicios técnicos para documentar el progreso de fases en órdenes de producción.

---

## 2. Decisiones de Arquitectura e Ingeniería

Para asegurar una entrega de calidad senior, el proyecto se construyó bajo principios de arquitectura limpia:

* **Inversión de Dependencias (SOLID - DIP)**: La vista inyecta la abstracción `IMovimientoService` y desconoce la implementación de red.
* **Alternancia Dinámica de Servicios (OCP)**: A través de `appsettings.json`, se puede cambiar de la API real de Ziur al simulador Mock instantáneamente, sin recompilar ni alterar código C#.
* **Tolerancia a Cambios de Contrato**: El uso de deserialización personalizada y abstracciones aísla a la UI de futuros cambios de nombres o estructuras en el JSON de la API.

---

## 3. Documentación Técnica del Proyecto

Para una revisión en profundidad, consulte la suite de documentación detallada en la carpeta `docs/`:

1. [**00-EnvironmentSetup.md** (Configuración del Entorno)](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/00-EnvironmentSetup.md): Requisitos de software, SDK de .NET 9, Git, SSL y extensiones del IDE.
2. [**01-ProjectWalkthrough.md** (Flujo de Desarrollo)](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/01-ProjectWalkthrough.md): Secuencia de pasos implementados para construir la solución de inicio a fin.
3. [**02-Architecture.md** (Arquitectura y SOLID)](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/02-Architecture.md): Diagramas de flujo de datos, justificación de patrones y mapeo con la oferta comercial de Ziur.
4. [**03-Configuration.md** (Parámetros y appsettings)](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/03-Configuration.md): Guía de parámetros de control, toggles de Mock y variables de entorno para producción.
5. [**04-Testing.md** (Bitácora de Pruebas)](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/04-Testing.md): Evidencia de builds limpios y verificación manual de la UI en navegador.
6. [**05-ApiIntegration.md** (Estrategia de API Real)](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/05-ApiIntegration.md): Plan de contingencia, conmutación de endpoints y manejo de cambios de nombres en propiedades del JSON.
7. [**06-Deployment.md** (Guía de Despliegue Local)](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/docs/06-Deployment.md): Comandos del CLI para clonar, restaurar, compilar y ejecutar el portal.

---

## 4. Instrucciones de Inicio Rápido (Quickstart)

Ejecute los siguientes comandos en su terminal para iniciar la aplicación:

```bash
# 1. Clonar y posicionarse en la carpeta
git clone <URL_DEL_REPOSITORIO>
cd ZiurSoftwareChallenge

# 2. Restaurar paquetes NuGet
dotnet restore

# 3. Compilar
dotnet build

# 4. Iniciar aplicación
dotnet run --project src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
```

Abra el navegador en: **`https://localhost:7196/movimientos`**

---

## 5. Historial de Versiones
Para ver los detalles del desarrollo incremental, consulte el archivo [CHANGELOG.md](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/CHANGELOG.md).
