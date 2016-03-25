using System.Web.Mvc;

namespace _69_ActionFilters.Controllers
{
    /*
      Action Filters: Controller'a veya Action Method'a uygulanabilen Attribute'lardır. Controller'a uygulanırsa tüm Action methodlara uygulanmış olur.      
      Bazı Action Filder'lar 
      Authorize       
      ChildActionOnly
      HandleError
      OutputCache 
      RequireHttps
      ValidateInput
      ValidateAntiForgeryToken 
      Custom Action Filter
     */
    /*70. Ders - Authorize and AllowAnonymous Attribute
      MVC'de varsayılan olarak CONTROLLER ACTION METHOD'lara herkes ulaşabilir. Yani View'lara ulaşabilir. Bazı view'ların sadece Authenticated ve Authorised kullanıcılar tarafından görüntülenebilmesini istiyorsak, View'ı çalıştıran Action Method'a Authorize Attribute'unu uygulamalıyız.
      Uygulamanın WindowsAuthentication kullanabilmesi IIS'de WindowsAuthentication'ı açmak gerekli. Açtıktan sonra Controller'daki bir ActionMethod'a Authorize Attribute'unu uygularsak, Action Method'u sadece girişli kişiler çalıştırabilir. 
      Not: Authorize Attribute'u IIS'de belirlenen açık Authentication'a göre çalışıyor. Derste sadece WindowsAuthentication açık olduğu için Windows kimliğini gireileceğimiz bir pencere geldi. FormsAuthentication'ıda açık methodu çalıştırdığımda ise Login.aspx'e yönlendirdi.
      
      AllowAnonymous Attribute: Anonymous olarak çalıştırılabilecek methodu belirler. Authorize Attribute'unu Controller seviyesinde uygularsak, bir kaç methodu Anonymous'a çevirmek iin kullanabiliriz.
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
    }
}
