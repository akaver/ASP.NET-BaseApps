using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PlanType : BaseEntity
    {
        [Key]
        [Display(ResourceType = typeof(Resources.Domain), Name = "EntityPrimaryKey")]
        public int PlanTypeId { get; set; }

        //public string PlanTypeName { get; set; }
        [ForeignKey(nameof(PlanTypeName))]
        public int? PlanTypeNameId { get; set; }
        public virtual MultiLangString PlanTypeName { get; set; }

        //public string Description { get; set; }
        [ForeignKey(nameof(PlanTypeDescription))]
        public int? PlanTypeDescriptionId { get; set; }
        public virtual MultiLangString PlanTypeDescription { get; set; }

        public virtual List<Plan> Plans { get; set; }
    }
}
