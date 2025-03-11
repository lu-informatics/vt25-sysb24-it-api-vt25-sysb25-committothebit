
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
    public class IngredientService : IIngredientService
    {
        private readonly RecipeContext _context;

        public IngredientService(RecipeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
        {
            var ingredients = await _context.Ingredients.ToListAsync();
            return ingredients.Select(i => i.ToDto());
        }

        public async Task<IngredientDto?> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            return ingredient?.ToDto();
        }

        public async Task<IngredientDto> CreateIngredientAsync(IngredientDto ingredientDto)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientDto.Name,
                Category = ingredientDto.Category,
                Unit = ingredientDto.Unit,
                DietTag = ingredientDto.DietTag
            };

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return ingredient.ToDto();
        }

        public async Task<bool> UpdateIngredientAsync(IngredientDto ingredientDto)
        {
            var ingredient = await _context.Ingredients.FindAsync(ingredientDto.Id);
            if (ingredient == null)
                return false;

            ingredient.Name = ingredientDto.Name;
            ingredient.Category = ingredientDto.Category;
            ingredient.Unit = ingredientDto.Unit;
            ingredient.DietTag = ingredientDto.DietTag;

            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteIngredientAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
                return false;

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
