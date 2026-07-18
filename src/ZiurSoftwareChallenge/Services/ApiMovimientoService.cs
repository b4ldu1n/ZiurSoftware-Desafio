using ZiurSoftwareChallenge.Models;

namespace ZiurSoftwareChallenge.Services;

/// <summary>
/// Servicio que consume la API REST.
/// Se activará cuando se reciba el endpoint y se actualice Program.cs.
/// </summary>
public class ApiMovimientoService : IMovimientoService
{
    private readonly HttpClient _http;
    private readonly ILogger<ApiMovimientoService> _logger;

    public ApiMovimientoService(HttpClient http, ILogger<ApiMovimientoService> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<List<Movimiento>> ObtenerMovimientosAsync()
    {
        try
        {
            _logger.LogInformation("Obteniendo movimientos desde la API...");
            
            var resultado = await _http.GetFromJsonAsync<List<Movimiento>>("api/movimientos")
                ?? new List<Movimiento>();
            
            _logger.LogInformation($"Se obtuvieron {resultado.Count} movimientos");
            
            return resultado;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Error al conectar con la API: {ex.Message}");
            return new List<Movimiento>();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error inesperado: {ex.Message}");
            return new List<Movimiento>();
        }
    }
}
