using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Add this if not already present


namespace Appetite.Api.Models;

    [Table("Ingredient")]
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ingredientId")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        [Column("Category")]
        [MaxLength(100)]
        public string? Category { get; set; }

        [Required]
        [Column("Unit")]
        [MaxLength(50)]
        public string? Unit { get; set; }

        [Required]
        [Column("DietTag")]
        [MaxLength(255)]
        public string? DietTag { get; set; }

        // Navigation properties
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
       
    }
