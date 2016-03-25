using System.Data;
using System.Linq;
using System.Web.Mvc;
using _86_ValidationSummary.Models;

namespace _86_ValidationSummary.Controllers
{
    /*
      ValidationSummary() Html Helper'i ValidationExtensions Class'ının methodudur ve 8 overload'ı vardır. Kullanıldığı View'daki tüm hataların tek bir yerde gösterilmesini sağlar.
      Overload'larda kullanılan parametreler
      -> bool excludePropertyErrors: Hata mesajının ValidationSummary alanında görünüp görünmeyeceğini belirler.
      -> string message: ValidationSummary alanının başlığını belirler.
      -> IDictionary<string, object> htmlAttributes: ValidationSummary'e html özellikleri uygulama için.
     
       CSS dosyasında Validation rengini değiştirmek için 3 Property kullandık.
      .field-validation-error: Validation mesajın rengini değiştirir.
      input.input-validation-error: Hata olduğunda Textbox gibi Controller'in kenarlık rengini belirliyor.
      .validation-summary-errors: Validation ayarları yapılıyor.
      
      ValidationMessageFor() Helper methdunun 3. parametresi gösterilecek hata mesajını belirlenmesse, Exception'daki hata mesajı görünür.(hata mesajı belirleyip Css uygulamadığımızda mesaj hep görünüyor.)
       
      Not: Model'daki Age Property'sinin türü int olduğu için oluşturulan Tag'ın türü number oluyor. String değer gidiğimizde javaScript falan kullanmasak bile server a gönderme işlemi sırarında tamamen tarayıcının belirlediği bir hata alıyoruz.
      
      
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
        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}