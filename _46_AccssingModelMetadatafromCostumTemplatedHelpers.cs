using System.Linq;
using System.Web.Mvc;
using _46_AccssingModelMetadatafromCostumTemplatedHelpers.Models;

namespace _46_AccssingModelMetadatafromCostumTemplatedHelpers.Controllers
{
    /*
      Geçen dersde EditorTemplates'deki View'a gelen Property'nin tarihin görünümünü Hard Code ile belirlemiştik. Bu durumda türü aynı olan tüm Property'lerin aynı ayar uygulanır. Templated Helper methodları parametre olarak aldığı Property'i Model olarak kullandığı template View'a gönderir. Bu yüzden Model Class'ında belirlediğim ayarları dinamik olarak teplate View'da kullanabiliriz. Böylece her Property kendi ayarını kullanır. Biz sadece varsayılan ayarı belirleriz.
     
     WebViewPage<TModel> Abstract Class'ının ViewData PROPERTY'sinin türü ViewDataDictionary<TModel> dır. Bu Class'ın ViewDataDictionary CLASS'ında türediğinden dolayı base Class'ın Property'lerine ViewData Property'sinden ulaşabiliriz. Base Class'ın üyesi olan TemplateInfo türündeki TemplateInfo PROPERTY'sini kullanarak TemplateInfo Class'ı içindeki Property'lere ulaşıp, Templated Helperler methodlarına parametre olarak verilen Property'nin Metadata'sına ulaşabiliriz. Ve ayrıca FormattedModelValue ve HtmlFieldPrefix değerlerini değiştirebiliriz.
      
      1. Not: DisplayFormat ATTRIBUTE'unda verilen değer varsayılan olarak Edit Mode'da geçerli olmadığı için beklediğimiz sonucu alamayız. Bu sorunu düzeltmek için Model CLASS'ında DisplayFormat Attribute'unun ApplyFormatInEditMode PROPERTY'sine TRUE değeri vermek gerekir.
      2. Not: CONTROLER'de kullandığımız VIEWDATA PROPERTY'si ViewDataDictionary CLASS'ın(yani base CLASS'ın) kendisine aittir.
      3. Not: kullandığımız tarih formatı CONGIF'de system.web içinde globalization culture'da belirlenmiş olana uymuyorsa validation hata algılar.
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
