using LocationsApi;
using LocationsApi.Adapters;
using LocationsApi.Services;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// this wouldd have in "ConfigureServices" in your Startup.cs
// Add services to the container.
// all the behind the scenes stuff that makes up our application

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var onCallAddress = builder.Configuration.GetValue<string>("onCallAddress");
if (onCallAddress is null)
{
    throw new Exception("Can't start API without the onCallAddress");
}
Console.WriteLine($"Using the API address of {onCallAddress}");
builder.Services.AddHttpClient<OnCallDeveloperHttpAdapter>(client =>
{
    client.BaseAddress = new Uri(onCallAddress); // TODO DON'T DO THIS.
})
    .AddPolicyHandler(SrePolicies.GetDefaultRetyPolicyAsync())
    .AddPolicyHandler(SrePolicies.GetDefaultCircuitBreaker());

var clock = new UptimeClock();
builder.Services.AddSingleton<UptimeClock>(clock);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(pol =>
    {
        pol.AllowAnyOrigin(); // Promiscuous 
        pol.AllowAnyHeader();
        pol.AllowAnyMethod();
    });
});

var locationsConnectionString = builder.Configuration.GetConnectionString("locations-db");
if(locationsConnectionString is null)
{
    throw new Exception("Can't start up without a connection string to the datbase, fool!");
}


builder.Services.AddMarten(options =>
{
    options.Connection(locationsConnectionString);
    options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
});

var app = builder.Build();

app.UseCors();
// This would have been in the Configure method in startup
// the thing that will actually handle incoming requests.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
// Create the "Route Table" - phone book.
// GET /status -> StatusController -> GetStatus


app.Run(); // Blocking - where the "kestrel web server starts listening"
