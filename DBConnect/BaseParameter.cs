using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TecxRecipeAX;

namespace TechRecipeAX
{
    public class BaseParameter
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public int type { get; set; } // numb
        public string description { get; set; } = string.Empty;
        public string tag { get; set; } = string.Empty;        
        public ParamKind kind { get; set; } = ParamKind.InputField;
        public ParamFormat format { get; set; } = ParamFormat.Default;
        public int? baseRecipeId { get; set; }

        // Вспомогательное свойство для биндинга
        [NotMapped]
        public int kindInt
        {
            get => (int)kind;
            set => kind = (ParamKind)value;
        }

        [NotMapped]
        public int formatInt
        {
            get => (int)format;
            set => format = (ParamFormat)value;
        }

        // Автоматическая связь Many-to-Many
        public virtual ICollection<BaseRecipeStage> BaseRecipeStages { get; set; } = new List<BaseRecipeStage>();
    }

}
