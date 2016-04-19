using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Competition : BaseEntity
    {
        public int CompetitionId { get; set; }

        [Required]
        [MaxLength(128, ErrorMessageResourceName = "CompetitionNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = "CompetitionNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        public string CompetitionName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateStart { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateEnd { get; set; }

        [ForeignKey(nameof(CompetitionDescription))]
        public int CompetitionDescriptionId { get; set; }
        public virtual MultiLangString CompetitionDescription { get; set; }

        public virtual List<Participation> Participations { get; set; }
    }
}
