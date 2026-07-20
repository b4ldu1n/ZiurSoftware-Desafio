# 07 - Ejecución

Resumen rápido (igual que en README):

```powershell
cd src\ZiurSoftwareChallenge
dotnet restore
dotnet build
dotnet user-secrets set "Api:AuthToken" "<TU_TOKEN>"
dotnet run --urls http://localhost:5250
```

Acceder a: http://localhost:5250/movimientos
