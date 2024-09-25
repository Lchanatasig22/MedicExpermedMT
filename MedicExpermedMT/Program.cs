using MedicExpermedMT.Models;
using MedicExpermedMT.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<medicossystembdIIIContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Agregar soporte para sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Configura el tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Asegura que la cookie solo se use a través de HTTP
    options.Cookie.IsEssential = true; // Asegura que la cookie sea esencial para que funcione la sesión
});
// Registrar el servicio de autenticación
builder.Services.AddScoped<AutenticationService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<ConsultationService>();
builder.Services.AddLogging(); ;

// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Habilitar el uso de sesiones
app.UseSession();  // Importante: Habilitar sesiones aquí

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Access}/{action=Login}/{id?}");
});

app.Run();
