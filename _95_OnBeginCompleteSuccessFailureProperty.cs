using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _95_OnBeginCompleteSuccessFailureProperty.Models;

namespace _95_OnBeginCompleteSuccessFailureProperty.Controllers
{
    /*
      AjaxOption Class'ının bu  Property'leri Ajax'ın yaşam döngüsünü sağlayan EVENT'ları temsileder. Bu yüzden parametre olarak çalışıtırılacak javaScript methodunun adını veriyoruz.
      Bu PROPERTY'ler SERVER ile CLIENT arasında konumlanırlar.
      OnBegin: JavaScript methodunu, ACTION METHOD SERVER'da çalışmadan önce çalıştırır. Yapılan Control işlemi ile ActionMethodun çalışması engellenebilir. Kontrol 2 kez çalıştırıldığı anda ilk çalıştırıldığında aldığımız veriyi ekrandan silebiliriz.
      OnSuccess: JavaScript methodu, PAGE güncellendikten sonra çalıştırır. Veritabanından gelen veri sayısını gösterebiliz.
      
      OnComplate: JavaScript methodunu, Server Response'unun bir örneği oluşturulduktan sonra ve yeni View sayfada eklenmeden önce çalıştırır.
      OnFailture: JavaScript methodu,PAGE güncelleme işleminin herhangi bir anında hata olursa çalıştırır
     */
     */
    public class HomeController : Controller
    {
        SampleDBContext db = new SampleDBContext();
        public ActionResult Index(){return View();}
        public PartialViewResult All() { System.Threading.Thread.Sleep(1000); List<Student> m = db.Students.ToList(); return PartialView("_Student", m); }
        public PartialViewResult Top3()
        {
            System.Threading.Thread.Sleep(1000);
            List<Student> model = db.Students.OrderByDescending(x => x.TotalMarks).Take(3).ToList();
            return PartialView("_Student", model);
        }
        public PartialViewResult Bottom3()
        { System.Threading.Thread.Sleep(1000); List<Student> m = db.Students.OrderBy(x => x.TotalMarks).Take(3).ToList(); return PartialView("_Student", m); }
    }
}