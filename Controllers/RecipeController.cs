using Appetite.Api.DTOs;
using Appetite.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appetite.Api.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/recipe
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RecipeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        // GET: api/recipe/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        // POST: api/recipe
        [HttpPost]
        [ProducesResponseType(typeof(RecipeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeDto recipeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRecipe = await _recipeService.CreateRecipeAsync(recipeDto);

            return CreatedAtAction(nameof(GetRecipeById), new { id = createdRecipe.Id }, createdRecipe);
        }

        // PUT: api/recipe/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeDto recipeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != recipeDto.Id)
                return BadRequest("Mismatched recipe ID");

            var updated = await _recipeService.UpdateRecipeAsync(recipeDto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/recipe/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var deleted = await _recipeService.DeleteRecipeAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
