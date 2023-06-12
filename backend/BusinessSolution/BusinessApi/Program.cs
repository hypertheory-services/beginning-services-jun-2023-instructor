var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// everything above here is configuring the "guts" of our API
var app = builder.Build();
// everything AFTER here is setting up the Request/Response pipeline.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/clock", () =>
{
    var response = new GetClockResponse(false, DateTime.Now.AddDays(1));
    return Results.Ok(response);
});
// Start the Web Server (Kestrel)

app.Run();


public record GetClockResponse(bool IsOpen, DateTime? NextOpenTime);