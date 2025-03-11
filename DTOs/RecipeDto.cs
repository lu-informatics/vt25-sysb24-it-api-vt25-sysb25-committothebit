namespace Appetite.Api.DTOs;

public class RecipeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Data { get; set; }
    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public string? DifficultyLevel { get; set; }
}


