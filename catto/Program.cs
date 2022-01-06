using catto.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CattoContext>();
builder.Services.AddTransient<UserProvider>();

var app = builder.Build();

app.UseSwagger();
app.MapGet("/", () => "Hello World!");
app.UseSwaggerUI();
app.Run();