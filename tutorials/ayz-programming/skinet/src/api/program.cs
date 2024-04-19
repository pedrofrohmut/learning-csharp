using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skinet.Core.Repositories;
using Skinet.Infrastructure.Data;
using Skinet.Infrastructure.Repositories;
using Skinet.Infrastructure.Utils;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<IProductRepository, ProductRepository>();
// builder.Services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
// builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped(typeof (IGenericRepository<>), typeof (GenericRepository<>));

builder.Services.AddDbContext<StoreDbContext>(options => {
    options.UseNpgsql(MyUtils.GetConnectionString(builder.Configuration));
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
