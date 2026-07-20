# 06 - Bruno (API testing with Bruno)

Uso rápido con Bruno (o herramientas similares):

1. Crear nueva colección o petición GET a:

```
https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/DocumentosFillsCombos
```

2. Añadir header `Authorization: Bearer <TU_TOKEN>`.
3. Ejecutar y observar JSON. Validar que la propiedad `value` contiene el array esperado.

Si la API devuelve 401, verificar token y problemas de CORS/Firewall.
