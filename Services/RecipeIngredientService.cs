
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
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly RecipeContext _context;

        public RecipeIngredientService(RecipeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecipeIngredientDto>> GetAllRecipeIngredientsAsync()
        {
            var recipeIngredients = await _context.RecipeIngredients.ToListAsync();
            return recipeIngredients.Select(ri => ri.ToDto());
        }

        public async Task<RecipeIngredientDto?> GetRecipeIngredientAsync(int recipeId, int ingredientId)
        {
            var recipeIngredient = await _context.RecipeIngredients
                .FirstOrDefaultAsync(ri => ri.RecipeId == recipeId && ri.IngredientId == ingredientId);

            return recipeIngredient?.ToDto();
        }

        public async Task<RecipeIngredientDto> CreateRecipeIngredientAsync(RecipeIngredientDto recipeIngredientDto)
        {
            var recipeIngredient = new RecipeIngredient
            {
                RecipeId = recipeIngredientDto.RecipeId,
                IngredientId = recipeIngredientDto.IngredientId,
                Amount = recipeIngredientDto.Amount
            };

            _context.RecipeIngredients.Add(recipeIngredient);
            await _context.SaveChangesAsync();

            return recipeIngredient.ToDto();
        }

        public async Task<bool> UpdateRecipeIngredientAsync(RecipeIngredientDto recipeIngredientDto)
        {
            var recipeIngredient = await _context.RecipeIngredients
                .FirstOrDefaultAsync(ri => ri.RecipeId == recipeIngredientDto.RecipeId
                                           && ri.IngredientId == recipeIngredientDto.IngredientId);
            if (recipeIngredient == null)
                return false;

            // Endast uppdatering av Amount (lägg till mer logik om du vill tillåta uppdatering av RecipeId/IngredientId)
            recipeIngredient.Amount = recipeIngredientDto.Amount;

            _context.RecipeIngredients.Update(recipeIngredient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRecipeIngredientAsync(int recipeId, int ingredientId)
        {
            var recipeIngredient = await _context.RecipeIngredients
                .FirstOrDefaultAsync(ri => ri.RecipeId == recipeId && ri.IngredientId == ingredientId);

            if (recipeIngredient == null)
                return false;

            _context.RecipeIngredients.Remove(recipeIngredient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
