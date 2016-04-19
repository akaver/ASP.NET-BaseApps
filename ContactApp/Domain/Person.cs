using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{
    public class Person : BaseEntity
    {
        public int PersonId { get; set; }

        [Required]
        [MaxLength(128, ErrorMessageResourceName = "FirstNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = "FirstNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Domain))]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128, ErrorMessageResourceName = "LastNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = "LastNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Domain))]
        public string LastName { get; set; }

        [Range(0, 2048, ErrorMessageResourceName = "HeightOutOfBoundsError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = "Height", ResourceType = typeof(Resources.Domain))]
        public int? Height { get; set; }

        [Range(0, 2048, ErrorMessageResourceName = "WeightOutOfBoundsError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = "Weight", ResourceType = typeof(Resources.Domain))]
        public int? Weight { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }


        [Display(Name = nameof(Resources.Domain.Person_DateTime2), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.DateTime, ErrorMessageResourceName = "FieldMustBeDataTypeDateTime", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime DateTime2 { get; set; }

        [Display(Name = nameof(Resources.Domain.Person_Date2), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.Date, ErrorMessageResourceName = "FieldMustBeDataTypeDate", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime Date2 { get; set; }

        [Display(Name = nameof(Resources.Domain.Person_Time2), ResourceType = typeof(Resources.Domain))]
        [DataType(DataType.Time, ErrorMessageResourceName = "FieldMustBeDataTypeTime", ErrorMessageResourceType = typeof(Resources.Common), ErrorMessage = null)]
        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public DateTime Time2 { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        public virtual List<Contact> Contacts { get; set; }
        public virtual List<PersonInPlan> PersonInPlans { get; set; }
        public virtual List<Plan> CreatorInPlans { get; set; }
        public virtual List<Participation> Participations { get; set; }

        // not mapped properties, just getters
        public string FirstLastName => (FirstName + " " + LastName).Trim();
        public string LastFirstName => (LastName + " " + FirstName).Trim();


    }
}
