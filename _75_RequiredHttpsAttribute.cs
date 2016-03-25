using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _75_RequiredHttpsAttribute.Controllers
{
    /*
      RequireHttps ATTRIBUTE: Kullanıcının Action Methodu kullanabilmesi için Url'de Https kullanması gerekir. Kullanıcı Http kullanırsa otomatik olarak Https'e çevirilir. Http ile Server'a gönderilen değerler Filder ile görüntülenebildiği için önemli bir değer varsa Https kullanmalıyız.
     */
    public class HomeController : Controller
    {
        [RequireHttps]
        public string Index()
        {
            return "This mehod should be accessed only using HTTPS protocol";
        }

    }
}
