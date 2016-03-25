using System.Web.Mvc;

namespace _58_RazorViews.Controllers
{
    /*
     Comman: ctrl -k+c
     Uncomman: ctrl -k+u
     @ den sonra bir c# değişkeni kullanıyorsak ve boşluk olmadan yazı ekleniyorsa(i.png) değişkeni yazının geri kanından ayırmak için () içine yazarız.
     @ işareti bir çok yerde koda geçmek dışında kullanabiliriz.
     örneğin email adresi alırken. Email'da @ kullandığımızda RAZOR bunu anlayabiliyor ve hata vermıyor.
     Fakat başka  @'i yazdırmak istiyorsak @@ olarak kullanırız.
     */
    public class HomeController : Controller { public ActionResult Index() { return View(); } }
}