using ZiurSoftwareChallenge.Models;
using System.Net.Http.Json;
using System.Text.Json;

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

        // Intentar deserializar directamente a List<Movimiento>
        try
        {
            var listaDirecta = await response.Content.ReadFromJsonAsync<List<Movimiento>>();
            if (listaDirecta != null)
            {
                _logger.LogInformation($"Petición HTTP exitosa. Se deserializaron {listaDirecta.Count} movimientos (deserialización directa).");
                return listaDirecta;
            }
        }
        catch (Exception ex) when (ex is NotSupportedException || ex is JsonException)
        {
            _logger.LogDebug("Deserialización directa a List<Movimiento> falló, se intentará parsear el JSON crudo.");
        }

        // Si la API devuelve un objeto wrapper (por ejemplo { "value": [ ... ], "Count": N }),
        // intentamos buscar el primer array dentro del JSON y deserializarlo.
        try
        {
            using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            // Si la raíz es un array, deserializamos directamente
            if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                var raw = doc.RootElement.GetRawText();
                var arr = JsonSerializer.Deserialize<List<Movimiento>>(raw);
                _logger.LogInformation($"Petición HTTP exitosa. Se deserializaron {arr?.Count ?? 0} movimientos (raíz array).");
                return arr ?? new List<Movimiento>();
            }

            // Si la raíz es un objeto, buscar la primera propiedad que sea un array
            if (doc.RootElement.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in doc.RootElement.EnumerateObject())
                {
                    if (prop.Value.ValueKind == JsonValueKind.Array)
                    {
                        var raw = prop.Value.GetRawText();
                        var arr = JsonSerializer.Deserialize<List<Movimiento>>(raw);
                        _logger.LogInformation($"Petición HTTP exitosa. Se deserializaron {arr?.Count ?? 0} movimientos (wrapper '{prop.Name}').");
                        return arr ?? new List<Movimiento>();
                    }
                }
            }
        }
        catch (JsonException je)
        {
            _logger.LogError($"Error al parsear JSON de la API: {je.Message}");
        }

        _logger.LogWarning("No fue posible deserializar movimientos desde la respuesta de la API.");
        return new List<Movimiento>();
    }
}
