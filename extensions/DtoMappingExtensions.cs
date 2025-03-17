using System.Linq;
using Appetite.Api.Models;
using Appetite.Api.DTOs;

namespace Appetite.Api.Extensions;

public static class DtoMappingExtensions
{
  public static RecipeDto ToDto(this Recipe recipe)
{
    return new RecipeDto
    {
        Id = recipe.Id,
        Name = recipe.Name,
        Data = recipe.Data,
        CookingTime = recipe.CookingTime,
        Servings = recipe.Servings,
        DifficultyLevel = recipe.DifficultyLevel,

        // Convert each related ingredient to its name
        IngredientNames = recipe.RecipeIngredients
            .Select(ri => ri.Ingredient?.Name) // ingredient could be null, so use ?
            .Where(name => !string.IsNullOrEmpty(name))
            .ToList()
    };
}


    // Existing mappings for Ingredient and RecipeIngredient remain unchanged.
    public static IngredientDto ToDto(this Ingredient ingredient)
    {
        return new IngredientDto
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Category = ingredient.Category,
            Unit = ingredient.Unit,
            DietTag = ingredient.DietTag
        };
    }

    public static RecipeIngredientDto ToDto(this RecipeIngredient recipeIngredient)
    {
        return new RecipeIngredientDto
        {
            RecipeId = recipeIngredient.RecipeId,
            IngredientId = recipeIngredient.IngredientId,
            Amount = recipeIngredient.Amount
        };
    }
}
