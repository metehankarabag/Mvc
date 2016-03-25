using System.Collections.Generic;
using System.Web.Mvc;

namespace _6_ViewDataandViewBag.Controllers
{
	/*
      ViewBag ContollerBase Class'ının dynamic Read-Only Property'si iken ViewData ViewDataDictionary türündeki PROPERTY'sidir.
      ViewDataDictionary CLASS'ında tanımlanmış object dönen INDEX olduğu için ViewData'ya INDEX değeri verebiliriz.(Index verdiğimizde object döner vermediğimizde ViewDataDictionary)
      
      ViewBag dinamik bir Property'si ile ViewData strin Key'i ile View'a kullanılır. View'da Property ve Key'i kullanarak Action method'da verdiğimiz değerleri alabiliriz.
      Not: dynamic özelliği c# 4 de tanıtılmış.
      
      Olumsuz yanları 2 side düzenleme zamanı hata kontrolü sağlamaz. STRONGLY TYPE MODEL kullanmak her zaman iyidir.
    */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Countries1"] = new List<string>();
            ViewBag.Countries = new List<string>()
            {
                "India",
                "UK",
                "US",
                "Canada"
            };
            return View();
        }
    }
}