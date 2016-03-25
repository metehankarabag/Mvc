using System.Data;
using System.Linq;
using System.Web.Mvc;  
using _63_ImplementPaging.Models;
using PagedList;
using PagedList.Mvc;

namespace _63_ImplementPaging.Controllers
{
   /*
      Manage NuGet Package'den PageList.Mvc'yi indir. ToPageList() Extension methodu PagedListExtensions Class'ının üyesidir ve 3 parametre bekleyen 2 overload'ı vardır. İki overload'da da 2. ve 3. parametreler aynı. 2. parametre gösterilecek sayfa numarasını 3. parametre sayfa genişliğini belirler. 1. overload'da Extension parametre olarak Enumerable<t> 2. overload'da Extension parametre olarak Queryable<T> alır. Yani listelere uygulanır. Sayfa genişliğine ve sayfa numarasına göre uygulandığı bazı liste üyelerini alır ve View'a gönderdir. Yani Model'da listenin tamamı olmaz. Tüm sayfaları görüntüleyebilmek için sayfa numarasını değiştirip Server'a giderek View'daki veriyi güncellemeliyiz. 
      1. Not: Html Helper'larda Lambda uyguladığımızda normalde IPagedList<T>'in alt türünü almamız gerekir fakat yine alt tür olarak IPagedList<T> alıyoruz. 
      2. Not: Var değişkeni direk Alt tür olan Employe'i temsil ediyor.
    
      PageListPager() methodu PagedList.Mvc namespace'i içindeki HtmlHelper Class'ının Helper methodudur. Bu method tarayıcıda her üyesinde bir <a href=""> tagı olan bir liste oluşturur. Bu a Tag'larının her biri bir sayfa numarası ile Server'a gider ve Gönderilen değeri ToPageList() methodunda kullanarak gösterilecek sayvayı belirleriz.
     PageListPager() methodunun 3 ve 4 parametreli 2 overload'ı var. 1. parametre Extension type
        2. parametre olarak IPagedList nesnesi istiyor Model'i veriyoruz.
        3. parametre olarak int parametre alan ve string dönen bir method bekliyor. Dönen String oluşturulacak listedeki a Tag'ının linkini belirlemek için kullanılacak. 
            Url WebViewPage Class'ının UrlHelper türündeki Property'sidir. Bu Class'ın üyesi olan Action() methodu link oluşturmak için kullanılır. 8 overload'ı vardır. 2. overload'ının 1. parametresi Url'in çalıştıracağı Action Methodu 2. parametresi Action Methodun istediği parametreyi alır. Parametre olarak int değeri veriyoruz. Bu int değeri ToPagedList() methodunda kullanarak istenilen sayfayı alıyoruz. Çalıştırdığımız Action method 2 parametre daha bekliyor. Bu parametrelere değer göndermessek, sayfa oluşturma kriterlerini kaybedeceğimiz için doğru sonucu alamayız.
      Get Action'lara gönderilen parametreler QueryString'lerde tutulduğu için Action() methodun 2. parametresinde bu QuerySting'leri kullanarak bir önceki arama değerlerini bir sonrakini çalıştıracak link'lere ekleyebiliriz.
      
        3. parametre olarak PagedListRenderOption TYPE'ında bir nesne bekliyor. Bu Class'ı kullanarak oluşturulacak çalışma ayarlarını belirliyoruz.
        Bu Class'ın Display Property'si tek bir sayfa varsa kontrol'ün görünüp gönümeyeceğini bekirlemek için PagedListDisplayMode Enumundan bir değer alır.
                    DisplayItemSliceAndTotal Property'si sayafa gösterilen üyelerin listedeki sıraları ve toplam üye sayısını gösterir boolen değer alır.
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (searchBy == "Gender") return View(db.Employees.Where(x => x.Gender == search || search == null).ToList().ToPagedList(page ?? 1, 3));
            else                      return View(db.Employees.Where(x => x.Name.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult Details(int id = 0) { Employee e = db.Employees.Single(x => x.ID == id); if (e == null) return HttpNotFound(); return View(e); }
        public ActionResult Create() { return View(); }
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
        {
            Employee employee = db.Employees.Single(e => e.ID == id);
            db.Employees.DeleteObject(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}