using ZiurSoftwareChallenge.Models;

namespace ZiurSoftwareChallenge.Services;

/// <summary>
/// Servicio que consume la API REST de Ziur.
/// Soporta configuración dinámica de endpoints y tokens de autenticación segura.
/// </summary>
public class ApiMovimientoService : IMovimientoService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiMovimientoService> _logger;

    public ApiMovimientoService(
        HttpClient http, 
        IConfiguration configuration, 
        ILogger<ApiMovimientoService> logger)
    {
        _http = http;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<List<Movimiento>> ObtenerMovimientosAsync()
    {
        var endpoint = _configuration["Api:Endpoint"] ?? "DocumentosFillsCombos";
        var token = _configuration["Api:AuthToken"];
        var headerName = _configuration["Api:AuthHeaderName"] ?? "Authorization";
        var headerValueFormat = _configuration["Api:AuthHeaderValueFormat"] ?? "Bearer {0}";

        _logger.LogInformation($"Preparando petición HTTP GET a: {endpoint}");

        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

        // Si hay un token configurado de forma externa, se agrega de manera segura a la cabecera
        if (!string.IsNullOrWhiteSpace(token))
        {
            _logger.LogInformation("Aplicando cabecera de autenticación token de forma dinámica...");
            var headerValue = string.Format(headerValueFormat, token);
            request.Headers.TryAddWithoutValidation(headerName, headerValue);
        }
        else
        {
            _logger.LogWarning("No se detectó ningún Token de autenticación en la configuración.");
        }

        // Realizar la solicitud
        var response = await _http.SendAsync(request);
        
        // Lanzar excepción si el servidor responde con error (ej. 401, 403, 500, etc.)
        response.EnsureSuccessStatusCode();

        var resultado = await response.Content.ReadFromJsonAsync<List<Movimiento>>()
            ?? new List<Movimiento>();

        _logger.LogInformation($"Petición HTTP exitosa. Se deserializaron {resultado.Count} movimientos.");
        return resultado;
    }
}
