using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DAL.Interfaces;
using Domain;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HelperDemoController : Controller
    {

        private readonly IUOW _uow;

        public HelperDemoController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: HelperDemo
        public ActionResult Index()
        {
            var vm = new HelperDemoIndexViewModel()
            {
                DropDownList = new SelectList(_uow.Persons.All,nameof(Person.PersonId),nameof(Person.Firstname)),
                ListBoxList = new MultiSelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.Firstname)),


            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HelperDemoIndexViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //save to db, redirect to some other view
            }

            vm.DropDownList = new SelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.Firstname), vm.DropDownListId);
            vm.ListBoxList = new SelectList(_uow.Persons.All, nameof(Person.PersonId), nameof(Person.Firstname), vm.DropDownListId);


            return View(vm);
        }


    }
}