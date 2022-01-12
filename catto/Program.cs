using System.Text.Json.Serialization;
using catto.Models;
using catto.Provider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CattoContext>();
builder.Services.AddTransient<UserProvider>();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CattoContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    "default",
    "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
