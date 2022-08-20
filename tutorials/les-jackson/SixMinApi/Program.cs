using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SixMinApi.Data;
using SixMinApi.Dtos;
using SixMinApi.Models;

var builder = WebApplication.CreateBuilder(args);

var username = builder.Configuration["DB_USER"];
var password = builder.Configuration["DB_PASS"];
var connectionString =
    $"Host=localhost;Port=5432;database=sixminapi_db;username={username};password={password}";

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<ICommandRepo, CommandRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("api/v1/commands", async (ICommandRepo repo, IMapper mapper) =>
{
    var commands = await repo.GetAllCommands();
    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
});

app.MapGet("api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, int id) =>
{
    var command = await repo.GetCommandById(id);
    if (command == null)
    {
        return Results.BadRequest("Command not found with the id passed");
    }
    return Results.Ok(command);
});

app.MapPost("api/v1/commands", async (ICommandRepo repo, IMapper mapper, CommandCreateDto command) =>
{
    var commandModel = mapper.Map<Command>(command);
    await repo.CreateCommand(commandModel);
    await repo.SaveChanges();
    var commandReadDto = mapper.Map<CommandReadDto>(commandModel);
    return Results.Created($"api/v1/commands/{commandReadDto.Id}", commandReadDto);
});

app.MapPut("api/v1/commands/{id}",
        async (ICommandRepo repo, IMapper mapper, int id, CommandUpdateDto commandUpdateDto) => {
    var command = await repo.GetCommandById(id);
    if (command == null)
    {
        return Results.BadRequest("Command not found with the id passed");
    }
    // Update command from db with values from request.body
    mapper.Map(commandUpdateDto, command);
    await repo.SaveChanges();
    return Results.NoContent();
});

app.MapDelete("api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, int id) => {
    var command = await repo.GetCommandById(id);
    if (command == null)
    {
        return Results.BadRequest("Command not found with the id passed");
    }
    repo.DeleteCommand(command);
    await repo.SaveChanges();
    return Results.NoContent();
});

app.Run();
