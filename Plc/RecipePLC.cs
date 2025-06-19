using System.Collections.Generic;

namespace TecxRecipeAX
{
    public class RecipePLC
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public List<RecipeStagePLC> Stages { get; set; } = new List<RecipeStagePLC>();
    }

    //public class RecipeStageToPLC
    //{
    //    public int StageNumb { get; set; }
    //    public string BaseStageNumb { get; set; }
    //    public string Name { get; set; }
    //    public string Tag { get; set; }
    //    public bool Auto { get; set; }
    //    public List<ParamInfo> Parameters { get; set; } = new List<ParamInfo>();
    //}
}
