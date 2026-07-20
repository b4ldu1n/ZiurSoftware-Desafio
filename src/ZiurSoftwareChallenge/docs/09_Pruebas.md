# 09 - Pruebas

Pruebas manuales y comandos útiles para verificar comportamiento en modo Mock y en modo API.

1) Modo Mock

- Si el proyecto incluye el mock y está registrado, la UI mostrará los datos simulados (ej. códigos 29, 51, 17). En la versión final este servicio fue removido; para pruebas use WireMock o restaurar commit previo que contenía `MockMovimientoService`.

2) Modo API

- Asegúrate de tener el token configurado:

```powershell
dotnet user-secrets set "Api:AuthToken" "<TU_TOKEN>"
```

- Ejecuta la app y observa logs:

```powershell
dotnet run --urls http://localhost:5250
```

- Abre http://localhost:5250/movimientos y verifica la tabla.

3) Forzar fallo de la API (verificar manejo de errores y banner)

- Remover token o usar token inválido:

```powershell
dotnet user-secrets remove "Api:AuthToken"
```

- Reinicia la app y verifica que la UI muestre el banner rojo con error y botón de reintento.

4) Verificar que realmente se hace una petición HTTP

- Usar `curl`:

```bash
curl -v -H "Authorization: Bearer <TU_TOKEN>" "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/DocumentosFillsCombos"
```

- Usar PowerShell:

```powershell
Invoke-RestMethod -Uri 'https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/DocumentosFillsCombos' -Headers @{ Authorization = 'Bearer <TU_TOKEN>' }
```

- Usar Postman/Insomnia: crear petición GET con cabecera `Authorization: Bearer <TU_TOKEN>`.

5) Inspeccionar logs

- Supervisar la salida de la app; `ApiMovimientoService` escribe mensajes como "Preparando petición HTTP GET a: DocumentosFillsCombos" y "Petición HTTP exitosa...".

6) Pruebas con herramientas de proxy/mocking

- WireMock, MockServer o Postman proxy permiten simular respuestas para reproducir diferentes códigos (401, 404, 500) y validar el comportamiento de la UI.
