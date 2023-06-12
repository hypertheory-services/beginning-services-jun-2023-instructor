using OnCallDeveloperApi.Models;
using OnCallDeveloperApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISystemTime, SystemTime>();
builder.Services.AddScoped<IProvideSupportSchedule, HolidayBasedSupportSchedule>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/oncalldeveloper", (IProvideSupportSchedule supportSchedule) =>
{
    OnCallDeveloperModel response;
    if (supportSchedule.InternalSupportAvailable)
    {
        response = new OnCallDeveloperModel
        {
            Name = "Robert Smith",
            Phone = "888-8888",
            Email = "bob@company.com"
        };
    }
    else
    {
        response = new OnCallDeveloperModel
        {
            Name = "House of Outsourced Support, Inc.",
            Phone = "800 111-1111",
            Email = "support@house-of-outsourced-support.com"
        };
    }
    return Results.Ok(response);
});

app.Run();

