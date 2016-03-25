using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _60_ViewStart.Models;

namespace _60_ViewStart.Controllers
{
    /*
      StartView View'ı tüm View'lardan önce çalışarak çalışan View'lara eklenir. StartView'ı Shared klasöründe oluşturup layout View'ı belirlediğimizde uygulamadaki tüm View'ları belirlenen layout View'ı kullanır. Action View'ı ile ne aynı klasörde bir StarView varsa o geçerli olur. - View'ın kendi içinde kullandığı Layout Property'si veya Action methodun View() methodu ile belirlenmiş bir layout View varsa en geçerli olanlardır.
      ViewStart View'ı tüm View'ları etkiler PartialView'ların etkilenmesini istemiyorsak Action Methodun dönüştürünü PartialViewResult yapıp Controller Class'ının PartialViewResult dönen PartialView() methodunu kullanmalıyız.
      Bir VIEW'ın STARTVIEW'dan etkilenmesini istemiyorsak VIEW'ı PARTIAL VIEW olarak oluşturacağız ve Method türünü ve dönüşnü değiştireceğiz(Details)
      Not: Controller Class'ının RedirectToRouteResult dönen RedirectToAction() methodu ile geçerli Action Methodun View'a gitmek yerine başka bir Action'ı gitmesini sağlayabiliriz.(Create)
     */

    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(db.Employees.ToList()); }
        public PartialViewResult Details(int id) { Employee e = db.Employees.Single(x => x.Id == id); return PartialView("_Employee", e); }
        public ActionResult Create() { return View("Create", "_DifferentLayout"); }
        [HttpPost]
        public ActionResult Create(Employee employee)
        { if (ModelState.IsValid) { db.Employees.AddObject(employee); db.SaveChanges(); return RedirectToAction("Index"); } return View(employee); }

        public ActionResult Edit(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null) return HttpNotFound(); return View(e); }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Attach(employee);
                db.ObjectStateManager.ChangeObjectState(employee, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public ActionResult Delete(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null)return HttpNotFound(); return View(e); }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            db.Employees.DeleteObject(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}