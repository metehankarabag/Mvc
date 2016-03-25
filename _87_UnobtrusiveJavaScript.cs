using System.Data;
using System.Linq;
using System.Web.Mvc;
using _87_UnobtrusiveJavaScript.Models;

namespace _87_UnobtrusiveJavaScript.Controllers
{
    /*
      UnobtrusiveJavaScript: Projeye eklediğimiz bir jaavaScriptFile'da javaScritp kodlarını yazıp View'larda kullanmaya denir. View dosyasının içinde yazsaydık, ObtrusiveJavaScript kullanmış olurduk.
      
      88-UnobtrusiveValidation
      ClientSideValidationEnabled ve UnobtrusiveJavaScriptEnabled Property'leri HtmlHelper Class'ının Property'leri dir. Bu Class'ı kullanarak bu ayarları Global.asax ve View'da Title alanında da belirleyebilriiz. UnobtrusiveJavaScriptEnabled açıksa, Server'da belirlediğimz Property ayarları Property'nin oluşturduğu HTML Tag'ının Data Attribute'larına eklenir. Fakat UnobtrusiveJavaScriptEnabled kapalıysa, Property'e uyguladığımız Attribute ayarları Pagesource'da oluşturulan Html Tag'ının Data Attribute'larına eklenmez. (Sayfanın en altına görünür.)
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();

        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.AddObject(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            employee.ConfirmEmail = employee.Email;//edittek otomatik yüklnmesi için
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

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

        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Single(e => e.Id == id);
            db.Employees.DeleteObject(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}