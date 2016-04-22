using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class PlanTypeCreateEditViewModel
    {
        public PlanType PlanType { get; set; }
        public string PlanTypeName { get; set; }
        public string Description { get; set; }
    }
}