using Microsoft.EntityFrameworkCore;
using PubSubPattern.MessageBroker.Data;
using PubSubPattern.MessageBroker.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlite("Data Source=MessageBroker.db"));

var app = builder.Build();

app.UseHttpsRedirection();

// Create Topic
app.MapPost("api/topics", async (AppDbContext ctx, Topic topic) => {
    await ctx.Topics.AddAsync(topic);
    await ctx.SaveChangesAsync();
    return Results.Created($"api/topics/{topic.Id}", topic);
});

// Return All Topics
app.MapGet("api/topics", async (AppDbContext ctx) => {
    var topics = await ctx.Topics.ToListAsync();
    return Results.Ok(topics);
});

// Publish Message
app.MapPost("api/topics/{id}/messages", async (AppDbContext ctx, int id, Message message) => {
    bool topicFound = await ctx.Topics.AnyAsync(topic => topic.Id == id);
    if (!topicFound) {
        return Results.NotFound("Topic not found");
    }
    var subscriptions = ctx.Subscriptions.Where(sub => sub.TopicId == id);
    if (subscriptions.Count() == 0) {
        return Results.NotFound("There are not subscriptions to this topic");
    }
    foreach (var subscription in subscriptions) {
        var msg = new Message {
            TopicMessage = message.TopicMessage,
            SubscriptionId = subscription.Id,
            ExpiresAfter = message.ExpiresAfter,
            MessageStatus = message.MessageStatus,
        };
        await ctx.Messages.AddAsync(msg);
    }
    await ctx.SaveChangesAsync();
    return Results.Ok("Message has been added");
});

// Create subscription
app.MapPost("api/topics/{id}/subscriptions",
        async (AppDbContext ctx, int id, Subscription subscription) => {
    bool topicFound = await ctx.Topics.AnyAsync(topic => topic.Id == id);
    if (!topicFound) {
        return Results.NotFound("Topic not found");
    }
    subscription.TopicId = id;
    await ctx.Subscriptions.AddAsync(subscription);
    await ctx.SaveChangesAsync();
    return Results.Created($"api/topics/{id}/subscriptions/{subscription.Id}", subscription);
});

// Get subscriber messages
app.MapGet("api/subscriptions/{id}/messages", async (AppDbContext ctx, int id) => {
    bool hasSubscriptions = await ctx.Subscriptions.AnyAsync(sub => sub.Id == id);
    if (!hasSubscriptions) {
        return Results.NotFound("Subscriptions not found");
    }
    var messages = ctx.Messages.Where(msg => msg.SubscriptionId == id
                                          && msg.MessageStatus != "SENT");
    if (messages.Count() == 0) {
        return Results.NotFound("No new messages");
    }
    foreach (var msg in messages) {
        msg.MessageStatus = "REQUESTED";
    }
    await ctx.SaveChangesAsync();
    return Results.Ok(messages);
});

// Ack Messages for subscribers
app.MapPost("api/subscriptions/{id}/messages", async (AppDbContext ctx, int id, int[] confirmations) => {
    bool hasSubscriptions = await ctx.Subscriptions.AnyAsync(sub => sub.Id == id);
    if (!hasSubscriptions) {
        return Results.NotFound("Subscriptions not found");
    }
    if (confirmations.Length <= 0) {
        return Results.BadRequest();
    }
    int counter = 0;
    foreach (var conf in confirmations) {
        var msg = ctx.Messages.FirstOrDefault(msg => msg.Id == conf);
        if (msg != null) {
            msg.MessageStatus = "SENT";
            await ctx.SaveChangesAsync();
            counter++;
        }
    }
    return Results.Ok($"Acknowledged {counter}/{confirmations.Length}");
});

app.Run();
