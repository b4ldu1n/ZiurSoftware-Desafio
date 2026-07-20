using ZiurSoftwareChallenge.Models;

namespace ZiurSoftwareChallenge.Services;

/// <summary>
/// Servicio simulado que devuelve datos de prueba en memoria.
/// Se usa como fallback de contingencia cuando la API real no está disponible.
/// </summary>
public class MockMovimientoService : IMovimientoService
{
    public async Task<List<Movimiento>> ObtenerMovimientosAsync()
    {
        // Simular latencia de red
        await Task.Delay(300);

        return new List<Movimiento>
        {
            new()
            {
                Codigo = 29,
                Descripcion = "Ajuste al Inventario",
                VActiva = false
            },
            new()
            {
                Codigo = 51,
                Descripcion = "Avance Produccion",
                VActiva = false
            },
            new()
            {
                Codigo = 17,
                Descripcion = "Balance Inicial",
                VActiva = false
            }
        };
    }
}
