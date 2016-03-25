using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;


namespace _15_UsingUpdateModelFunction.Controllers
{
    /*
      Methoda parametre olarak, herhangi bir Class'ın bir örneğini verebiliriz. ModelBinder, Class'ın Property'lerini de sayfadaki CONTROL isimleri ile eşleştirebilir. Böylece parametre olarak girdiğimiz Class örneğini doldurmuş oluruz.(FormCollection'daki gibi)Nesneyi veri tabanına kaydetmek için kullandığımız için direk kayıt işlemini gerçekleştirebiliriz.
      
      Aynı işi Action Method içinde Class örneğini kurup örneği Controller Class'ının UpdateModel<T>() Generic methodunu vererek de yapabiliriz.UpdateModel<t>() methodu form tag'larından verileri alır ve parametre olarak girdiğimiz örneğe aktarır.(Önerilen kullanım.)
      
      1. Not: Bu methoda Get Request ile gelen verileri(postalanmış veriler, QueryStirng'ler, Cookies) parametre olarak aldığı Class örneğini doldurmak için kullanabilir. Demişim ama video da sadece denetler yazıyor ve QueryString ile değer göndermeyi denedim olmadı. Url'den değer alınmıyor. Employee nesnesinin parametreye eklediğimde de almadı.
      
      Son durumda Get ve Post Create Action method'lar aynı olduğu için hata alırız. Bu yüzden Method adlarını değiştirmeliyiz. Fakat hiç bir ayar belirlememize rağmen Create View'daki form Tag'ı Html'de Creat Action methodunu çalıştırmaya ayarlanıyor.(Şimdi farkettim) Bu yüzden Form doldurulup Server'a postalandığında method adını değiştirdiğimiz için hiç bir Action çalışmıyor. Ayrıca method çalıştırılsa bile View() methodunca çalıştırılacak View() belirlenmediği için method adına bir View aranacak ve bulunamayacak. ActionName Attribute uygulandığı Action methodun çalışma zamanında adını değiştirir. Bu durum 2 sorunuda çözer.
      
      2. Not: Sanırım Url'den değer alınmamasının nedeni Form Tag'ındaki Url ile Server'a geliniyor olması.
      3. Not: RedirectToAction() methodu bizi bir Action methoduna yönlendirir. URL'i değiştirir ve işini gittiği Action'da bitirir.
      
      Arasıra - Adding the specified count the semaphore would cause it to exceed its maximum count hatası alabiliriz. 
      Bunu çözmek için -- IIS'i yeniden başlatmak veya web.config dosyasındaki ConnectionString'de connection pooling'i kapatmak gerekir.
    */
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();
            return View(employees);
        }
        [HttpGet] [ActionName("Create")] public ActionResult Create_Get() { return View(); }

        [HttpPost] [ActionName("Create")]
        public ActionResult Create_Post() 
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();

                Employee employee = new Employee();
                UpdateModel<Employee>(employee);

                employeeBusinessLayer.AddEmmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}