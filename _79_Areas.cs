using System.Web.Mvc;

namespace _79_Areas.Controllers
{
    /*
     Karmaşık uygulamalar için AREA'lar kullanılır. Bir AREA oluştruduğumuzda Area Registration Class Area'nın oluşturulduğu Class'ın içine eklenir. Bu Class RouteConfig Class'ı ile aynı işi yapar. Uygulamada bir Controller ve Action methodu birden fazla kes kullanabiliriz. Uygulama ilk çalıştığında RouteConfig Class'ındaki ayarlara göre Main Area'daki HomeController'in çalışması gerekir. Fakat uzun bir hata alırız.  Kısaca hatanın nedeni CONTROLLER adı ile eşleşen bir çok TPYE olması ve çalıştırılacak CONTROLLER'in hangi NAMESPACE'deki Controller'in kullanılacağını belli olmamasından kaynaklanır. RegisterRouteConfig methodu içinde kullandığımız MapRoute() methodunun 5. overload'ı parametre olarak bir namespace alır. Hangi Name Space içindeki Controller'i çalıştırmayı istiyorsak onu kullanabilriz.
     
     Root Url'den sonra Areas klasörünü yazmadan direk AREA adını yazarsak, Area'nın varsayılan ACTION'ını çalıştırırız. Fakat AreaRegistiration doyasıda varsayılan olarak controller name belirlenmediği için belirlemessek hata alırız. Belirledikten sonra gene hata verdir. Bu hatayı çözmek için Package Manager Console'u aç. Instal-Package Microsoft.Web.Optimization -Pre kodunu çalıştır.(Build et.)
     ACTIONLINK ile başka bir AREA'daki alana girmeyi istiyorsak, ActionLink'in 7. overload'ını kullanmamız gerekir. 7. overload'ın 4. parametresi objext routeValues anonymous method içinde area adında bir Property oluşturup Area adını veriyoruz. AREA adını parametre olarak eklemeseek, gitmeyi istediğimiz Area'daki CONTROLLER ve ACTION bulunduğumuz AREA'da aranır. 
     Not: Doğru overload'ı kullanmadığımızda URL'e ?Lenght=4 QueryString'i ekleniyor.
     */
    public class HomeController : Controller { public ActionResult Index() { return View(); } }
}
