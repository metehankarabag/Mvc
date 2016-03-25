using System.Data;
using System.Linq;
using System.Web.Mvc;
using _62_ImplementingSearchFunctionality.Models;

namespace _62_ImplementingSearchFunctionality.Controllers
{
    /*
      Select sorgusun ile birlikte where like kullandığımızda koşula uyan tüm tablo üyleri veritabanından alınır. Index View'a kullanıcının arama değeri gönderebileceği bir textBox ve arama sütunun belirleyen 2 RadioButton kullandık. Bu Tag'ları oluşturan hepler'ları BeginFom() içinde kullanıyoruz ki Tag'lar Server'a gitsin. BeginForm() methodu çalıştırmak için Post varsayılan olarak Post Action method bekler. Fakat Server'da Post Index Action method yok. Bu yüzden Index View postalandığında hiç bir Action'ı çalıştıramaz. BeginForm() methodunun 5. overload'ı 3. parametre olarak çalıştırılacak Action methodun türünü alıyor. Burda Get Action belirleyip tüm işleri Get Action'da yapmışız.(Row State değiştirmeyeceğimiz için işlemleri için Get Request kullanmak daha iyiymiş.)
      
      1. Not: Get Action parametrelerini QueryString'le alır
      2. Not: RouteConfig Class'ındaki RegisterRoutes() methodunda birden fazla kez MapRoute() methodunu kullanabiliriz. Sadece belirlenen Url yapılarının farklı olması gerekir.
      Çalıştırılan Url içindeki bilgiler sıra ile MapRoute() methodunda karşılaştırılır ilk eşleşen çalışır.
      {} içinde belirlenen değerler url de böyle bir olmayabileceği anlamına gelir ve parantezler içinde gelen değer tarayıcıda görünmez. Parantez içine değer gelmemişse defaults propertsindeki değer kullanılır.
      Parantez kullanmadan da yapabiliriz Url'de bu değer olmak zorunda ve tarayıcıda gönünür.
      Çalıştırılan Url ilk eşleştiği ile çalışır.
      
     */

    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "Gender") return View(db.Employees.Where(x => x.Gender == search || search == null).ToList());
            else return View(db.Employees.Where(x => x.Name.StartsWith(search) || search == null).ToList());
        }
        public ActionResult Details(int id = 0) { Employee e = db.Employees.Single(x => x.ID == id); if (e == null)return HttpNotFound(); return View(e); }
        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(Employee employee)
        { if (ModelState.IsValid) { db.Employees.AddObject(employee); db.SaveChanges(); return RedirectToAction("Index"); } return View(employee); }
        public ActionResult Edit(int id = 0) { Employee e = db.Employees.Single(x => x.ID == id); if (e == null)return HttpNotFound(); return View(e); }
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
        public ActionResult Delete(int id = 0) { Employee e = db.Employees.Single(x => x.ID == id); if (e == null)return HttpNotFound(); return View(e); }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        { Employee employee = db.Employees.Single(e => e.ID == id); db.Employees.DeleteObject(employee); db.SaveChanges(); return RedirectToAction("Index"); }

        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}