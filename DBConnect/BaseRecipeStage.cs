using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechRecipeAX
{
    public class BaseRecipeStage
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;

        // Коллекция параметров (One-to-Many)
        public virtual ICollection<BaseParameter> BaseParameters { get; set; } = new List<BaseParameter>();
    }
}
