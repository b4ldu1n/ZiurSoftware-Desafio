var builder = WebApplication.CreateBuilder(args);

// Configurar CORS para permitir peticiones desde el frontend y clientes como Bruno
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar opciones de JSON para mantener la nomenclatura original de las propiedades (PascalCase)
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Mostrar información de la API en desarrollo
    app.MapGet("/", () => "API de Movimientos - Ejecutando en http://localhost:7000")
        .WithName("Info")
        .WithOpenApi();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

// Endpoint GET /api/movimientos
app.MapGet("/api/movimientos", () =>
{
    var movimientos = new List<Movimiento>
    {
        new() { Codigo = 29, Descripcion = "Ajuste al Inventario", VActiva = false },
        new() { Codigo = 51, Descripcion = "Avance Produccion", VActiva = false },
        new() { Codigo = 17, Descripcion = "Balance Inicial", VActiva = false }
    };
    return Results.Ok(movimientos);
})
.WithName("GetMovimientos")
.WithOpenApi();

app.Run();

// Definición de la clase Movimiento (compatible con el modelo de Blazor)
public class Movimiento
{
    public int Codigo { get; set; }
    public string Descripcion { get; set; } = "";
    public bool VActiva { get; set; }
}

