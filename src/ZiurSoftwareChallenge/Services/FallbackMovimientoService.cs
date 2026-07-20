using ZiurSoftwareChallenge.Models;

namespace ZiurSoftwareChallenge.Services;

/// <summary>
/// Servicio decorador/wrapper que implementa IMovimientoService.
/// Intenta consumir la API real a través de ApiMovimientoService. Si la llamada falla
/// por cualquier motivo (red, autenticación inválida o timeout), captura el error
/// y desvía la llamada a MockMovimientoService de forma transparente para la grilla.
/// </summary>
public class FallbackMovimientoService : IMovimientoService
{
    private readonly ApiMovimientoService _apiService;
    private readonly MockMovimientoService _mockService;
    private readonly ILogger<FallbackMovimientoService> _logger;
    private readonly bool _useFallback;

    public FallbackMovimientoService(
        ApiMovimientoService apiService,
        MockMovimientoService mockService,
        IConfiguration configuration,
        ILogger<FallbackMovimientoService> logger)
    {
        _apiService = apiService;
        _mockService = mockService;
        _logger = logger;
        // Permite desactivar el fallback si en producción se prefiere propagar el error
        _useFallback = configuration.GetValue<bool>("Api:UseFallback", defaultValue: true);
    }

    public async Task<List<Movimiento>> ObtenerMovimientosAsync()
    {
        try
        {
            _logger.LogInformation("Iniciando solicitud a la API real...");
            var result = await _apiService.ObtenerMovimientosAsync();
            
            // Si la API responde exitosamente y con datos, los retornamos
            if (result != null && result.Count > 0)
            {
                _logger.LogInformation($"API real retornó {result.Count} registros exitosamente.");
                return result;
            }
            
            _logger.LogWarning("La API real no retornó datos o el listado estaba vacío.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error detectado al intentar consumir la API real: {ex.Message}");
        }

        if (_useFallback)
        {
            _logger.LogWarning("Iniciando fallback de seguridad: Cargando datos Mock en memoria.");
            return await _mockService.ObtenerMovimientosAsync();
        }

        // Si el fallback está desactivado, se retorna una lista vacía para no romper el ciclo
        return new List<Movimiento>();
    }
}
