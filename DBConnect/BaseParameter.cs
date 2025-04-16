using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechRecipeAX
{
    public class BaseParameter
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int type { get; set; }
        public string description { get; set; } = string.Empty;

        // Автоматическая связь Many-to-Many
        public virtual ICollection<BaseRecipeStage> BaseRecipeStages { get; set; } = new List<BaseRecipeStage>();
    }
}
