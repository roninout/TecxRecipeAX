using System.Collections.Generic;

namespace TecxRecipeAX
{
    public class RecipeStagePLC
    {
        public int StageNumb { get; set; }
        public string BaseStageNumb { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public bool Auto { get; set; }
        public List<ParamInfo> Parameters { get; set; } = new List<ParamInfo>();
    }
}
