using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _92_ExampleOfAjax.Models;

namespace _92_ExampleOfAjax.Controllers
{
    /*
      Ajax, Web sayfanın belirlenen bölümünü, sayfadan bağımsız olarak Server'a gönderme yeteneğidir. Mvc'de AjaxExtensions Class'ının üyesi olan 5 tane AjaxHelper bulunur ve bu  Helper'lar ile oluşturulan Html Tag'ları Ajax Property'leri içerir. Oluşturulan Tag'lar tetiklendiğinde, bu Property'ler kullanılarak tarayıcıda JavaScript kodları çalıştırılır ve bu kodlar sayesinde Server'a sayfanın sadece belirlenen bölümünün gönderilir. Bölüm Server'a geldiğinde Helper'da belirlediğimiz Action Method çalıştırılır ve sayfanın belirlenen bölümü Action methodun View'ı ile doldurur. Yani Ajax kullanarak oluşturduğmuz Tag'lar Button gibi Post'alama işlemi gerçekleştiren Tag'ları olmadılır. Ajax ile server'dan gelen View page source'da görünmüyor ve Action methodun dönüştürünü PartialView olarak ayarlamış fakat normal View döndüğümüzde de sorun olmuyor.
      
      AjaxHelper'lar Extension parametre olarak AjaxHelper bekleyen methodlardır. Bu yüzden methodlara AjaxHelper nesnesi dönen kodlardan sonra ulaşabiliriz. View'da WebViewPage<TModel> Abstract Class'ının Ajax Property AjaxHelper nesnesi döndüğü için bu methodara bu Property'sinden ulaşırız. Aynı mantıkla Html Property'si HtmlHelper nesnesi döndüğü için bu property'den HtmlHelper'a ulaşırız. AjaxHelper'lar -> ActionLink(),BeginForm(),BeginRouteForm(), GlobalizationScript() ve RouteLink() methodularıdır. Bu methodların hepsi Controller'da bir Action method çalıştırmak için parametre ister. Methodların tüm overload'larında son parametre olarak istenilan AjaxOption Class'ın oluşturulacak Html Tag'ının Ajax Property'lerini belirler.
       
      AjaxOptions Class'ının 11 Property'si 1 Constructor'ı ve bir de methodu var. Tüm Property'leri değer alabiliyor. 2 property hariç diğer tüm Property'ler string türünde bu 2 property'den birinin türü InsertionMode(Enum) diğerinin int.
      HttpMethod: Action Methodu çalıştıracak istek türünü belirler.
      UpdateTargetId: Axaj'in Server'da göndereceği alanı belirler. Parametre olarak alanı veren Tag'ın ID değerini alır.
      InsertionMode: Server'dan gelecek View'ın geçerli görünüme nasıl geleneceğini belirler InsertionMode'da 3 değer var. -replace, insertAfter, insertBefore
       
      Diğer Property'leri sonraki derste göreceğiz.
     */
    public class HomeController : Controller
    {
        SampleDBContext db = new SampleDBContext();

        public ActionResult Index() { return View(); }
        public PartialViewResult All() { List<Student> model = db.Students.ToList();return PartialView("_Student", model); }
        public PartialViewResult Top3()
        { List<Student> model = db.Students.OrderByDescending(x => x.TotalMarks).Take(3).ToList(); return PartialView("_Student", model); }
        public PartialViewResult Bottom3() { List<Student> m = db.Students.OrderBy(x => x.TotalMarks).Take(3).ToList(); return PartialView("_Student", m); }
    }
}
