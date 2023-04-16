using Figure.WebAPI.Context;
using Figure.WebAPI.Repositories;
using Figure.WebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true);

builder.Services.AddDbContext<FiguresDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("FIGUREDB")));

builder.Services.AddScoped<FigureRepository>();
builder.Services.AddScoped<FigureService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();