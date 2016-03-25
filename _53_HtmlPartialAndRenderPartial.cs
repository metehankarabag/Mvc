using System.Linq;
using System.Web.Mvc;
using _53_HtmlPartialAndRenderPartial.Models;

namespace _53_HtmlPartialAndRenderPartial.Controllers
{
    /*
     RENDERPARTIAL() methodu void dönen bir HTML HELPER'dir. Bu yüzden aldığı veriyi işler ve hemen sonuca yansıtır. İşlem direk sonuca eklendiği için daha hızlıdır.
     PARTIAL() method diğer HtmlHelper'lar gibi MvcHtmlString döner. Yani Partial View'dan alınan sonuç normal VIEW'da tekrar işleme sokulabilir. 
      
     Partial view'dan alınan veri işleme sokulmak isteniyorsa Partial() methodu kullanılır aksi taktirde RenderPartial() methodu kullanılır.
     RenderPartial() methodu ile Partial methodunun yazım farkları da var.
      -> Parial() Razor'da @'den sonra aspx'de : den sonra yazılır
      -> RenderPartial() Razor'da {} içinde  aspx'de : olmadan yazılır ve ikisindede ; ile biter.
     */
    public class HomeController : Controller
    {
        public ActionResult Index(){SampleDBContext db = new SampleDBContext();return View(db.Employees.ToList());}

        public ActionResult Details(int id){SampleDBContext db = new SampleDBContext();Employee e = db.Employees.Single(x => x.Id == id);return View(e);}
        public ActionResult Edit(int id){SampleDBContext db = new SampleDBContext();Employee e = db.Employees.Single(x => x.Id == id);return View(e);}
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                SampleDBContext db = new SampleDBContext();
                Employee employeeFromDB = db.Employees.Single(x => x.Id == employee.Id);
                employeeFromDB.FullName = employee.FullName; /**/ employeeFromDB.Gender = employee.Gender;
                employeeFromDB.HireDate = employee.HireDate; /**/ employeeFromDB.Salary = employee.Salary;
                employeeFromDB.PersonalWebSite = employee.PersonalWebSite; /**/ employeeFromDB.Age = employee.Age;

                db.ObjectStateManager.ChangeObjectState(employeeFromDB, System.Data.EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = employee.Id });
            }
            return View(employee);
        }
    }
}
