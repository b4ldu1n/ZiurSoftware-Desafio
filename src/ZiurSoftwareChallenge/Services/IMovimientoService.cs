using ZiurSoftwareChallenge.Models;

namespace ZiurSoftwareChallenge.Services;

public interface IMovimientoService
{
    Task<List<Movimiento>> ObtenerMovimientosAsync();
}
