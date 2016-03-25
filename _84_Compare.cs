using System.Data;
using System.Linq;
using System.Web.Mvc;
using _84_Compare.Models;

namespace _84_Compare.Controllers
{
    /*
     CompareAttirbute Class'ının parametre olarak string otherProperty bekleyen bir Constructor'u var. Class ValidationAttirbute Class'ından türemiş bir Class ve NamedParametre olarak bu Class'ın Parametrelerini kullanıyor. Kendi içinde string dönen 2 property'si var biri Read-Only, diğeri Internal set içeren bir Property(ilk defa gördüm.)
     
     Derste kullanıcın Email COLUMN'una yanlış değer girmesini önlemek için Email'ını 2. kez yazdıracağız ve değerleri karşılaştıracağız. Fakat Model'da 2. Email değerini tutabilecek bir Property' yok. Bu yüzden Model'a eklediğimz Partial Class'da bir Property oluşturup Attribute'u uyguluyoruz. Attribute'a parametre olarak karşılaştırmanın yapılacağı değerin geldiği Property'nin adını veriyoruz.
     1. Not: Edit işlemin sayfa tarayıcıya gösterilirken oluşturduğumuz Property'nin dolu olmasını istiyorsak Get Edit Action Method'da veri tabanından Email değerini alıp oluşturduğumuz Property'e atmalıyız.
     2. Not: Compare Attribute, System.Web.Mvc Namespace içinde diğerleri System.Componentmodel.Dataannotations içinde 
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(db.Employees.ToList()); }
        public ActionResult Details(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null) return HttpNotFound(); return View(e); }
        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            employee.ConfirmEmail = employee.Email;

            if (employee == null) return HttpNotFound(); return View(employee);
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

        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}