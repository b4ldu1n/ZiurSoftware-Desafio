# ⚡ GUÍA RÁPIDA DE INICIO

## 🚀 Ejecutar en 3 Pasos

### Terminal 1: Iniciar API
```powershell
cd c:\Users\Lenovo\ZiurSoftwareChallenge
dotnet run --project src/ZiurSoftwareChallenge.Api/ZiurSoftwareChallenge.Api.csproj
```
✅ API disponible en: **http://localhost:7000/api/movimientos**

### Terminal 2: Iniciar Blazor
```powershell
cd c:\Users\Lenovo\ZiurSoftwareChallenge
dotnet run --project src/ZiurSoftwareChallenge/ZiurSoftwareChallenge.csproj
```
✅ Blazor disponible en: **http://localhost:5255/movimientos**

### Verificar
- Abre navegador → **http://localhost:5255/movimientos**
- Deberías ver tabla con 3 registros (Códigos: 29, 51, 17)
- Abre Bruno → GET `/api/movimientos` → Status 200 ✅

---

## 🔧 Cambiar entre Mock y API

### Archivo: `appsettings.json`
```json
{
  "Api": {
    "UseMock": false,                    ← Cambiar a true para Mock
    "BaseUrl": "http://localhost:7000"
  }
}
```

- **false** → Consume HTTP desde API (recomendado)
- **true** → Usa datos en memoria (sin HTTP)

Reinicia Blazor después de cambiar.

---

## 🧪 Probar con Bruno

1. **Abrir Bruno**
2. **Open Collection** → Carpeta `bruno/`
3. Selecciona ambiente **Development** (arriba a la derecha)
4. Haz click en **"Obtener Movimientos"**
5. **Send** → Verifica Status 200

---

## 📊 Estructura Visual

```
http://localhost:5255/movimientos
           ↓
    [Componente Blazor]
           ↓
    IMovimientoService
           ↓
    ApiMovimientoService (HttpClient)
           ↓
    GET http://localhost:7000/api/movimientos
           ↓
    [Minimal API]
           ↓
    JSON [{...}, {...}, {...}]
           ↓
    <table> renderizada
```

---

## 📝 Archivos Clave

| Archivo | Propósito |
|---------|-----------|
| `src/ZiurSoftwareChallenge.Api/Program.cs` | API con endpoint /api/movimientos |
| `src/ZiurSoftwareChallenge/Program.cs` | Configuración inyección dependencias |
| `src/ZiurSoftwareChallenge/appsettings.json` | UseMock + BaseUrl |
| `src/ZiurSoftwareChallenge/Services/ApiMovimientoService.cs` | Consumo HTTP |
| `src/ZiurSoftwareChallenge/Services/MockMovimientoService.cs` | Mock en memoria |
| `src/ZiurSoftwareChallenge/Components/Pages/Movimientos.razor` | UI (tabla) |
| `bruno/ZiurSoftwareChallenge.bru` | Petición HTTP con assertions |

---

## ✅ Validar Compilación

```bash
dotnet build
```
Deberías ver:
```
✅ ZiurSoftwareChallenge.Api: SUCCESS
✅ ZiurSoftwareChallenge: SUCCESS
```

---

## 🔗 URLs

| Componente | URL | Puerto |
|-----------|-----|--------|
| Blazor (HTTP) | http://localhost:5255 | 5255 |
| Blazor (HTTPS) | https://localhost:7156 | 7156 |
| API (HTTP) | http://localhost:7000 | 7000 |
| API (HTTPS) | https://localhost:7001 | 7001 |
| Movimientos (ruta) | /movimientos | - |

---

## 🐛 Troubleshooting

### ❌ Puerto 5255 ya está en uso
```powershell
# Matar proceso
taskkill /F /IM ZiurSoftwareChallenge.exe

# O usar otro puerto
dotnet run --project src/ZiurSoftwareChallenge --urls http://localhost:5256
```

### ❌ La tabla no se llena
1. Verifica que la API está corriendo en puerto 7000
2. Revisa que `UseMock = false` en `appsettings.json`
3. Abre la consola del navegador (F12) y busca errores
4. Revisa logs de Blazor en la terminal

### ❌ Error SSL/HTTPS
```bash
dotnet dev-certs https --trust
```

---

## 📚 Documentación Completa

Para documentación detallada, ver:
- `docs/01_Objetivo.md` - Objetivo del reto
- `docs/05_API.md` - Especificación de la API
- `docs/06_Bruno.md` - Pruebas con Bruno
- `docs/07_Ejecucion.md` - Guía paso a paso
- `docs/08_CambiarMockPorAPI.md` - Cómo cambiar configuración
- `ENTREGA_FINAL.md` - Informe técnico completo

---

## 🎯 Estado del Proyecto

✅ Compilación: **PASS**  
✅ API HTTP: **FUNCIONANDO**  
✅ Blazor Consumo: **FUNCIONANDO**  
✅ Tabla Llena: **CONFIRMADO**  
✅ Bruno Tests: **PASS**  
✅ Git Commits: **GUARDADOS**  

**Listo para producción** ✨
