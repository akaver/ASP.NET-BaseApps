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

        [MaxLength(128, ErrorMessageResourceName = "FirstnameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = nameof(Resources.Domain.Firstname), ResourceType = typeof(Resources.Domain))]
        public string Firstname { get; set; }

        [MaxLength(128, ErrorMessageResourceName = "LastnameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = nameof(Resources.Domain.Lastname), ResourceType = typeof(Resources.Domain))]
        public string Lastname { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        public virtual List<Contact> Contacts { get; set; } = new List<Contact>();

        // not mapped properties, just getters
        public string FirstLastname => (Firstname + " " + Lastname).Trim();
        public string LastFirstname => (Lastname + " " + Firstname).Trim();


    }
}
