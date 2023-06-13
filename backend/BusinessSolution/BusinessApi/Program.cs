


using ApiUtils;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Transient, Scoped, and Singleton - tomorrow morning we will learn how to choose these.
builder.Services.AddScoped<BusinessClock>();
builder.Services.AddSingleton<ISystemTime, SystemTime>();

// everything above here is configuring the "guts" of our API
var app = builder.Build();
// everything AFTER here is setting up the Request/Response pipeline.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/clock", ([FromServices] BusinessClock businessClock) =>
{
  
    var response = businessClock.GetClockResponse();
    return Results.Ok(response);
});

app.MapGet("/info", () =>
{
    var fullName = Formatters.FormatName("Bob", "Smith");
    return Results.Ok(new { number = "555-1212", fullName = fullName, age=21 });
});
// Start the Web Server (Kestrel)

app.Run();


