using System.Linq;
using System.Web.Mvc;
using _45_CustomizeDisplayAndEditTemplates.Models;

namespace _45_CustomizeDisplayAndEditTemplates.Controllers
{
    /*
     View klasörü içinde EditorTemplates veya DisplayTemplates klasörleri oluşturup bu klasör içine VIEW'lar ekleyerek Diplay() ve Editor() Templated Html Helper methodlarının veriyi tarayıcıya yansıtmak için kullandığı Tag'ı değiştirebilir veya son ayarlarını yapabiliriz. Klasör içinde oluşturulacak View adları bir tür adı olmalıdır. Çünkü Templated Helper methodları kullanacağı View'ı parametre olarak aldığı Property'nin türüne göre arar.(Methodların TemplateName parametrelerini kullanarak sanırım kullanılacak View'ı değiştirebiliriz.) Strongly Type olsun istiyorsak Model olarak da türün Class'ını belirliyoruz.
     Derste Tag'a bir Html class değeri atanıp javascript ile oluşturulan Html elemanti bulunduktan sonra mouse ile her tıklandığında Jquery calendar kontrolü gösterilmiştir.
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult Details(int id)
        { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); ViewData["EmployeeData"] = e; return View(); }

        public ActionResult Edit(int id = 1) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
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
