
using Appetite.Api.DTOs;
using Appetite.Api.Extensions;
using Appetite.Api.Models;
using Appetite.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Appetite.Api
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeContext _context;

        public RecipeService(RecipeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecipeDto>> GetAllRecipesAsync()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return recipes.Select(r => r.ToDto());
        }

        public async Task<RecipeDto?> GetRecipeByIdAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            return recipe?.ToDto();
        }

        public async Task<RecipeDto> CreateRecipeAsync(RecipeDto recipeDto)
        {
            var recipe = new Recipe
            {
                Name = recipeDto.Name,
                Data = recipeDto.Data,
                CookingTime = recipeDto.CookingTime,
                Servings = recipeDto.Servings,
                DifficultyLevel = recipeDto.DifficultyLevel
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return recipe.ToDto();
        }

        public async Task<bool> UpdateRecipeAsync(RecipeDto recipeDto)
        {
            var recipe = await _context.Recipes.FindAsync(recipeDto.Id);
            if (recipe == null)
                return false;

            recipe.Name = recipeDto.Name;
            recipe.Data = recipeDto.Data;
            recipe.CookingTime = recipeDto.CookingTime;
            recipe.Servings = recipeDto.Servings;
            recipe.DifficultyLevel = recipeDto.DifficultyLevel;

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
