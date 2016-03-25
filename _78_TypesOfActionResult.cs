using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _78_TypesOfActionResult.Controllers
{
    
    public class HomeController : Controller
    {
        /*ILSpy prgoramı tüm ASSEMBLY'leri gösteriyor. .NET'in semasını veriyor. 
         ActionResult: Bir çok alt(türemiş) TYPE'ı olan bir ABSTRACT TYPE'dır.
         ILSyp'ı indirip çalıştır. FILE -> Open FROM GAC (Burada tüm .NET ASSEMBLY'leri var.) Aradığımız Class'ın nameSpace'ini programa yükleyerek içindeki tüm Type'ları Type'ların base ve türemiş Type'larını görebiliriz.Burada bir CLASS'a çift tıklarsa CLAS Decompled olur ve içinde kullanabileceğimiz her şeyi gösterir.
         
         Bir ACTION METHOD bir çok farklı türde nesne dönebilir örneğin
         ViewResult
         PartialViewResult
         JsonResult
         RedirectResult etc..

         Base Type olarak ActionResult kullanmmızın nedeni uygulama duruma göre birden fazla dönüştürü kullanabileceği içindir. kesin olarak tek bir tür kullanacaksak o türe ait Class'ı dönmek gerekir. 
         Bir tablo var Jpeg olarak çektim PROJECT klasörü içinde duruyor.
         */
        public ActionResult Index()
        {
            return View();
        }

    }
}
