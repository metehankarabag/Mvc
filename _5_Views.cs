using System.Collections.Generic;
using System.Web.Mvc;

namespace _5_Views.Controllers
{
    /*
      Geçen derslerde kullandığımız Action methodlar sonucunu direk tarayıcıya yazdırıyor. Action methodun dönüş türünü COMPLEX TYPE olarak belirleyip oluşturduğumuz Copmlex Type nesneyi return a verdiğimizde, tarayıcıda nesnenin ToString() methodundan çıkan değeri görürürüz. Bu yüzden Action methodun dönüş değerini direk tarayıcıya yazdıramıyoruz. Bu durumda VIEW kullanmak gerekir. View basitçe bir Html sayfasıdır. Değerler View'a gönderilir Server'da sayfayı hazırlanıp tarayıcıya gönderilir. Bir Action Methodun View çalıştırabilmesi için dönüş türünün ActionResult olması gerekir. Bu yüzden oluşturduğumuz nesneyi direk return a veremeyiz. Çalışrılıcak Viewı ve View'a gönderilecek nesneyi Controller Class'ının View() methodu belirlediği için return'a View() methodunu vermeliyiz. Bu methodun 8 overload'u var ve hepsi parametre olarak aldığı nesneyi parametrelerinde belirlenen View'a göndererek çalıştırmak için kullanılır.
      Not: ActionResult Base Class'dır. Bu Class'dan türeyen Class'lar var. Controller Class'ında bu türleri dönen başka View() methodları da var.
      Not: View() methodunu parametresi kullanırsak varsayılan olarak Action method adında bir View'ı çalıştırır ve değer göndermez.
      
      Bilgiler ACTION method'dan VIEW'a 3 farklı yolla gidebilir. --VIEWBAG --VIEWDATA -- View() Methodu ile
     */
    public class HomeController : Controller
    { public ActionResult Index() { ViewBag.Countries = new List<string>() { "India", "UK", "US", "Canada", }; return View(); } }
}
