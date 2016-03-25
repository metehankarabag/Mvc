using System.Data;
using System.Linq;
using System.Web.Mvc;
using _25_QueriesUsingEntityFramework.Models;
//Manage nuget pageces tan spark view engine indirdik
//C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\ItemTemplates\CSharp\Web\MVC 4\CodeTemplates\AddView  
//Bu yol içinde Spark klasörü oluşturduk
//Klasörün içine bir xml dosyası ekledik ve razor veya aspx deki gibi delete create list gibi template leri ekleyeceğiz.
namespace _25_QueriesUsingEntityFramework.Controllers
{
    /*25.ders
      EntityFrameWork kullanarak veritabanı işlemlerini gerçekleştireceğiz. Ctrl+Shift+A > Data > Ado.net Entity Data Model FrameWork'ü Model Class'larını otomatik olarak oluşturmamızı sağlar. 
      İsim verip onayladıktan sonra ilk pencerede Model olacak Class'ların neyi temel alarak oluşturulacağını belirlemek gerekiyor. Generate form DataBase: Bir veritabanını göre Model Class'larını oluşturur. Bunu şeçtikten sonra açılan 
      1. pencerede kullanılacak SERVER(veritabanının olduğu bilgisayar., ağ...)'ın adı, bağlantı türü ve kullanılacak Database seçilir. 
      2. pencerede Web.Config dosyasında belirtilen ayarlara göre oluşturulacak COnnectionString'in adı belirlenir. 
      3. Pencerede veritabanına bağlanılır ve tüm içeriği ekrana yansıtılır. Model'e eklemeyi istediğimiz, tabloları,Stored Procedure'leri vs.. seçip, oluşturulacak Model Class'ların NAMESPACE'ini belirliyoruz ve bitiriyoruz.
      
      Oluşturulan Model Class'larına göre hiç kod yazmadan Controller Class'ını ve View'ları oluşturmayı istiyoruz.
      Bunun için Controller Class'ını ekleme penceresindeki Templates sekmesinden Contollerin ve Action method'ların Model'a göre otomatik oluşturulmasını sağlayabiliriz.
     */
    /*26. ders
      Otomatik oluşturulan Class'lar içinde yapılan değişiklikler Class'lar yenilendiğinde kaybolur. Bu yüzden Model Class'ları üzerinde değişikilk yapmayı istediğimizde EntityFramWork'ün oluşturduğu Class'ları kullanamayız. EntityFrameWork Model Class'larını Partial olarak oluşturduğu için Model'ın bir parçasını başka bir Class dosyasını kullanarak oluşturabiliriz. Entity Class'ına ekleyeceğimiz parçanın Entity Class'ının NAMESPACE'i ile Class adının aynı olması gerekir ve bir Partial Class'da olan üyenin aynısını başka bir Partial Class'da oluşturamayız. Bu yüzden ya üye üzerinde çalışan yeni bir üye oluşturmak yada oluşturduğumuz Partial Class'a MetaData Attribute'unu uygulayarak MetaData Class'ını belirlemeliyiz. Attribute parametre olarak MetaData olacak Class'ın TPYE'ını alır. MetaData Class'ı içinde yapacağımız iş Partial Class'larda bulunan üyelerin aynısını ekleyip, bu üyelere üylere Attribute'lar uygulayarak çalışma zamanındaki işleyişlerine etki etmek.
      [Display(Name = "Department Name")] : Diplay Attribute'u uylandığı üyenin tüm görünüm/ayarlarını değiştirir. Sadece Name parametresine değer vermişiz. DiplayName Attribute'u da var. (Department Class'ında kullanıldı.)
      /*27. ders
      Bu derste otomatik oluşturulan View'lar(Create View'da) üzerinde değişiklik yaptık(DropDownList kullanarak). 2 Taane DropDownList kullandık. 1. Gender için option'larını hard kod ile View'da yazdığımız. 2. Department için kullandığımız. Department için kullandığımız DropDownList'de 2 parametre ver 1. oluşturulacak Tag'ın Adı 2. DropDownList'de görünecek varsayılan yazı. Normalde nesnesi olmadığı için hata vermesi gerekir fakat Create Actionda 1. parametrede belirlediğimz string i kulanarak ViewBag dinamik Property'si oluşturduk ve değer olarak SelectList nesnesi verdik. Bu durumda DropDownList View'dan değerleri alabiliyor. SelectList Class'ının overload'larında birinci parametre olarak liste alınıyor bu listenin belirlenen Property değerleri sanırım base class'ındaki Property'ler kullanılarak 8liste olarak dönderiliyor.
      Ayrıca Employee partial Class'ının tüm Property'lerine Required Attribute'u uyguladık.
     */
    /*28.Ders
      Edit View'da da değişiklikler yapıyoruz. Gender değerini DropDownList'den alamak için DropDownList kullandık. Name değerinin değiştirilmemesi için DisplayFor kullanıp, HiddenField ile Server'a gönderiyoruz.Fidler ile değişitilebileceği için Action method'da değişiklikler yapmamız gerekir.Veri tabanını güncelleme işini EntityFrameWOrk methodunu kullanarak güncelleme yapıyoruz. Bu method veri tabanında aldığı satırın tüm sütunlarını günceller. Bind Attribute'u ile güncellenmesini istemediğimiz bir Property belirlediğimizde değeri Server'a Null olarak gelir fakat EntityFrameWork'un oluşturduğu method Null değerini sütunu güncellemek için kullanır. (UpdateModel() kullansaydık güncelleme işleminden sütun çıkarılırdı.) Sütunun Null olarak güncellenmesini engellemek için veri tabanından aynı satırı alıp bir Model örneği oluşturmamız gerekiyor. Bu örneğe Action method parametresinden adlığımız yeni değerleri verip FrameWork Methodlarına atıyoruz. Oluşturduğumuz Model örneğinde Name Property'sinin değeri dolu olduğu için güncelleme işlemine eklemiyoruz. Bu durumda Bind Attribute'unu kullanmaya gerekte kalmıyor. Fakat silmediğimiz için ve Name değeri Null geldiği için isim tarayıcıda gösterilmiyor ve ModelState hata veriyor. Required Attribute'unu kaldırarak bu sorunu çözdük. Fakat Name zorunlu olmadığı için yeni katıy oluştururken girme zorunluluğumuz olmuyor. Bu yüzden dinamik ModelState'e dinamik olarak Hata eklemek zorundayız. Controller Class'ının ModelState Property'sine AddModelError() methodunu uyguluyoruz. Methodun 2 overload'ı var. 1. hataye düşürülecek key, 2. mesaj veya Exteption.
     */
    /*29. Ders -> Using Data transfer objects as a Model
      Model olarak kullandığımız 2 tablo var. Linqu kullanarak bu 2 tabloyu birleştirip gruplamışız. Gruplanmış sonuç'ları kullanarak bir tür oluşturup View'a göndermeyi istiyoruz. Yani Model içinden aldığımız değerleri farklı bir Model olarak kullan için oluşturduğumuz Class'lar Data Transfer Object'leridir.
      Not: Include() methodu uygulandığı tabloya ekelenecek(Join) tablo adını parametre olarak alıyor.(yolunu alıyomuş-> namespace)
     */

