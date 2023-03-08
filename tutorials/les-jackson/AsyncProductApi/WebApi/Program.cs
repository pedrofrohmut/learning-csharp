using AsyncProduct.WebApi.Models;
using AsynProductApi.WebApi.Data;
using AsynProductApi.WebApi.Dtos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlite("Data Source=RequestDB.db"));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("api/v1/products", async (AppDbContext ctx, ListingRequest listingRequest) => {
    if (listingRequest == null) return Results.BadRequest();
    listingRequest.RequestStatus = "ACCEPT";
    listingRequest.EstimateCompetionTime = "2023-02-06 14:00:00";
    await ctx.ListingRequests.AddAsync(listingRequest);
    await ctx.SaveChangesAsync();
    return Results.Accepted($"api/v1/productstatus/{listingRequest.RequestId}", listingRequest);
});

app.MapGet("api/v1/productstatus/{requestId}", (AppDbContext ctx, string requestId) => {
    var listingRequest = ctx.ListingRequests.FirstOrDefault(x => x.RequestId == requestId);
    if (listingRequest == null) {
        return Results.NotFound();
    }
    ListingStatus listingStatus = new ListingStatus() {
        RequestStatus = listingRequest.RequestStatus,
        ResourceUrl = String.Empty,
    };
    if (listingRequest.RequestStatus!.ToUpper() == "COMPLETE") {
        listingStatus.ResourceUrl = $"api/v1/products/.manjaro-tools{Guid.NewGuid().ToString()}";
        // return Results.Ok(status);
        return Results.Redirect("https://localhost:5001/" + listingStatus.ResourceUrl);
    }
    listingStatus.EstimatedCompetionTime = "2023-02-06 15:00:00";
    return Results.Ok(listingStatus);
});

app.MapGet("api/v1/products/{requestId}", (string requestId) =>
    Results.Ok("This is where you would pass back the final result"));

app.Run();
