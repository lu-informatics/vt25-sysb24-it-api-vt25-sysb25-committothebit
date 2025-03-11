using Appetite.Api.DTOs;

namespace Appetite.Api.Services;

public interface IRecipeIngredientService
{
Task<IEnumerable<RecipeIngredientDto>> GetAllRecipeIngredientsAsync();
        Task<RecipeIngredientDto?> GetRecipeIngredientAsync(int recipeId, int ingredientId);
        Task<RecipeIngredientDto> CreateRecipeIngredientAsync(RecipeIngredientDto recipeIngredientDto);
        Task<bool> UpdateRecipeIngredientAsync(RecipeIngredientDto recipeIngredientDto);
        Task<bool> DeleteRecipeIngredientAsync(int recipeId, int ingredientId);
  
}
