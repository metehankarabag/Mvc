using System.Data;
using System.Linq;
using System.Web.Mvc;
using _61_NamedSectionsInLayoutFiles.Models;

namespace _61_NamedSectionsInLayoutFiles.Controllers
{
    /*NAMED SECTION
     Layour View'lar Action view'ları RenderBody() methodu ile tek parça halinde gösterir. Bir Action View'da Section oluşturduğumuzda, Layout View'da kullandığımız RenderBody() methodu Section'ı yazdırmaz. Yani Section'daki Html'i Layout View'da farklı konumlandırabiliriz. Yani bir View'da Section Section varsa View'ın 2 parçadan oluşur.(Section'lar ve diğerleri). 
     Action View'da Section alanı oluşturmak için -> @Section SectionName{} şeklinde VIEW'larda belirlenir.
     Layout View'a Section alanını eklemek için -> @RenderSection("SectionName")  methodunu kullanırız.
     RenderSection() methodu WebPageBase Absract Class'ının overload'ı olan bir methoddur. Birinci parametre olarak aldığı Section'ı View'da bulamassa çalışma zamanında hata verir. -> Section no defined: "SectionName". ikinci parametre Section'ın gerekliliğini belirler. False verdiğimizde Action View'da Section yoksa hata vermez fakat farklı isimde de olsa bir Section varsa hata verir. Bu sorunu çözmek WebPageBase Absract Class'ının boolen dönen isSectionDefined() methodunu kullanıyoruz. Parametre olarak Section adını alır. if kontrolü ile section'ı kontrol edebiliriz.
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(db.Employees.ToList()); }
        public ActionResult Details(int id) { Employee e = db.Employees.Single(x => x.Id == id); return View(e); }

        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(Employee employee)
        { if (ModelState.IsValid) { db.Employees.AddObject(employee); db.SaveChanges(); return RedirectToAction("Index"); } return View(employee); }
        public ActionResult Edit(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null) { return HttpNotFound(); } return View(e); }

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

        public ActionResult Delete(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null) { return HttpNotFound(); } return View(e); }

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