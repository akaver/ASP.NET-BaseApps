using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ExerciseType : BaseEntity
    {
        [Key]
        [Display(ResourceType = typeof(Resources.Domain), Name = "EntityPrimaryKey")]
        public int ExerciseTypeId { get; set; }

        [ForeignKey(nameof(ExerciseTypeName))]
        public int? ExerciseTypeNameId { get; set; }
        public virtual MultiLangString ExerciseTypeName { get; set; }

        [ForeignKey(nameof(ExerciseTypeDescription))]
        public int? ExerciseTypeDescriptionId { get; set; }
        public virtual MultiLangString ExerciseTypeDescription { get; set; }

        public virtual List<Exercise> Exercises { get; set; }
    }
}
