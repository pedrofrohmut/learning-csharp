using Catalog.Repositories;
using Catalog.Settings;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

// Converters for MongoDb
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IMongoClient>(opts => {
    var settings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});
// builder.Services.AddSingleton<IItemsRepository, InMemoryItemsRepository>();
builder.Services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
