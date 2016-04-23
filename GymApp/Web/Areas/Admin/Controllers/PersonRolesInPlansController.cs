using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces;
using Domain;
using Web.Areas.Admin.ViewModels;
using Web.Controllers;
using Web.Helpers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PersonRoleInPlansController : BaseController
    {
        //private DataBaseContext db = new DataBaseContext();
        private readonly IUOW _uow;

        public PersonRoleInPlansController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin/PersonRoleInPlans
        public ActionResult Index()
        {
            var vm = _uow.PersonRoleInPlans.All;
            return View(vm);
        }

        // GET: Admin/PersonRoleInPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonRoleInPlan personRoleInPlan = _uow.PersonRoleInPlans.GetById(id);
            if (personRoleInPlan == null)
            {
                return HttpNotFound();
            }
            return View(personRoleInPlan);
        }

        // GET: Admin/PersonRoleInPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PersonRoleInPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonRoleInPlanCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.PersonRoleInPlan == null) vm.PersonRoleInPlan = new PersonRoleInPlan();

                vm.PersonRoleInPlan.RoleName = new MultiLangString(vm.RoleName,
                    CultureHelper.GetCurrentNeutralUICulture(), vm.RoleName,
                    nameof(vm.PersonRoleInPlan) + "." + vm.PersonRoleInPlan.PersonRoleInPlanId + "." + nameof(vm.PersonRoleInPlan.RoleName));
                _uow.PersonRoleInPlans.Add(vm.PersonRoleInPlan);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Admin/PersonRoleInPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonRoleInPlan personRoleInPlan = _uow.PersonRoleInPlans.GetById(id);
            if (personRoleInPlan == null)
            {
                return HttpNotFound();
            }
            var vm = new PersonRoleInPlanCreateEditViewModel()
            {
                PersonRoleInPlan = personRoleInPlan,
                RoleName = personRoleInPlan.RoleName.Translate()
            };
            return View(vm);
        }

        // POST: Admin/PersonRoleInPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonRoleInPlanCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.PersonRoleInPlan.RoleName = _uow.MultiLangStrings.GetById(vm.PersonRoleInPlan.PersonRoleInPlanId);
                vm.PersonRoleInPlan.RoleName.SetTranslation(vm.RoleName, CultureHelper.GetCurrentNeutralUICulture(),
                    nameof(vm.PersonRoleInPlan) + "." + vm.PersonRoleInPlan.PersonRoleInPlanId + "." + nameof(vm.PersonRoleInPlan.RoleName));
                _uow.PersonRoleInPlans.Update(vm.PersonRoleInPlan);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Admin/PersonRoleInPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonRoleInPlan personRoleInPlan = _uow.PersonRoleInPlans.GetById(id);
            if (personRoleInPlan == null)
            {
                return HttpNotFound();
            }
            return View(personRoleInPlan);
        }

        // POST: Admin/PersonRoleInPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.PersonRoleInPlans.Delete(id);
            _uow.Commit();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
