using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Plan : BaseEntity
    {
        [Key]
        [Display(ResourceType = typeof(Resources.Domain), Name = "EntityPrimaryKey")]
        public int PlanId { get; set; }

        [ForeignKey(nameof(PlanName))]
        public int PlanNameId { get; set; }
        public virtual MultiLangString PlanName { get; set; }

        public int? Rating { get; set; }

        [ForeignKey(nameof(PlanDescription))]
        public int? PlanDescriptionId { get; set; }
        public virtual MultiLangString PlanDescription { get; set; }

        [ForeignKey(nameof(PlanInstructions))]
        public int? PlanInstructionsId { get; set; }
        public virtual MultiLangString PlanInstructions { get; set; }

        public int PlanTypeId { get; set; }
        public virtual PlanType PlanType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateClosed { get; set; }

        public string Duration { get; set; }

        public virtual List<PersonInPlan> People { get; set; }
        public virtual List<Workout> Workouts { get; set; }
    }
}
