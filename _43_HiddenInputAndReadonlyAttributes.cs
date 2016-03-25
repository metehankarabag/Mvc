using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _43_HiddenInputAndReadonlyAttributes.Models;

namespace _43_HiddenInputAndReadonlyAttributes.Controllers
{
    /*
     HiddenInput Attribute: Uygulandığı Property'i türü hidden olan bir input nesnesi kullanarak tarayıca gönderir. DisplayForModel() methodun bu Property'i kullanmaz.
     ReadOnly Attribute: Property değerinin değiştirilmesini engeller. Değeri View'da değiştirebilir. Fakat Model Binder Property'den gelen değeri kullanmaz.
     Değer SERVER'e her zaman null olarak gider. Fakar isValid() methodu false dönmez. Aynı işi SET'i silerek'de yapabiliriz.
     
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult Details(int id)
        { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }

        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(FormCollection collection) { try { return RedirectToAction("Index"); } catch { return View(); } }

        public ActionResult Edit(int id)
        { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                SampleDBContext db = new SampleDBContext();
				Employee employeeFromDB = db.Employees.Single(x => x.Id == employee.Id);
                employeeFromDB.FullName = employee.FullName; /**/ employeeFromDB.Gender = employee.Gender;
                employeeFromDB.HireDate = employee.HireDate; /**/ employeeFromDB.Age = employee.Age;
                employeeFromDB.PersonalWebSite = employee.PersonalWebSite; /**/ employeeFromDB.Salary = employee.Salary;

                db.ObjectStateManager.ChangeObjectState(employeeFromDB, System.Data.EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = employee.Id });
            }
            return View(employee);
        }

        public ActionResult Delete(int id) { return View(); }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) { try { return RedirectToAction("Index"); } catch { return View(); } }
    }
}
