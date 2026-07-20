# 12 - Preguntas de entrevista y respuestas sugeridas

Este documento agrupa preguntas que un entrevistador puede formular sobre este proyecto, con respuestas técnicas concisas.

1) ¿Por qué usar `IMovimientoService` como interfaz?

- Respuesta: Desacopla la UI de la implementación concreta, permite intercambiar el origen de datos (mock, API) sin cambiar componentes. Cumple el principio de inversión de dependencias (SOLID).

2) ¿Cómo maneja la aplicación la autenticación con la API?

- Respuesta: El token se configura de forma externa (User Secrets o variables de entorno) y `ApiMovimientoService` agrega la cabecera `Authorization: Bearer <token>` dinámicamente antes de enviar la petición.

3) ¿Qué ventajas tiene `HttpClientFactory`?

- Respuesta: Gestiona el ciclo de vida de `HttpClient`, evita problemas de agotamiento de sockets, permite configurar policies, handlers y resiliencia centralizada.

4) ¿Cómo se manejan errores HTTP en la app?

- Respuesta: `ApiMovimientoService` llama `response.EnsureSuccessStatusCode()` para lanzar excepción con códigos no 2xx; la página captura `HttpRequestException` y muestra el código y detalle al usuario.

5) ¿Qué es el wrapper JSON y por qué lo soportamos?

- Respuesta: En algunos servicios la respuesta es un objeto que contiene la colección en una propiedad (p. ej. `value`). Soportarlo mejora interoperabilidad con API que no devuelven arrays en la raíz.

6) ¿Cómo probarías la app en CI?

- Respuesta: Ejecutar `dotnet build`, ejecutar tests unitarios de servicios con respuestas simuladas (mock HttpMessageHandler), verificar linting y análisis estático.

7) ¿Por qué evitar guardar tokens en `appsettings.json`?

- Respuesta: Evita exposición accidental en repositorios. Los secretos deben almacenarse en vaults o `dotnet user-secrets` en desarrollo.

8) ¿Qué cambios harías para producción?

- Respuesta: Configurar key vault para secretos, activar HTTPS con certificados, centralizar logging/telemetría (Application Insights), agregar retries con backoff y circuit breaker para resiliencia.

9) ¿Qué patrones se usan en este proyecto?

- Respuesta: Inyección de dependencias, Repository-style abstraction para servicios, y un simple Decorator/Fallback (se implementó y luego se removió según alcance).

10) ¿Cómo añadirías caché?

- Respuesta: Usaría `IMemoryCache` para caché en memoria con expiración, o Redis para distribución. Implementaría en `ApiMovimientoService` o en un decorador.
