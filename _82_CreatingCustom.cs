using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _82_CreatingCustom.Models;

namespace _82_CreatingCustom.Controllers
{
    /*
      Range ATTRIBUTE için parametre olaraak DATETIME.NOW kullanamayız.
      Bir CLASS oluşturup RANGEATTRIBUTE CLASS'ında türeteceğiz. 
      RANGEATTRIBUTE System.ComponentModel.DataAnnotations NAMESPACE'sindedir.
      Oluşturduğumuz CLASS'ın BASE CLASS'daki bir CONSTRUCTER'i çalıştırması için BASE KEY WORD'u kullanıyoruz.
      DATETIME.NOW() methodunu burada 3. parametre olarak veriyoruz çalışıyor.
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();

        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.AddObject(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

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

        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            db.Employees.DeleteObject(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}