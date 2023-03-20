using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

using Catalog.WebApi.Repositories;
using Catalog.WebApi.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

var builder = WebApplication.CreateBuilder(args);
var connectionString = Connection.GetConnectionString(builder.Configuration);

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));
builder.Services.AddSingleton<IItemsRepository, MongoItemsRepository>();

builder.Services.AddControllers();

// https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
builder.Services.AddHealthChecks()
    .AddMongoDb(connectionString,
                name: "MongoDB",
                timeout: TimeSpan.FromMilliseconds(2000),
                tags: new[] { "ready" });

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
app.MapHealthChecks("/health/ready", new HealthCheckOptions() {
    Predicate = check => check.Tags.Contains("ready"),
    ResponseWriter = async(context, report) => {
        var result = JsonSerializer.Serialize(
            new {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new {
                    name      = entry.Key,
                    status    = entry.Value.Status.ToString(),
                    exception = entry.Value.Exception != null
                        ? entry.Value.Exception.Message
                        : "None",
                    duration  = entry.Value.Duration.ToString(),
                }),
            }
        );
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }
});

app.MapHealthChecks("/health/live", new HealthCheckOptions() {
    Predicate = _ => false
});

app.Run();
