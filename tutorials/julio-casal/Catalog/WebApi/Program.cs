using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

using Catalog.WebApi.Repositories;

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IMongoClient>(_ =>
        new MongoClient(builder.Configuration.GetConnectionString("Mongo")));
builder.Services.AddSingleton<IItemsRepository, MongoItemsRepository>();
builder.Services.AddControllers();

// Swagger Crap
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
