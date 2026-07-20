# 08 - Cambiar Mock por API

En la versión actual la aplicación ya está integrada con la API real. Si necesita aplicar o revertir cambios para usar un mock local temporalmente, siga estas opciones sin modificar la lógica principal:

Opción A (revertir a commit previo que contenía `MockMovimientoService`):

1. Localiza el commit previo donde existía `MockMovimientoService` y crea una rama temporal.
2. Restaura solo el archivo `MockMovimientoService.cs` o modifique `Program.cs` para registrar el mock.

Opción B (usar proxy/mock local):

1. Monta WireMock o Postman mock con la ruta `DocumentosFillsCombos` que devuelva la estructura observada.
2. Cambia `Api:BaseUrl` en `appsettings.json` para apuntar al mock local.
