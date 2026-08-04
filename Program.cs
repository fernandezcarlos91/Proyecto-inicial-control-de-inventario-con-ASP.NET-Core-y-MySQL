using Microsoft.EntityFrameworkCore;
using InventarioWeb.Data;

var builder = WebApplication.CreateBuilder(args);
 
// 1. Registrar servicios que la app va a usar (equivalente a configurar
//    extensiones en Flask, como Flask-SQLAlchemy)
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Inventario API",
        Version = "v1",
        Description = "API REST para el control de inventario"
    });
});


var connectionString = builder.Configuration.GetConnectionString("InventarioDb");
builder.Services.AddDbContext<InventarioContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventario API v1");
    });
}


// 2. Configurar el 'pipeline' de peticiones (middlewares)
app.UseStaticFiles();   // habilita wwwroot/, como Flask sirve static/ automáticamente
app.UseRouting();
app.UseAuthorization();
 
// 3. Definir la ruta por defecto (patrón de todas las rutas del proyecto)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
 
// 4. Arrancar el servidor (equivalente a app.run())
app.Run();
