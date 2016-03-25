using System.Linq;
using System.Web.Mvc;
using _50_DetectErrorsInViewsAtCompileTime.Models;

namespace _50_DetectErrorsInViewsAtCompileTime.Controllers
{
     /*
     STRONGLY TYPED VIEW kullandığımızda, MODEL'daki tüm PROPERTY'lere INTELICENSE kullanarak ulaşabiliriz. PROPERTY adını değiştirirsek kırmızı çizgi ile yanlış olduğu belirtilir. 
     Fakat PROJE'i BUILD yaptığımızda hata almayız. Çünkü MVC varsayılan olarak VIEW'ları BUILD etmez. PROJE dosyasını (csproj uzantılıdır) notPad'de açıp "MvcBuildViews" TAG'ını bulup TRUE değeri verirsek, artık VIEW'lar da BUILD edilir. Bu sadece hata olduğunda düzenleme zamanında alırız.
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult Details(int id) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
        public ActionResult Edit(int id){SampleDBContext db = new SampleDBContext();Employee e = db.Employees.Single(x => x.Id == id);return View(e); }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                SampleDBContext db = new SampleDBContext();
                Employee employeeFromDB = db.Employees.Single(x => x.Id == employee.Id);
                employeeFromDB.FullName = employee.FullName; /**/ employeeFromDB.Gender = employee.Gender;
                employeeFromDB.HireDate = employee.HireDate; /**/ employeeFromDB.Salary = employee.Salary;
                employeeFromDB.Age = employee.Age; /**/ employeeFromDB.PersonalWebSite = employee.PersonalWebSite;

                db.ObjectStateManager.ChangeObjectState(employeeFromDB, System.Data.EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = employee.Id });
            }
            return View(employee);
        }
    }
}
