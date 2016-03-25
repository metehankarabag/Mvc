using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using _64_ImplementSorting.Models;
using PagedList;

namespace _64_ImplementSorting.Controllers
{
    /*
      Sıralama işlemi derste üyelerin yeni bir düzen ile veritabanında çekilmesini gerektiriyor. Bu yüzden sıralama düzeni değiştiğinde sayfayı yeni düzenin değerini belirleyen bir link ile Server'a göndermeliyiz. Link'in çalıştırdığı Action Method'da bu değer alınır veritabanından yeni düzene veri çekilir ve sonuç View'a gönderilir.
      Sonuç taracıyıya yansıtıldığında sıralamayı eski haline getirebilmemiz için tekrar link'e tıklamalıyız. Fakat değer aynı olduğu için sıralama değişmeyecek. Bu yüzden Action Link çalıştığında Server'a gelen değerin tam tersi View'daki ActionLink'e eklenmelidir. Böylece tarayıcıda oluşturulan link'in değeri değişir.
     INDEX ACTION method ilk çalıştığında, sortBy değeri Empty olur. Sayfa ilk çalıştığında varsayılan olarak NAME ve normal sıralanması gerekir. Bu yüzden sortBy'ın Empty değeri için Action Link'e NAME ters sıralama değeri verilmelidir. Çünkü bir sonraki görüntü ters olacak. Action Link'e göndereceğimiz Değer Model'in bir parçası olmadığı için değeri ViewBag/Data Property'si ile View'a gönderebilriz. sortBy ile gelen değere göre şimdiki View'ı hazırlayacağımız için sortBy değerini Switch'e alıp sıralama işini yapan OrderBy() Extension methodlarından birini çalıştırdıktan sonra veriyi View'a gönderiyoruz.
     
     Where() Extensin methodu Extension parametresi olarak IQueryable<TSource> türünde bir nesne örneği istediği için ObjectSet<Employee> örneğine Queryable Class'ının Extension methodu olan AsQueryable() methodunu uygulamalıyız.
     
          Not: QueryString'ler sayfayı Server'a postalayan link'ler ile birlikte gelir. Bu link'lerin QuerySting değerleri ActionLink'lerin object routeValue parametrelerindeki değerlere ile oluşturulur. (ActionLink Server'dan aldığı bir değeri tarayıcıya gönderiyorsa QueryString oluşturur. Sanırım RouteValue alan tüm Helper'lar için bu böyle.) Yani Bir ActionLink çalıştırdığı Action Methodun kullandığı tüm parametrelere QueryString'den değer gönderemiyorsa, daha önce o parametrede olan değer Null olur ve yeni oluşturulacak View'daki ActionLink oluşturacağı link'e eski değeri aktaramaz. Bu yüzden oluşturulacak her ActionLink View'ı çalıştıran Action Methodun tüm parametreleri için QueryString oluşturmalıdır.
    */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index(string searchBy, string search, int? page, string sortBy)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.GenderSort = sortBy == "Gender" ? "Gender desc" : "Gender";

            var employees = db.Employees.AsQueryable();
            if (searchBy == "Gender") employees = employees.Where(x => x.Gender == search || search == null);
            else employees = employees.Where(x => x.Name.StartsWith(search) || search == null);

            switch (sortBy)
            {
                case "Name desc":
                    employees = employees.OrderByDescending(x => x.Name);
                    break;
                case "Gender desc":
                    employees = employees.OrderByDescending(x => x.Gender);
                    break;
                case "Gender":
                    employees = employees.OrderBy(x => x.Gender);
                    break;
                default:
                    employees = employees.OrderBy(x => x.Name);
                    break;
            }

            return View(employees.ToPagedList(page ?? 1, 3));
        }

        public ActionResult Details(int id = 0) { Employee e = db.Employees.Single(x => x.ID == id); if (e == null) return HttpNotFound(); return View(e); }
        public ActionResult Create() { return View(); }

        [HttpPost]
        public ActionResult Create(Employee employee)
        { if (ModelState.IsValid) { db.Employees.AddObject(employee); db.SaveChanges(); return RedirectToAction("Index"); } return View(employee); }
        public ActionResult Edit(int id = 0) { Employee e = db.Employees.Single(x => x.ID == id); if (e == null) return HttpNotFound(); return View(e); }
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