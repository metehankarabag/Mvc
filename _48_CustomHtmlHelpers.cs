using System.Linq;
using System.Web.Mvc;
using _48_CustomHtmlHelpers.Models;

namespace _48_CustomHtmlHelpers.Controllers
{
    /*
      Tüm HtmlHelper'lar, MvcHtmlString nesnesi dönen ve ilk parametresinde HtmlHelper öreneği alan EXTENSION METHOD'lardır. Yani bir HtmlHelper oluşturmak için MvcHtmlString türünde değer dönen ve ilk parmetresi HtmlHelper alan bir EXTENSION METHOD oluşturmamız gerekir. (Extension methodlar STATIC CLASS'ların ilk parametresini this anahtarı ile alan STATIC methodlarıdır.)
      
      Oluşturduğumuz methodun dönüş türü IHtmlString'dir. Bu INTERFACE'i MvcHtmlString SEALED CLASS'ının base INTERFACE'ı olduğu için return TYPE olarak MvcHtmlString nesnesi verebiliriz. 
      MvcHtmlString CLASS'ı Server'dan kodlanmamış String çıkarmak için kullanılır. Parametre olarak oluşturulacak Tag'ın adını alan bir Constuctor'u vardır. Bir Tag oluşturmak için TagBuilder CLASS'ı kullanırız. TagBuilder CLASS'ının TagRenderMode Enum'undan bir değer bekleyen ToString() methodu var. Bu method TagBuilder üyelerini kullanarak hazırladığımız TAG'ı düzenler ve string olarak yansıtır. MvcHtmlString parametre olarak aldığı string içindeki <,>,/,' gibi karakterleri kodlamadan tarayıcıya gönderir. Kodlanmış String ACSII karakterlerin HTML eşitlerine çevrilmesidir. Tarayıcı Kodlanmamış String'i Server'den alırsa HTML TAG'ını oluşturur. Tarayıcı kodlanmış String alırsa, String yorumlanarak kullanıcıya ACSII karateri gösterilir.
      
      TagBuilder CLASS'ının kullanarak bir tag oluşturmak için ilk önce TagBuilder CLASS'ının Contructor'ına parametre olarak oluşturulacak Tag adını vermeliyiz. TagBuilder CLASS'ının Attributes READ-ONLY PROPERTY'si, IDictionary<string,string> türünde nesne döndüğü için PROPERTY'e bu INTERFACE'in Add() methodunu uygulayabiliriz. Add() methodu Tag'a PROPERTY eklemek için kullanılır. Add() methodu biri parametre key için diğeri Value için 2 parametre alır.
                 
     1. Not: Kullanıcı src Attribute'ına virtual Path'ı girer bunu VirtualPathUtility CLASS'ının STATIC methodu ToAbsulute() methodu içine alarak FullPath a çeviririz.
     2. Not: Sonuçta VIEW'a bir CLASS'ın nesnesini kullandığımız için using ile CLASS'ın adını VIEW'a eklemeliyiz. Bu işi her VIEW için yapmamız gerektiğinden zordur. VIEW içindeki  Web.Config'i kullanarak Helper'ı VIEW'larda kullanabiliriz.
          <system.web.webPages.razor> taghı içinde NAMESPACE olarak ekleyeceğiz.
          İsim uzayını web.config'den aldığımızda vs intelicanse çalışmıyor bunu düzeltmek için solutionu kapatıp baştan açıyoruz.
      
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult Details(int id) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
        public ActionResult Edit(int id) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                SampleDBContext db = new SampleDBContext();
                Employee employeeFromDB = db.Employees.Single(x => x.Id == employee.Id);

                employeeFromDB.FullName = employee.FullName;
                employeeFromDB.Gender = employee.Gender;
                employeeFromDB.Age = employee.Age;
                employeeFromDB.HireDate = employee.HireDate;
                employeeFromDB.Salary = employee.Salary;
                employeeFromDB.PersonalWebSite = employee.PersonalWebSite;

                db.ObjectStateManager.ChangeObjectState(employeeFromDB, System.Data.EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = employee.Id });
            }
            return View(employee);
        }
    }
}
