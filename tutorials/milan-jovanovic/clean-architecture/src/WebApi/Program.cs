using CleanArch.Application;
using CleanArch.Infrastructure;
using CleanArch.Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection from other projects
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddPresentation();

builder.Host.UseSerilog((ctx, conf) => conf.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.Run();
