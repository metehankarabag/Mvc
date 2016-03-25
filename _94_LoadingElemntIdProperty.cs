using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _94_LoadingElemntIdProperty.Models;

namespace _94_LoadingElemntIdProperty.Controllers
{
    /*
      Ajaxoption Class'ın LoadingElementId Property'si Ajax sayfanın bir bölümünü Server'a gönderdiğinde Server'dan cevap gelene kadar kullanıcıya gösterilecek ekran görüntüsünü belirler. Bu yüzden kullanıcıya gösterilecek görüntü hazır olmalı. Yani yeni bir alan oluşturup görüntüyü alan içinde sayfa ilk yüklendiğinde yüklememiz gerekiyor. Alanı oluşturan Tag'ın Id değerini Property'e verdiğimizde çalışıyor.
      Kullanıcıya Animasyonlu bir gif göstereceğimiz için img Tag'ını kullandık.
      Gif oluşturmak için bu siteyi kullan -> http://siffygif.com 
     */

    /* 96 Loading Element Duration Property
      Server'a yapılan istek sonucunuda eklediğimiz görüntünün açılma hızını belirliyor. Yani görüntü yavaş yavaş yükleniyor. 
      Bu Property'e 1000 değerini verdiğimizde sorun yok. Fakat 3000 gibi bir değer verdiğimizde ayar geçerli olmuyor. Bunun nedeni JavaScript dosyasındaki bir bug'dan oluyormuş. Property'e verdiğimiz değer Html'de Tag'ın data-loading-duration Attribute'una geliyor. JavaScript methodu bu Attribute'daki değeri getAttribute() methodu ile alıyor. Bu bug'ı çözmek için methoddan çıkan değeri parseInt() methodu ile int'e çevirmemiz gerekiyor. (değişikliği jquery.unobtrusive-ajax.js de yapmış ama min de yadabiliriz. veya dosyayı düzenleyip yeni min dosyası oluşturabiliriz.)
     */

    public class HomeController : Controller
    {
        SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(); }
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
