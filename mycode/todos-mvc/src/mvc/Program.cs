var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.UseAuthentication();
//app.UseAuthorization(); // TODO: activate if session stop working

app.UseSession();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/*

  TODOS:

  1. Remove try/catch from controllers
  2. Make a catch all in exceptions in the Program.cs
  3. Make error page if needed to display those error messages

  4. On the use cases make validation return a valid value

*/
