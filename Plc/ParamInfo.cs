using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecxRecipeAX
{
    public class ParamInfo
    {
        public int BaseParameterId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public double Value { get; set; }
        public string Tag { get; set; }
        public bool HasDifference { get; set; }
        public ParamKind Kind { get; set; }
        public ParamFormat Format { get; set; }
    }
}
