using Appetite.Api.DTOs;
using Appetite.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appetite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientService _recipeIngredientService;

        public RecipeIngredientController(IRecipeIngredientService recipeIngredientService)
        {
            _recipeIngredientService = recipeIngredientService;
        }

        // GET: api/recipeingredient
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RecipeIngredientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRecipeIngredients()
        {
            var recipeIngredients = await _recipeIngredientService.GetAllRecipeIngredientsAsync();
            return Ok(recipeIngredients);
        }

        // GET: api/recipeingredient/{recipeId}/{ingredientId}
        [HttpGet("{recipeId}/{ingredientId}")]
        [ProducesResponseType(typeof(RecipeIngredientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecipeIngredient(int recipeId, int ingredientId)
        {
            var recipeIngredient = await _recipeIngredientService.GetRecipeIngredientAsync(recipeId, ingredientId);
            if (recipeIngredient == null)
                return NotFound();

            return Ok(recipeIngredient);
        }

        // POST: api/recipeingredient
        [HttpPost]
        [ProducesResponseType(typeof(RecipeIngredientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRecipeIngredient([FromBody] RecipeIngredientDto recipeIngredientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRecipeIngredient = await _recipeIngredientService.CreateRecipeIngredientAsync(recipeIngredientDto);

            // Because there are two keys, we pass both in CreatedAtAction
            return CreatedAtAction(nameof(GetRecipeIngredient),
                new { recipeId = createdRecipeIngredient.RecipeId, ingredientId = createdRecipeIngredient.IngredientId },
                createdRecipeIngredient);
        }

        // PUT: api/recipeingredient/{recipeId}/{ingredientId}
        [HttpPut("{recipeId}/{ingredientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRecipeIngredient(int recipeId, int ingredientId, [FromBody] RecipeIngredientDto recipeIngredientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure the keys in the URL match the DTO
            if (recipeId != recipeIngredientDto.RecipeId || ingredientId != recipeIngredientDto.IngredientId)
                return BadRequest("Mismatched recipeId or ingredientId");

            var updated = await _recipeIngredientService.UpdateRecipeIngredientAsync(recipeIngredientDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/recipeingredient/{recipeId}/{ingredientId}
        [HttpDelete("{recipeId}/{ingredientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRecipeIngredient(int recipeId, int ingredientId)
        {
            var deleted = await _recipeIngredientService.DeleteRecipeIngredientAsync(recipeId, ingredientId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
