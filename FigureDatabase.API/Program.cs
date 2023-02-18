using FigureDatabase.API.Context;
using FigureDatabase.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true);

builder.Services.AddDbContext<FigureDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("FIGURE_DB")));

builder.Services.AddScoped<IFigureRepository, MockFigureRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();