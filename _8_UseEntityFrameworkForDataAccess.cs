 using System.Linq;
using System.Web.Mvc;
using _8_UseEntityFrameworkForDataAccess.Models;

namespace _8_UseEntityFrameworkForDataAccess.Controllers
{
    /*
      ENTITY FRAMEWORK, Nuget package Manager'den indir. İndirdikten sonra uygulamaya EntityFrameWork assembly'si eklenir.
      System.Data.Entity Namespace'i içinde olan DbContext CLASS'ı veritabanına bağlanıp programa veri getirme işlerinin tamamını yapar. (Bu Class'dan türeyen bir Class oluşturduk.) Kullanılacak ConnectionString'i Web.config dosyasında DbContext'den türeyen Class'ın adı ile aranır. Aynı isimde ConnectionString'i varsa kullanılır. DbContext Class'ının veritabanından aldığı tabloyu bir DbSet<T> olarak Programa getirir ve tabloyu veritabanında DbSet'in base Type'ın adına göre arar. Base Type olarak kullandığımız Class'ın adı ile aynı adda bir tablo varsa ve tablosun'ları ile Class Property'leri tam olarak eşleşmezse hata alırız. DbContext, Class'ı tablo olarak kullanabilmesi için Class'a [Table("tabloadı")] Attribute'unu uygulamamız gerekir.
      Son olarak Global.asax'de Database.SetInitializer {} kullanıyoruz. Bu aranan tablo veri tabanında yoksa bir tane oluşturup basit verileri ile doldurmamızı sağlar. Yani parametre olarak veri tabanının nasıl oluşturulacağını belirememiz gerekir.
     */
    public class EmployeeController : Controller
    {
        EmployeeContext employeeContext = new EmployeeContext();
        public ActionResult Details(){ Employee employee = employeeContext.Employees.Single(emp => emp.EmployeeId == 1); return View(employee);}
    }
}
