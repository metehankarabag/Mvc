using System.Linq;
using System.Web.Mvc;
using _44_DisplayAndEditTemplatedHelpers.Models;

namespace _44_DisplayAndEditTemplatedHelpers.Controllers
{
    /*Display()/DisplayFor()/DisplayForModel(): Veriyi tarayıcıda görüntüleyecek Html arayüzünü oluşturan DisplayExtensions Static Class'ının Extension Method'lardır.
      Editor()/EditorFor()/EditorForModel(): Tarayıcıdan Server'a veri alınabilecek arayüzü oluşturan EditorExtensions Static Class'ının Extension Method'lardır.
      Not: DiplayFor<T,TResult>() ve EditorFor<T,TResult>() Class'larının Generic methodlarıdır.
      
      Edit/Display + for + Model() methodlarının kullanım farkları.
      Bu methodların MvcHtmlString dönen 6 overload'ı vardır ve hepsi 1. parametre olarak HtmlHelper alır.
      2. parametre olarak görüntüsü hazırlanacak Property istenir.
      -> diğer object AdditionalViewData, teplateName(sanırım property'i gösterecek Tag'ı belirlediğimiz template adını alıyor.), htmlFieldName
       
      1. Not: Display() ve Editor() methodlarını kullanıyorsak parametre olarak ViewBag Property adını veya ViewData Key'ini string olarak yazarken For()'lu olanları kullanıyorsak bir Delegate aracılığı ile bir Model Property'sini veririz. ForModel() methodlarında 2. parametre olarak Property açıklaması istenmez çünkü Model olarak belirlenmiş Class'ın tüm Property'lerini otomatik olarak kullanır.
       2. Not: ViewBag/Data'ya değer olarak bir Class'ın Property verilirse bile, oluşturulacak Html Tag'ı Property'e uygulanan Attribute'dan etkilenmez. Property DisplayFor() içinde kullanılırsa methoduna oluşturulacak Tag Attribute'dan etkilenir.
       3. Not: ViewBag/Data'ya Model Class'ının örneği verilirse forModel() ile aynı sonucu verir. Bu kullanımda tarayıcıya Property adı ve verisi ayrı ayrı yansıtılır.
       4. Not: Html Property'si WebViewPage<T> Generic Class'ının Generic HtmlHelper türündeki Property'si olduğu için bu Property'den methodlara ulaşabiliyoruz
    */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult Details(int id){SampleDBContext db = new SampleDBContext();Employee e = db.Employees.Single(x => x.Id == id);
            //return View(employee);
            ViewData["EmployeeData"] = e;
            return View();
        }
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
