using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Exercise : BaseEntity
    {
        public int ExerciseId { get; set; }

        public int ExerciseTypeId { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }
        [Required]
        [MaxLength(128, ErrorMessageResourceName = "ExerciseNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = "ExerciseNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public string ExerciseName { get; set; }

        [MaxLength(32768, ErrorMessageResourceName = "DescriptionLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public string Description { get; set; }

        [MaxLength(32768, ErrorMessageResourceName = "InstructionsLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public string Instructions { get; set; }

        [MaxLength(512, ErrorMessageResourceName = "VideoURLLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public string VideoUrl { get; set; }

        [Range(0, 10, ErrorMessageResourceName = "RatingOutOfBoundsError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public int? Rating { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public virtual List<ExerciseInWorkout> ExercisesInWorkouts { get; set; }
    }
}
