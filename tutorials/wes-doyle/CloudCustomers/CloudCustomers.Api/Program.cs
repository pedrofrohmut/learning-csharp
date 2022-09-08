using CloudCustomers.Api;

var builder = AppBuilder.AddServices(args);
var app = RequestPipeline.Configure(builder);
app.Run();
