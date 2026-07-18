# 04 - Pruebas y Evidencia de Funcionamiento (Testing)

Este documento contiene la bitácora y la evidencia empírica de compilación, ejecución y navegación del proyecto.

---

## 1. Verificación del Build (Compilación)

Se ejecuta el comando de restauración y compilación desde el CLI de .NET:
```bash
dotnet build
```

### Resultado Obtenido:
```text
  Determinando los proyectos que se van a restaurar...
  Todos los proyectos están actualizados para la restauración.
  ZiurSoftwareChallenge -> C:\Users\Lenovo\ZiurSoftwareChallenge\src\ZiurSoftwareChallenge\bin\Debug\net9.0\ZiurSoftwareChallenge.dll

Compilación correcta.
    0 Advertencia(s)
    0 Errores

Tiempo transcurrido 00:00:02.54
```
*Se confirma que el código no posee errores de compilación sintáctica ni de referencias.*

---

## 2. Ejecución de la Aplicación

Se levanta el servidor web Kestrel local:
```bash
dotnet run
```

### Resultado de Arranque:
```text
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7196
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5250
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
```

---

## 3. Pruebas de Navegación en el Portal

Al acceder mediante el navegador web a la dirección `https://localhost:7196`, se realizaron pruebas sobre la navegación del menú lateral en [NavMenu.razor](file:///c:/Users/Lenovo/ZiurSoftwareChallenge/src/ZiurSoftwareChallenge/Components/Layout/NavMenu.razor):
1. **Home**: Carga la página de bienvenida por defecto.
2. **Counter**: El componente contador interactivo funciona de forma reactiva en servidor.
3. **Weather**: Renderiza los datos meteorológicos simulados del framework.
4. **Movimientos**: Carga la grilla personalizada del desafío técnico.

---

## 4. Resultados Esperados vs. Obtenidos en Movimientos

Al hacer clic en la sección de **Movimientos**, se verificaron los siguientes hitos de funcionamiento:

| Elemento de Prueba | Comportamiento Esperado | Estado |
| :--- | :--- | :---: |
| **Spinner de Carga** | Se muestra un indicador de carga durante la espera de 500 ms de latencia ficticia. | ✅ Pasa |
| **Tabla responsiva** | La tabla se adapta a pantallas de diferentes resoluciones (Mobile, Desktop). | ✅ Pasa |
| **Carga de Registros** | Se despliegan exactamente los 3 registros previstos (`Ajuste al Inventario`, `Avance Produccion`, `Balance Inicial`). | ✅ Pasa |
| **Visualización de Badges** | Los códigos se destacan con badges azules y los estados como badges de colores diferenciados para visualización inmediata. | ✅ Pasa |
| **Navegación Activa** | La pestaña "Movimientos" en el menú lateral permanece remarcada al estar en su ruta. | ✅ Pasa |

---

## 5. Tabla de Verificación de Requisitos (Ziur Challenge)

| Criterio de Verificación | Descripción | Estado |
| :--- | :--- | :---: |
| **Arranque de App** | El host de Blazor inicia sin excepciones ni advertencias críticas. | ✅ |
| **Respuesta de Servicio** | El servicio resuelve el contrato asíncrono y devuelve los objetos. | ✅ |
| **Modelo Deserializado** | Se respetan los tipos de datos exactos del JSON esperado (`int`, `string`, `bool`). | ✅ |
| **Grilla Renderizada** | Los elementos se estructuran en columnas responsivas y legibles. | ✅ |
| **Preparado para API Real** | Arquitectura inyectable y parametrizada sin necesidad de alterar código posterior. | ✅ |
