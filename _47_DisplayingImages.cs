using System.Linq;
using System.Web.Mvc;
using _47_DisplayingImages.Models;

namespace _47_DisplayingImages.Controllers
{
    /*Bu derste tarayıcıda IMAGE göstermek için details VIEW'da IMG TAG'ını kullandık. Gelecek derste img Tag'ı oluşturan Html Helper'i oluşturacağız.
      Not: Url.Content() methodu parametre olarak RELATIVE PATH alır FULL PATH döner. IMAGE kontrolün src özelliği Fullpart ile çalıştığı için bu methodu veriyoruz. Parametre olarak RELATIVE PATH vermemizin nedeni ise SERVER'de uygulamanın nerede tutulduğunu bilmememizdir. Relative Path kullanarak IMAGE'in uygulama içindeki yerini veriyoruz. Kullandığımız method ise uygulamanın Server'deki yolunu veriyor ve tam yol oluşturuluyor. Bu sayede resimi işleme sokabiliyoruz.
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult Details(int id)
        { SampleDBContext db = new SampleDBContext(); Employee employee = db.Employees.Single(x => x.Id == id); return View(employee); }
        public ActionResult Edit(int id) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
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
    }
}
