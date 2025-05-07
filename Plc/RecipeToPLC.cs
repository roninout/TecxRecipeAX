using System.Collections.Generic;
using System.Linq;
using TechRecipeAX;

namespace TecxRecipeAX
{
    public class RecipeToPLC
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public List<RecipeStageToPLC> Stages { get; set; } = new List<RecipeStageToPLC>();


    }



}
