using System.Data;
using System.Linq;
using System.Web.Mvc;
using _80_StringLength.Models;

namespace _80_StringLength.Controllers
{
     /*
      [StringLengthAttribute]: System.ComponentModel.DataAnnotations içindedir. Field/Property'lerin kaç karakterlik veri tutabileceğini belirler. Bu attribute uygulandığı Property'e değer girilme zorunluluğu kazanırmaz girilecek değerin karakter aralığını belirler. Değer girme zorunluğu için Required Validation gereklidir.
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
        protected override void Dispose(bool disposing){db.Dispose();base.Dispose(disposing);}
    }
}