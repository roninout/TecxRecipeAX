using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechRecipeAX
{
    public class RecipeParameter
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public double value { get; set; } = 0.0;
        public string description { get; set; } = string.Empty;
        public bool active { get; set; } = true;

        // Внешний ключ к стадии рецепта
        public int? recipeStageId { get; set; }
        [ForeignKey("recipeStageId")]
        public virtual RecipeStage recipeStage { get; set; }

        // Внешний ключ на BaseParameter
        public int baseParameterId { get; set; }
        [ForeignKey("baseParameterId")]
        public virtual BaseParameter baseParameter { get; set; }
    }
}
