using System.Collections.Generic;
using System.Web.Mvc;

namespace _71_ChildactiononlyAttribute.Controllers
{
    /*
     ChildActionOnly Attribute'u uygulandığı Action Methodun Url'den çalıştırılmasını engeller. Method sadece Uygulamadaki View'larda kullanılan Action ve RenderAction() html helper'ları ile çalıştırılabilir. Bu helper'lar normal bir Action Methodu da çalışırabilir, çalıştrılacak methodun Url'den tetiklenmesini istemediğimiz için ChildActionOnly Attribute'unu kullanıyoruz. Methodu NonAction olarak belirleyip Url'den çalışmasını engelleyebilirdik fakat bu sefer de Html Helper methodlar NonAction olan methodu bulamayacağı için View'ı görüntülenemeyecekti.
     
      Derste 2 Action method var. kullanıcı Index Action'ı çalıştırdığından Index View'da kullandıımız RenderAction() methodu Countries() ChildOnlyAction methodunu parametreleri ile çalıştrıyor ve Index View'a Child Action View'ı eklenip tarayıcıya yansıtılıyor. Böylece parametreli Action'ı parametre girmeden varsayılan parametler ile çalıştırmış oluyor.
      
     1. Not: ChildActionOnly uygulanmış bir method Url'den tetiklenirse -- The ACTION 'Countires' is by accessiable only by a child request hatası verir.
     2. Not: RenderAction() VOID'in anlamı direk OUTPUTSTREAM a yazılan değerler dir. VOID dönüyor yazımı @{} içinde olacak
     3. Not: Sanırım View'da RenderAction() methodu ile çağardığımız Action View dönmek zorunda
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }[ChildActionOnly]public ActionResult Countries(List<string> countryNames) { return View(countryNames); }
    }
}
