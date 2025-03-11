using Appetite.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using Appetite.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RecipeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppetiteDatabase"));
});

// Add services to the container.
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();