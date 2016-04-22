using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class PersonRoleInPlanCreateEditViewModel
    {
        public PersonRoleInPlan PersonRoleInPlan { get; set; }
        public string RoleName { get; set; }
    }
}