using System.Linq;
using System.Web.Mvc;
using _52_PartialViews.Models;

namespace _52_PartialViews.Controllers
{
    /*
      Partial VIEW normal VIEW'ların bir bölümünü oluşturmak için kullanılan VIEW'lardır. Yani normal bir VIEW'dan farkı yoktur. Fakat ekleneceği VIEW'ın bir bölümünü temsil ettiği için sadece o bölümün gerekliliğini karşılamak gerekir.
      Bir VIEW'a Partial VIEW' eklemek için Partial() HTML HELPER methodu kullanılır. Bu methodun 4 overload'ı var. Tüm Overload'ların ilk parametresi çalışırılacak View adı alır ve Overload'larda kullanılan Object ve ViewDataDictionary parametreleri Partial View'ın kullanacağı veri kaynağını belirler. Yani PartialView verisini her zaman eklendiği VIEW'dan alır.  Strongly Type partial view oluşturdu ise VIEW'dan Partial View'a gönderilen nesne ile partial View'da belirlenen tür aynı olmalıdır.
      Partial() methodunun Partial View'ı bulabilmesi için SHARED veya normal VIEW'ın olduğu klasörün içinde olmalıdır. Yani Oluşturulan Partial View ya sadece bir Controller'da geçerlidir yada tüm uygulamada geçerlidir.
     */
    public class HomeController : Controller
    {
        public ActionResult Index(){SampleDBContext db = new SampleDBContext();return View(db.Employees.ToList());}
        public ActionResult Details(int id) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
        public ActionResult Edit(int id) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
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