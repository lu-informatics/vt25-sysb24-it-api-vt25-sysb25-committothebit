using Appetite.Api.DTOs;
using Appetite.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appetite.Api.Controllers
{
    [Route("api/ingredient")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        // GET: api/ingredient
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IngredientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        // GET: api/ingredient/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
                return NotFound();
            
            return Ok(ingredient);
        }

        // POST: api/ingredient
        [HttpPost]
        [ProducesResponseType(typeof(IngredientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDto ingredientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdIngredient = await _ingredientService.CreateIngredientAsync(ingredientDto);

            // Return 201 + the location of the newly created resource
            return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.Id }, createdIngredient);
        }

        // PUT: api/ingredient/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] IngredientDto ingredientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure the ID in the URL matches the DTO
            if (id != ingredientDto.Id)
                return BadRequest("Mismatched ingredient ID");

            var updated = await _ingredientService.UpdateIngredientAsync(ingredientDto);
            if (!updated)
                return NotFound(); // If ingredient not found in DB

            return NoContent(); // 204
        }

        // DELETE: api/ingredient/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var deleted = await _ingredientService.DeleteIngredientAsync(id);
            if (!deleted)
                return NotFound(); // If ingredient not found in DB

            return NoContent();
        }
    }
}
