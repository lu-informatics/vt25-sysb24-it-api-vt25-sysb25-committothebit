namespace Appetite.Api.DTOs;

public class RecipeIngredientDto
{
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }
    public double Amount { get; set; }
}
