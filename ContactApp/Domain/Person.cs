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
    public class Person
    {
        public int PersonId { get; set; }

        [Required]
        [MaxLength(128, ErrorMessageResourceName = "FirstNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = "FirstNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = "Firstname", ResourceType = typeof(Resources.Domain))]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128, ErrorMessageResourceName = "LastNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = "LastNameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = "Firstname", ResourceType = typeof(Resources.Domain))]
        public string LastName { get; set; }

        [Range(0, 2048, ErrorMessageResourceName = "HeightOutOfBoundsError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = "Height", ResourceType = typeof(Resources.Domain))]
        public int? Height { get; set; }

        [Range(0, 2048, ErrorMessageResourceName = "WeightOutOfBoundsError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = "Weight", ResourceType = typeof(Resources.Domain))]
        public int? Weight { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "BirthDate", ResourceType = typeof(Resources.Domain))]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "DateCreated", ResourceType = typeof(Resources.Domain))]
        public DateTime? DateCreated { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Time", ResourceType = typeof(Resources.Domain))]
        public DateTime Time { get; set; }

        public virtual List<Contact> Contacts { get; set; }
        public virtual List<PersonInPlan> PersonInPlans { get; set; }
        public virtual List<Plan> CreatorInPlans { get; set; }
        public virtual List<Participation> Participations { get; set; }

        // not mapped properties, just getters
        public string FirstLastname => (FirstName + " " + LastName).Trim();
        public string LastFirstname => (LastName + " " + FirstName).Trim();


    }
}
