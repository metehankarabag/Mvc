using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _3_CreatingYourFirstMvcApplication.Controllers
{
    /*
     URL, Asp.net uygulamaları için çalıştırılacak PAGE veya herhangi bir dosyanın yolunu belirtir.
     http://..../WebForm1.aspx
     URL, MVC'de ise Assembly adından sonra CONTROLLER adı sonra CONTROLLER içindeki bir ACTION METHO adı sonra parametreler
     http://..../Home/Index/1
     
     CONTROLLER veye içindeki methoddan biri doğru yazılmassa veya yoksa 404 hatası alırız.
     */
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello from MVC application";
        }
        public string GetDetails()
        {
            return "GetDetails invoked";
        }
    }
}
