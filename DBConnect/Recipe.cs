using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechRecipeAX
{
    public class Recipe
    {
        [Key]
        public int id { get; set; }
        public int numb { get; set; }
        public string name { get; set; } = string.Empty;
        public int? baseRecipeId { get; set; }

        public virtual ICollection<RecipeStage> RecipeStages { get; set; }
    }
}
