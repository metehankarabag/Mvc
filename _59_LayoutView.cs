using System.Data;
using System.Linq;
using System.Web.Mvc;
using _59_LayoutView.Models;

namespace _59_LayoutView.Controllers
{
       /*
      Layout View Action Methodların çalıştırdığı VIEW'ları içinde bir yerde gösteren HTML sayfasıdır. Yani Partial VIEW'lar Aciton methodların çalıştırdığı VIEW'ların içinde eklenen VIEW'larken Layout View'lar bu View'ları içinde alan View'lardır. -> Layout > Action > Partial 
       Action View'da WebPageBase Abstract Class'ının Layout Property'ine parametre olara kullanılacak Layout View'ın sanal yolunu verdiğimizde View belirlenen Layout View'ı kullanır. Layout Property'si ile belirlenen Layout View sadece kullanılan View için geleçerlidir. Bu yüzden her sayfada kullanmak gerekir. Bu sorunu ViewStart View'ı kullanarak çözeceğiz.
      
      Layout View'da WebPageBase Abstract Class'ının HelperResult dönen RanderBody() methodu Layout View'ı kullanan View'ın Layout View'da ekleneceği yeri belirler.
      Not: Action method'un kullandığımı View() methodunun 7. overload'ındanın 2. parametresi masterView'ı alır bu da Layout View'ı belirler.
      Not: Layout Property WebPageRenderingBase Abstract Class'ının absract Property'sidir override ile WebPageBase'e eklenmiştir.
     
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(db.Employees.ToList()); }
        public ActionResult Details(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null) return HttpNotFound(); return View(e); }

        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(Employee employee)
        { if (ModelState.IsValid) { db.Employees.AddObject(employee); db.SaveChanges(); return RedirectToAction("Index"); } return View(employee); }

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

        public ActionResult Delete(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null) return HttpNotFound(); return View(e); }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        { Employee employee = db.Employees.Single(e => e.Id == id); db.Employees.DeleteObject(employee); db.SaveChanges(); return RedirectToAction("Index"); }

        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}