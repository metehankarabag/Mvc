using System.Web.Mvc;

namespace _57_RazorViews.Controllers
{
    /*
     Razor VIEW'da @ ile C# kodları kullanılır. 
     VIEW'da if, for, foreeach gibi bir blok içinde çalışan kodlarda, blokları içinde HTML TAG'ları kullanabiliriz ve bu kodların parametrelerine dışarıdan ulaşamayız. İçerideki Html'lerde kullanmayı istiyorsak @ ile kullanabiliriz.
     
     @{} VIEW'da C# kod alanı verir. Bu alan içinde oluşturulmuş değişkenlere @ kullanarak alan dışından ulaşabiliriz.
     Bu alan içinden direk tarayıcıya yazı yazmayı istiyorsak <TEXT> TAG'ını veya @: kullanmalıyız.
     */
    public class HomeController : Controller { public ActionResult Index() { return View(); } }
}