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

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials());

app.Run();
