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
    public class ExerciseTypesController : BaseController
    {
        //private DataBaseContext db = new DataBaseContext();
        private readonly IUOW _uow;

        public ExerciseTypesController(IUOW uow)
        {
            _uow = uow;
        }

        // GET: Admin/ExerciseTypes
        public ActionResult Index()
        {
            var vm = _uow.ExerciseTypes.All;
            return View(vm);
        }

        // GET: Admin/ExerciseTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExerciseType exerciseType = _uow.ExerciseTypes.GetById(id);
            if (exerciseType == null)
            {
                return HttpNotFound();
            }
            return View(exerciseType);
        }

        // GET: Admin/ExerciseTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ExerciseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExerciseTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.ExerciseType == null) vm.ExerciseType = new ExerciseType();

                vm.ExerciseType.ExerciseTypeName = new MultiLangString(vm.ExerciseTypeName, CultureHelper.GetCurrentNeutralUICulture(), vm.ExerciseTypeName, nameof(vm.ExerciseType)+"."+vm.ExerciseType.ExerciseTypeId + "." + nameof(vm.ExerciseType.ExerciseTypeName));
                vm.ExerciseType.ExerciseTypeDescription = new MultiLangString(vm.Description, CultureHelper.GetCurrentNeutralUICulture(), vm.Description, nameof(vm.ExerciseType)+"."+vm.ExerciseType.ExerciseTypeId + "." + nameof(vm.ExerciseType.ExerciseTypeDescription));

                _uow.ExerciseTypes.Add(vm.ExerciseType);
                _uow.Commit();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Admin/ExerciseTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExerciseType exerciseType = _uow.ExerciseTypes.GetById(id);
            if (exerciseType == null)
            {
                return HttpNotFound();
            }
            var vm = new ExerciseTypeCreateEditViewModel()
            {
                ExerciseType = exerciseType,
                ExerciseTypeName = exerciseType.ExerciseTypeName.Translate(),
                Description = exerciseType.ExerciseTypeDescription.Translate()
            };
            return View(vm);
        }

        // POST: Admin/ExerciseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExerciseTypeCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.ExerciseType.ExerciseTypeName = _uow.MultiLangStrings.GetById(vm.ExerciseType.ExerciseTypeId);
                vm.ExerciseType.ExerciseTypeName.SetTranslation(vm.ExerciseTypeName, CultureHelper.GetCurrentNeutralUICulture(),
                    nameof(vm.ExerciseType) + "." + vm.ExerciseType.ExerciseTypeId + "." + nameof(vm.ExerciseType.ExerciseTypeName));
                vm.ExerciseType.ExerciseTypeDescription = _uow.MultiLangStrings.GetById(vm.ExerciseType.ExerciseTypeId);
                vm.ExerciseType.ExerciseTypeDescription.SetTranslation(vm.Description, CultureHelper.GetCurrentNeutralUICulture(),
    nameof(vm.ExerciseType) + "." + vm.ExerciseType.ExerciseTypeId + "." + nameof(vm.ExerciseType.ExerciseTypeDescription));

                _uow.ExerciseTypes.Update(vm.ExerciseType);
                _uow.Commit();
                return RedirectToAction(nameof(Index));

            }
            return View(vm);
        }

        // GET: Admin/ExerciseTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExerciseType exerciseType = _uow.ExerciseTypes.GetById(id);
            if (exerciseType == null)
            {
                return HttpNotFound();
            }
            return View(exerciseType);
        }

        // POST: Admin/ExerciseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.ExerciseTypes.Delete(id);
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
