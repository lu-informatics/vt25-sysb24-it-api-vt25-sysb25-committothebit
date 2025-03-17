public class RecipeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }

    private string? _data;
    public string? Data 
    { 
        get => string.IsNullOrWhiteSpace(_data) ? "{}" : _data;
        set => _data = value;
    }

    public int CookingTime { get; set; }
    public int Servings { get; set; }
    public string? DifficultyLevel { get; set; }

    // Add this property to expose the ingredient names in JSON
    public List<string>? IngredientNames { get; set; }
}
