using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ExerciseInWorkout
    {
        public int ExerciseInWorkoutId { get; set; }

        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public int WorkoutId { get; set; }
        public virtual Workout Workout { get; set; }
        [Range(0, 128, ErrorMessageResourceName = "SetCountOutOfBoundsError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public int? Sets { get; set; }
        [Range(0, 256, ErrorMessageResourceName = "RepCountOutOfBoundsError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public int? Repetitions { get; set; }
        //TODO start time and end time
        public int? Time { get; set; }
        [Range(0, 128, ErrorMessageResourceName = "RepWeightOutOfBoundsError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public int? Weight { get; set; }
    }
}
