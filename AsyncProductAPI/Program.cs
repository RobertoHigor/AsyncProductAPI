using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Dtos;
using WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlite("Data Source=RequestDB.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Start Endpoint
app.MapPost("api/v1/products", async (AppDbContext context, ListingRequest listingRequest) => {
    if (listingRequest == null)
        return Results.BadRequest();

    listingRequest.RequestStatus = "IN PROGRESS";
    listingRequest.EstimatedCompletionTime = "2023-04-15 14:00";

    await context.ListingRequests.AddAsync(listingRequest);
    await context.SaveChangesAsync();

    // Espera uma URI onde é possível obter o status do conteúdo requisitado
    return Results.Accepted($"api/v1/productstatus/{listingRequest.RequestId}", listingRequest);
});

// Status Endpoint

app.MapGet("api/v1/productstatus/{requestId}", (AppDbContext context, string requestId) => {
    var listingRequest = context.ListingRequests.FirstOrDefault(lr => lr.RequestId == requestId);

    if (listingRequest == null)
    return Results.NotFound();

    ListingStatus listingStatus = new () {
        RequestStatus = listingRequest.RequestStatus,
        ResourceUrl = String.Empty
    };

    // Se finalizou, enviar a URL com o resultado.
    if (listingRequest.RequestStatus!.ToUpper() == "COMPLETE")
    {
        listingStatus.ResourceUrl = $"api/v1/products/{Guid.NewGuid()}";
        return Results.Redirect($"http://localhost:5270/{listingStatus.ResourceUrl}");
    }

    listingStatus.EstimatedCompletionTime = "2023-04-15 16:00";
    return Results.Ok(listingStatus);
});

// Final Endpoint

app.MapGet("api/v1/products/{requestId}", (string requestId) => {
    return Results.Ok("Listing placeholder");
});

app.Run();