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
using Web.Helpers;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlanTypesController : Controller
    {
        //private DataBaseContext db = new DataBaseContext();
        private readonly IUOW _uow;

        public PlanTypesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin/PlanTypes
        public ActionResult Index()
        {
            var vm = _uow.PlanTypes.All;
            return View(vm);
        }

        // GET: Admin/PlanTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanType planType = _uow.PlanTypes.GetById(id);
            if (planType == null)
            {
                return HttpNotFound();
            }
            return View(planType);
        }

        // GET: Admin/PlanTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PlanTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlanTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.PlanType == null) vm.PlanType = new PlanType();

                vm.PlanType.PlanTypeName = new MultiLangString(vm.PlanTypeName,
                   CultureHelper.GetCurrentNeutralUICulture(), vm.PlanTypeName,
                   nameof(vm.PlanType) + "." + vm.PlanType.PlanTypeId + "." + nameof(vm.PlanType.PlanTypeName));
                //TODO lisada kirjelduse tõlke lisamine

                _uow.PlanTypes.Add(vm.PlanType);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Admin/PlanTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanType planType = _uow.PlanTypes.GetById(id);
            if (planType == null)
            {
                return HttpNotFound();
            }
            var vm = new PlanTypeCreateEditViewModel()
            {
                PlanType = planType,
                PlanTypeName = planType.PlanTypeName.Translate(),
                Description = planType.PlanTypeDescription.Translate()
            };
            return View(vm);
        }

        // POST: Admin/PlanTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlanTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.PlanType.PlanTypeName = _uow.MultiLangStrings.GetById(vm.PlanType.PlanTypeId);
                vm.PlanType.PlanTypeName.SetTranslation(vm.PlanTypeName, CultureHelper.GetCurrentNeutralUICulture(),
                    nameof(vm.PlanType) + "." + vm.PlanType.PlanTypeId + "." + nameof(vm.PlanType.PlanTypeName));
                //TODO lisada kirjelduse tõlke muutmine
                _uow.PlanTypes.Update(vm.PlanType);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Admin/PlanTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanType planType = _uow.PlanTypes.GetById(id);
            if (planType == null)
            {
                return HttpNotFound();
            }
            return View(planType);
        }

        // POST: Admin/PlanTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.PlanTypes.Delete(id);
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
