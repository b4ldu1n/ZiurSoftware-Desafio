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

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

// Endpoint GET /api/movimientos
app.MapGet("/api/movimientos", () =>
{
    var movimientos = new List<Movimiento>
    {
        new(29, "Ajuste al Inventario", false),
        new(51, "Avance Produccion", false),
        new(17, "Balance Inicial", false)
    };
    return Results.Ok(movimientos);
})
.WithName("GetMovimientos");

app.Run();

// Definición del record Movimiento con PascalCase
public record Movimiento(int Codigo, string Descripcion, bool VActiva);

