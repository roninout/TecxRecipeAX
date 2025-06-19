using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TecxRecipeAX
{
    public class RecipePage
    {
        [Key]
        public int id { get; set; }
        public string page { get; set; } = string.Empty;
        public int recipeId { get; set; }
        public int baseRecipeId { get; set; }
    }
}
