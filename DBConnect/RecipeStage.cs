using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechRecipeAX
{
    public class RecipeStage
    {
        [Key]
        public int id { get; set; }
        public int stageNumb { get; set; }
        public string name { get; set; } = string.Empty;
        public bool auto { get; set; } = false;
        public string description { get; set; } = string.Empty;

        // Добавляем внешний ключ
        public int? recipeId { get; set; } // Nullable, чтобы стадия могла быть свободной
        [ForeignKey("recipeId")]
        public virtual Recipe recipe { get; set; }

        // Новое: Связь с базовой стадией
        public int baseRecipeStageId { get; set; } // ID шаблонной стадии
        [ForeignKey("baseRecipeStageId")]
        public virtual BaseRecipeStage baseRecipeStage { get; set; }

        // Коллекция параметров (One-to-Many)
        public virtual ICollection<RecipeParameter> RecipeParameters { get; set; } = new List<RecipeParameter>();
    }
}
