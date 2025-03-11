using Appetite.Api.Models;
using Microsoft.EntityFrameworkCore;

public class RecipeContext : DbContext
{
    public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tell EF Core that RecipeId + IngredientId is the composite PK
        modelBuilder.Entity<RecipeIngredient>()
            .HasKey(ri => new { ri.RecipeId, ri.IngredientId });
    }
}
