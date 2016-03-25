using System.Data;
using System.Linq;
using System.Web.Mvc;
using _83_RegularExpression.Models;

namespace _83_RegularExpression.Controllers
{
    /*
      RegularExpression: belirli karakterlerin veri olarak girilmesini engellemk için kullanılır.
      http://gskinner.com/RegExr/ > sağ üstte community > burdaki kutucuktan araştırma yap(first name araştırması)> en çok kullanılanlar aşağıda listelenir.
      Çift tıkladığımızda sorgu sol üstteki usun textbox'a yazılır. Alttaki alanın yazısını silip, sorguyu test edebiliriz. Doğru değerler vurgulanır.
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();

        public ActionResult Index() { return View(db.Employees.ToList()); }

        public ActionResult Details(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null)return HttpNotFound(); return View(e); }
        public ActionResult Edit(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null)return HttpNotFound(); return View(e); }

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