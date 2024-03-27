var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// builder.Services.AddDbContext<ApplicationDbContext>(options => {
//     options.UseNpgsql(new MyUtils().GetConnectionString(builder.Configuration));
// });

var app = builder.Build();

// app.UseHttpsRedirection();
app.UseStaticFiles();
// app.UseRouting();
// app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
