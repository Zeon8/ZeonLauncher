using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Launcher.Web.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LauncherWebContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LauncherWebContext") ?? throw new InvalidOperationException("Connection string 'LauncherWebContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
