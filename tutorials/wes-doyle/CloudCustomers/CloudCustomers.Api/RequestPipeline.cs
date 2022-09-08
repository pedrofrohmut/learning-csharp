namespace CloudCustomers.Api;

public class RequestPipeline
{
    public static WebApplication Configure(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }
}