    /*31. Ders
      30 derste view engine'in ne olduğunu anlatmış.
      Manage nuget pageces tan spark view engine indirdik. BU view engine'i view eklerken kullanabilmek için Template'ini belirlememiz gerekir. Tüm template'lerin belirlendiği dosyanın adı "C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\ItemTemplates\CSharp\Web\MVC 4\CodeTemplates\AddView"
      Bu yol içinde Spark klasörü oluşturup, klasörün içine bir xml dosyası ekledikten sonra View'da görünüm ayarlarını uzantı ayarlarını belirten Xml verilerini gireceğiz. Bu işi yaptıktan sonra Vıew ekleme penceresinde görünür. razor veya aspx deki gibi delete create list gibi template'lerinin olmasını istiyorsa bu dosyalarıde eklemeliyiz. ekleyeceğiz.
     */
    /*32. Ders
      Contoller Action method'lar View'ı nasıl bulur.
      Views Klasörü varsayılan klasör. Bu klasör içinde ilk önce Controller Adında bir klasör sonra Shared adına bir klasor aranır. Aradan Controller Adı ile aynı adı almış klasör içinde Action Name ile aynı olan View yoksa Shared klasöre bakılır. View türlerine göre de sıralama vardır. ilk önce 2 klasör de de -aspx -ascx uzantılı View'lar aranır. Sonra -cshtml -vbhtml aranır.
     */
    public class EmployeeController : Controller
    {
        private EmployeeContext db = new EmployeeContext();
        public ActionResult Index()
        {
            var tblemployee = db.tblEmployee.Include("Department");
            return View("List", tblemployee.ToList());
        }

        public ActionResult EmployeesByDepartment()
        {
            var departmentTotals = db.tblEmployee.Include("Department").GroupBy(x => x.Department.Name).Select(y => new DepartmentTotals
                                                                                                        {
                                                                                                            Name = y.Key,
                                                                                                            Total = y.Count()
                                                                                                        }).ToList().OrderByDescending(y => y.Total);
            return View(departmentTotals);
        }

        public ActionResult Details(int id = 0)
        { Employee employee = db.tblEmployee.Single(e => e.EmployeeId == id); if (employee == null) return HttpNotFound(); return View(employee); }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.tblDepartment, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name)) ModelState.AddModelError("Name", "The name field is required.");

            if (ModelState.IsValid) { db.tblEmployee.AddObject(employee); db.SaveChanges(); return RedirectToAction("Index"); }
            ViewBag.DepartmentId = new SelectList(db.tblDepartment, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.tblEmployee.Single(e => e.EmployeeId == id);
            if (employee == null) return HttpNotFound();
            ViewBag.DepartmentId = new SelectList(db.tblDepartment, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Name")] Employee employee)
        {
            Employee employeeFromDB = db.tblEmployee.Single(x => x.EmployeeId == employee.EmployeeId);
            employeeFromDB.EmployeeId = employee.EmployeeId;
            employeeFromDB.Gender = employee.Gender;
            employeeFromDB.City = employee.City;
            employeeFromDB.DepartmentId = employee.DepartmentId;
            employee.Name = employeeFromDB.Name;

            if (ModelState.IsValid)
            {
                db.ObjectStateManager.ChangeObjectState(employeeFromDB, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.tblDepartment, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        public ActionResult Delete(int id = 0)
        { Employee employee = db.tblEmployee.Single(e => e.EmployeeId == id); if (employee == null) return HttpNotFound(); return View(employee); }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.tblEmployee.Single(e => e.EmployeeId == id);
            db.tblEmployee.DeleteObject(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}