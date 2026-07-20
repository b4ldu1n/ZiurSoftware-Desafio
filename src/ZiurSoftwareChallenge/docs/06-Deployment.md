# 06 - Deployment

Notas rápidas para desplegar (conceptual):

- Configure `Api:BaseUrl` y secretos en variables de entorno o en un vault del entorno de despliegue.
- Asegure HTTPS y certificados para producción.
- Habilite logging y telemetría (Application Insights u otro).

Comandos de build para producción:

```powershell
dotnet publish -c Release -o ./publish
# publicar la carpeta ./publish al servidor/hosting
```
