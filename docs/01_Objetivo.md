# 01 - Objetivo del Proyecto

Este documento describe el objetivo general del reto técnico de Ziur Software, los requisitos funcionales y no funcionales, y el alcance de la simulación de la API mediante HTTP en lugar de usar servicios puramente en memoria.

## Objetivo del Reto

El objetivo principal es completar las partes faltantes de una aplicación web construida con **Blazor Web App (.NET 9)** que consuma una API REST y muestre una grilla de movimientos con una estructura JSON bien definida.

Dado que la empresa no suministró la API real para el desarrollo, este reto incluye la creación de un backend simulado mediante una **Minimal API** en ASP.NET Core que sirva como una réplica real sobre HTTP.

## Qué pide Ziur

1. **Estructura del JSON**: El endpoint de movimientos debe devolver una respuesta JSON con el siguiente formato exacto (nombres de campos con mayúsculas en PascalCase):
   ```json
   [
     {
       "Codigo": 29,
       "Descripcion": "Ajuste al Inventario",
       "VActiva": false
     },
     {
       "Codigo": 51,
       "Descripcion": "Avance Produccion",
       "VActiva": false
     },
     {
       "Codigo": 17,
       "Descripcion": "Balance Inicial",
       "VActiva": false
     }
   ]
   ```
2. **Consumo HTTP en Blazor**: La aplicación Blazor debe consumir esta API mediante `HttpClient` de forma totalmente desacoplada del origen físico de los datos.
3. **Mantenimiento de la Arquitectura**: La interfaz `IMovimientoService` debe conservarse. Se debe poder alternar dinámicamente entre el servicio en memoria (`MockMovimientoService`) y el consumo real HTTP (`ApiMovimientoService`) a través del archivo de configuración `appsettings.json` mediante la propiedad `UseMock`.
4. **Colección de Pruebas**: Crear una colección de peticiones HTTP en **Bruno** para poder probar y validar el comportamiento de la Minimal API tal como si fuera el ambiente productivo final entregado por la empresa.

## Alcance del Reto

El alcance abarca:
* **Backend**: Creación del proyecto `ZiurSoftwareChallenge.Api` y exposición del endpoint `GET /api/movimientos`.
* **Frontend**: Conexión de `ApiMovimientoService` a la dirección local del backend e integración transparente con el componente `Movimientos.razor`.
* **Configuración**: Flexibilidad total en `appsettings.json` para encender/apagar el modo mock (`UseMock`) sin necesidad de alterar código C# de los componentes o la firma de la interfaz.
* **Testing**: Colección de pruebas automatizadas y aserciones básicas del estado HTTP y datos de respuesta usando Bruno.
* **Documentación**: Explicación técnica de arquitectura, flujo de datos, guías de instalación y despliegue del proyecto.

## Requisitos Técnicos

* **SDK**: .NET SDK 9.0 o superior.
* **Entorno**: ASP.NET Core Minimal API (.NET 9) y Blazor Web App (Interactive Server render mode, .NET 9).
* **SOLID**: Cumplimiento de los principios SOLID, en particular:
  * *Single Responsibility Principle (SRP)*: Componentes para renderizar la UI, servicios dedicados a la comunicación HTTP.
  * *Dependency Inversion Principle (DIP)*: Los controladores y componentes dependen de la abstracción `IMovimientoService`, no de las implementaciones concretas `MockMovimientoService` o `ApiMovimientoService`.
