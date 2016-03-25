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
        [MaxLength(128, ErrorMessageResourceName = "FirstnameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = "FirstnameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = nameof(Resources.Domain.Firstname), ResourceType = typeof(Resources.Domain))]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(128, ErrorMessageResourceName = "LastnameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [MinLength(1, ErrorMessageResourceName = "LastnameLengthError", ErrorMessageResourceType = typeof(Resources.Domain))]
        [Display(Name = nameof(Resources.Domain.Lastname), ResourceType = typeof(Resources.Domain))]
        public string Lastname { get; set; }


        // demo fields for date/time handling
        // DataType attribute is used mainly for UI only, to render correct elements
        // https://msdn.microsoft.com/en-us/library/ms186724.aspx#DateandTimeDataTypes
        // look also into DBContext, SQL is forced to use certain fieldtypes according to this attribute
        // this is only tested against MS SQL / LocalDb
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual UserInt User { get; set; }

        public virtual List<Contact> Contacts { get; set; } = new List<Contact>();

        // not mapped properties, just getters
        public string FirstLastname => (Firstname + " " + Lastname).Trim();
        public string LastFirstname => (Lastname + " " + Firstname).Trim();


    }
}
