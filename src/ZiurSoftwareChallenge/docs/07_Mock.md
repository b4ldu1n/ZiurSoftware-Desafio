# 07 - Mock (modo desarrollo)

Propósito:

- Permitir desarrollo sin depender de la API remota.
- Permitir tests manuales y demostraciones locales.

Cómo funcionaba originalmente:

- `MockMovimientoService` devolvía en memoria una lista fija de `Movimiento` con un retraso simulado (`Task.Delay(500)`).

Estado actual:

- El proyecto fue preparado para usar exclusivamente `ApiMovimientoService` en producción. El mock fue removido en una fase de limpieza para que la integración con la API real sea la referencia principal.

Cómo activar un mock local (recomendación de uso sin editar código):

1. Si deseas recuperar el modo mock sin restaurar el archivo, puedes temporeramente cambiar en `Program.cs` la inyección para usar una implementación local que devuelva datos (recomendamos revertir al historial git donde existía `MockMovimientoService`).

2. Alternativamente, intercepta la URL con una herramienta local (MockServer, WireMock, Postman proxy) y sirve la respuesta observada desde la API.
