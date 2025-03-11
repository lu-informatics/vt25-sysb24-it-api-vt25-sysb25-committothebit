using Appetite.Api.DTOs;

namespace Appetite.Api.Services;

public interface IRecipeService
{
Task<IEnumerable<RecipeDto>> GetAllRecipesAsync();
        Task<RecipeDto?> GetRecipeByIdAsync(int id);
        Task<RecipeDto> CreateRecipeAsync(RecipeDto recipeDto);
        Task<bool> UpdateRecipeAsync(RecipeDto recipeDto);
        Task<bool> DeleteRecipeAsync(int id);
}
