# 02 - Arquitectura y Decisiones de Diseño (Architecture)

Este documento detalla el diseño de software de la aplicación **ZiurSoftwareChallenge**, justificando las decisiones de ingeniería de acuerdo con principios de diseño avanzados y contextualizándolas con el portafolio de negocio de la compañía.

---

## 1. Diagrama de Flujo de Datos

El recorrido de la información y la interacción de componentes en Blazor se estructuran de la siguiente manera:

```
[Usuario / Cliente]
       ↓
  Interactúa con
       ↓
[Movimientos.razor] 
       ↓
  Consume abstracción
       ↓
[IMovimientoService]
       ↓ (DI Dinámica según appsettings.json)
  ┌────┴────────────────────────┐
  ▼                             ▼
[MockMovimientoService]    [ApiMovimientoService]
  │                             │
  ├─► Genera datos mock         ├─► Invoca HttpClient
  └─► Retardo (500ms)           └─► Deserializa JSON
                                        │
                                        ▼
                                  [API REST Real]
```

---

## 2. Contexto de Negocio: Mapeo de Movimientos con los Planes de Ziur

Los movimientos solicitados en el reto corresponden a transacciones típicas en el ecosistema de sistemas contables y ERP que Ziur Software comercializa a través de sus tres ofertas principales:

```
 ┌────────────────────────────────────────────────────────────────────────┐
 │                      PRODUCTOS DE ZIUR SOFTWARE                        │
 └───────────┬──────────────────────────┬──────────────────────────┬──────┘
             ▼                          ▼                          ▼
       [ STANDARD ]              [ ENTERPRISE ]          [ ENTERPRISE SENIOR ]
             │                          │                          │
  ┌──────────┼──────────┐        ┌──────┼──────┐            ┌──────┴──────┐
  ▼          ▼          ▼        ▼      ▼      ▼            ▼             ▼
Reg. Doc.  Activos  Conciliac.  Contab. Interf. Aud.     Adm. Cartera  CRM  Tareas
Contables   Fijos    Bancaria    Auto.   IPS     Perm.
  │                              │       │
  │                              │       │
  ▼                              ▼       ▼
[Balance       [Ajuste al             [Avance
 Inicial]       Inventario]            Producción]
```

### Ejemplos Prácticos de Uso de cada Movimiento:

1. **Balance Inicial (Código 17)**:
   * *Dónde se vende*: Plan **STANDARD** (módulo *Registro de Documentos Contables*).
   * *Ejemplo de uso*: Se utiliza cuando una Pyme adquiere por primera vez el software contable de Ziur y carga sus saldos contables, inventario y cuentas por cobrar de apertura para iniciar la operación comercial en la plataforma.
2. **Ajuste al Inventario (Código 29)**:
   * *Dónde se vende*: Plan **STANDARD** (*Documentos Productos y Servicios*) y Plan **ENTERPRISE** (*Contabilización automática*).
   * *Ejemplo de uso*: Corrección de descuadres físicos de stock identificados durante una auditoría en bodega. El sistema genera un movimiento de entrada o salida de almacén para regularizar las existencias.
3. **Avance Producción (Código 51)**:
   * *Dónde se vende*: Plan **ENTERPRISE** (módulo *Interfaz IPS y Taller Automotriz* o producción manufacturera).
   * *Ejemplo de uso*: Registro del avance de órdenes de reparación o fabricación. Por ejemplo, en un taller automotriz, para declarar que una fase de reparación de motor (orden de trabajo) ha sido completada y transferida a la fase de pintura.

---

## 3. Justificación de Decisiones de Arquitectura

### ¿Por qué existe una Interfaz (`IMovimientoService`)?
Para cumplir con el **Principio de Inversión de Dependencias (DIP)**. La UI (`Movimientos.razor`) solo conoce las firmas de métodos necesarias para operar. Esto desacopla la lógica de consumo (ya sea REST, base de datos local o de prueba) de la capa gráfica.

### ¿Por qué existe un Mock (`MockMovimientoService`)?
Permite el desarrollo paralelo del frontend. Los diseñadores e ingenieros de UI pueden construir e iterar sobre los componentes interactivos de Blazor sin depender de que los servicios backend o APIs de terceros estén finalizados o disponibles.

### ¿Por qué se utiliza Inyección de Dependencias (DI)?
Evita la instanciación acoplada con `new`. El framework ASP.NET Core gestiona el ciclo de vida de los servicios (registrados como `Scoped`), permitiendo cambiar la implementación de forma transparente en el arranque (`Program.cs`) de la aplicación.

### ¿Por qué Blazor Web App (.NET 9)?
* **Componentes reutilizables**: Facilita la creación de grillas y dashboards avanzados.
* **Modelo interactivo en servidor (Interactive Server)**: Provee una comunicación fluida en tiempo real y reduce la latencia percibida, ideal para aplicaciones ERP internas de alta concurrencia.
* **Manejo de lenguaje unificado**: Permite usar C# en toda la pila de desarrollo (backend, modelos y frontend), eliminando la fricción de traducción de tipos y validaciones entre TypeScript/JS y C#.
