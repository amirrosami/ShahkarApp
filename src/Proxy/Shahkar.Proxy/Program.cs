using Shahkar.Proxy.Configurations;
using Shahkar.Proxy.RabbitClient;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RabbitRpcClient>();
var app = builder.Build();


app.Use(async (context, next) =>
{
    var stopWatch = new Stopwatch();
    stopWatch.Start();
    Console.WriteLine($"Start Request...{DateTime.Now.Second}" );
    await next();
    stopWatch.Stop();
    Console.WriteLine($"time is {DateTime.Now.Second}");
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
