using PruebaTecnicaWS.Interfaces;
using PruebaTecnicaWS.Models;
using PruebaTecnicaWS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConnectionWS connectionWS = builder.Configuration.GetSection("ConnectionWS").Get<ConnectionWS>();
builder.Services.AddScoped<IApiConsumer>(options => new ApiConsumerService(builder.Configuration.GetSection("BaseURL").Get<string>()));
builder.Services.AddScoped<IFactura, FacturaService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAlllowSpecificOrigins", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("_myAlllowSpecificOrigins");

app.Run();
