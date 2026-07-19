using ZiurSoftwareChallenge.Components;
using ZiurSoftwareChallenge.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar el servicio de movimientos dinámicamente conectando siempre con la API
var apiConfig = builder.Configuration.GetSection("Api");

builder.Services.AddHttpClient<IMovimientoService, ApiMovimientoService>(client =>
{
    client.BaseAddress = new Uri(apiConfig["BaseUrl"] ?? "http://localhost");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
