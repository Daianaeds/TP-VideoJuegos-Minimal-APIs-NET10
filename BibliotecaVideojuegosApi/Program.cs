using BibliotecaVideojuegosApi.Data;
using BibliotecaVideojuegosApi.Endpoints;
using BibliotecaVideojuegosApi.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("v1");
builder.Services.AddValidation();
builder.Services.AddScoped<VideoJuegoService>();
builder.Services.AddScoped<PrestamoVideoJuegoService>();
builder.Services.AddScoped<CopiaVideoJuegoService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapVideoJuegosEndpoints();
app.MapBusquedaEndpoints();
app.MapCopiaVideoJuegoEndpoints();
app.MapPrestamoVideoJuegoEndpoints();
app.MapGet("/", () => builder.Configuration["SaludoBienvenida"] ?? "Hola CodigoFacilito!!");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();   
}

app.Run();
