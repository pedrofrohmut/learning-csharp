using BuberDinner.Api;

var builder = WebApplication.CreateBuilder(args);
AppBuilder.AddServices(builder);

var app = builder.Build();
RequestPipeline.Configure(app);
app.Run();
