using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using PagedList;

namespace Web.ViewModels
{
    public class PersonIndexViewModel
    {
        public IPagedList<Person> Persons { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
        public string Filter { get; set; }
    }
}