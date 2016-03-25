using System.Linq;
using System.Web.Mvc;
using _40_UsingDisplayNameAndFormatScaffold.Models;

namespace _40_UsingDisplayNameAndFormatScaffold.Controllers
{
    /*
      Display Attribute: Uygulandığı Field/Property'lerin görünüm özelliklerini Constructor'ındaki Property'lere verilen değerler ile belirler. 
      DisplayName Attribute: Uygulandığı Field/Property'lerin NAME özelliğini Constructor'inde Property kullanmadan belirler.
      DisplayFormat Attribute: Constructor'undaki parametrekler ile Property verisinin nasıl görüneceği
     
      ScaffoldColumn Attribute: Contructor'ına BOOLEN değer alır. Property'nin görünüp görünmeyeceğini ayarlar. (View Source'da görülür.) View'da sadece DisplayForModel() kullanıldığında çalışır.
      
     */
    public class HomeController : Controller
    {
        public ActionResult Details(int id) {SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e);}
    }
}
