using Microsoft.EntityFrameworkCore;
using Skinet.Infrastructure.Data;
using Skinet.Infrastructure.Utils;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreDbContext>(options => {
    options.UseNpgsql(MyUtils.GetConnectionString(builder.Configuration));
});

var app = builder.Build();
app.MapControllers();
app.Run();
