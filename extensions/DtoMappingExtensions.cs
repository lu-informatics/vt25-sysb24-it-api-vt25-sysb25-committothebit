using Appetite.Api.DTOs;
using Appetite.Api.Models;

namespace Appetite.Api.Extensions;

public static class DtoMappingExtensions
{

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

public static RecipeDto ToDto(this Recipe recipe)
        {
            return new RecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Data = recipe.Data,
                CookingTime = recipe.CookingTime,
                Servings = recipe.Servings,
                DifficultyLevel = recipe.DifficultyLevel
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