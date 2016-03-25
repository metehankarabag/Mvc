using System.Web.Mvc;

namespace _76_ValidateInputAttribute.Controllers
{
    /*VALIDATE INPUT ATTRIBUTE
      CROSS-SIDE SCRIPTING ATTCK'ı işlediğimiz 56. Dersde görmüştük. MVC varsayılan olarak Client'dan Server'e ValidateInput Attribute'ını kullanarak bazı karakterlerin gönderilebilmesini engelliyor.
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        [HttpPost]
        public string Index(string comments) { return "Your Comments: " + comments; }
    }
}
