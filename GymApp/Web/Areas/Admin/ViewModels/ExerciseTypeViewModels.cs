using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class ExerciseTypeCreateEditViewModel
    {
        public ExerciseType ExerciseType { get; set; }
        public string ExerciseTypeName { get; set; }
        public string Description { get; set; }
    }
}