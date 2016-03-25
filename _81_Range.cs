using System.Data;
using System.Linq;
using System.Web.Mvc;
using _81_Range.Models;

namespace _81_Range.Controllers
{
    /*
      Range Attribute: Değer aralığını belirler. RangeAttribute Class'ının 3 Contructor'ı var fakat 6 overload görünüyor. Görünmeyen 3 overload'a görünenlere NamedParametres eklenmiş halleri.(Daha önce böyle bişey farketmedim.). RangeAttribute Class içinde 3 Read-Only Property' var. 2'sinin türü Object diğerininki Type. NamedParametre olarak kullanılan PROPERTY'ler hata mesajı için ve bu Class'da değil. Bu Property'ler Range Attribute'unun Base Class'ı olan ValidationAttribute'un Property'leridir.
      
      RangeAttribute CLass'ının Overload'larının 2'si 2 parametreli diğeri 3 parametreli. 3 parametreli olan 1. parametre olarak Type bekliyor.  RangeAttribute Class'ının 3 parametreli olan Constructor'ının Type olarak aldığı parametre string olarak girilen değerlerin Type'ı gerçek Type'ı olmalıdır. Diğer parametreler Minimum ve Maximum değerler için fakat her overload'da farklı tür kullanılmış.(1. double 2. int 3. string)
     */
    /*DisplayFormat Attribute
      Parametresi bir overload'ı var ve hiç biri Read-Only olmayan 5 tane Property'si var. Class Constructer'ında NamedParametres olarak kullanılan tüm Property'ler bu Class'a aitdir.
      -> bool ApplyFormatInEditMode, bool ConvertEmptyStringToNull, string DataFormatString, bool HtmlEncode, string NullDisplayText. 
      
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(db.Employees.ToList()); }
        public ActionResult Details(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null) return HttpNotFound(); return View(e); }
        public ActionResult Edit(int id = 0) { Employee e = db.Employees.Single(x => x.Id == id); if (e == null) return HttpNotFound(); return View(e); }
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