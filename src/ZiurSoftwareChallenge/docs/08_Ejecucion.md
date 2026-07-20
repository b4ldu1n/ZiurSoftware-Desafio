# 08 - Ejecución (Cómo ejecutar el proyecto desde cero)

Pasos rápidos (Windows PowerShell):

1. Clonar el repositorio

```powershell
git clone <repo-url>
```

2. Entrar a la carpeta del proyecto

```powershell
cd path\to\ZiurSoftwareChallenge\src\ZiurSoftwareChallenge
```

3. Restaurar dependencias

```powershell
dotnet restore
```

4. Compilar

```powershell
dotnet build
```

5. Ejecutar

```powershell
dotnet run --urls http://localhost:5250
```

6. Abrir en navegador y navegar a la página de movimientos:

http://localhost:5250/movimientos

Notas:

- Si usas `user-secrets` para el token de la API:

```powershell
dotnet user-secrets init
dotnet user-secrets set "Api:AuthToken" "<TU_TOKEN>"
```

- Para limpiar:

```powershell
dotnet clean
```
