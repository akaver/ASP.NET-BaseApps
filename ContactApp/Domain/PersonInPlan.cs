using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PersonInPlan
    {
        [Key]
        [Display(ResourceType = typeof(Resources.Domain), Name = "EntityPrimaryKey")]
        public int PersonInPlanId { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        public int PersonRoleInPlanId { get; set; }
        public PersonRoleInPlan PersonRoleInPlan { get; set; }
    }
}
