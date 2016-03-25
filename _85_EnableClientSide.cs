using System.Data;
using System.Linq;
using System.Web.Mvc;
using _85_EnableClientSide.Models;

namespace _85_EnableClientSide.Controllers
{
    /*Client Side Validation kullanabilmesi için Web.Config'de <appsetting> -> <add key="ClientValidationEnabled" value="true" /> <add key="UnobtrusiveJavaScriptEnabled" value="true" /> Property'lerine izin vermemiz gerekiyor. Client'de Validation'ı gerçekleştirecek kodları içeren kütüphaneleri View'a vermek gerekli.
      Client'da çalışacak Validation ayarları Model Property'lerine uyguladığımız Attirbute'lara göre belirlenir. Bu yüzden javascript kodu yazmaya gerek yok.
     
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
            employee.ConfirmEmail = employee.Email;//edittek otomatik yüklnmesi için
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