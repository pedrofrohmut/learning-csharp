using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Infrastructure.Repositories;
using WhiteLagoon.Application.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    // var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var connectionString = "Host=localhost;Port=5106;database=postgres;username=postgres;password=password";
    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<IVillaRepository, VillaRepository>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
