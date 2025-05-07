using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecxRecipeAX
{
    public class RecipeStageToPLC
    {
        public int StageNumb { get; set; }
        public string BaseStageNumb { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public bool Auto { get; set; }
        public List<ParamInfo> Parameters { get; set; } = new List<ParamInfo>(); 
    }

    public class ParamInfo
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public double Value { get; set; }
        public string Tag { get; set; }
    }
}
