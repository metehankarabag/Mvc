using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _100_WhatIfCDNisDown.Controllers
{
    /*Cnd kapanırsa ne olur?
      PC'nini internet bağlantısını kes ve CACHE'i sil. JQuery dosyası internetten indirelemeyeceği için uygulama çalışmaz.
      windows.jQuery Property Jqery internetten indirilebilmeşse True döner. || indirilememisse çalışacak kodu belirler. Burada yapmayı istediğimiz şey kendi hostumuzdaki Jquery dosyasına referans vermek. document.write() methoduna parametre olarak JavaScirpt dosyamızın yolunu Script Tag'ı içinde yazıyoruz. Method javaScript'e ait olduğu için HtmlEncoding falan olmuyor demekki. Fakat sadece bir karakteri değiştiriyoruz. Çünkü </ karakterlerini View'da text içinde yazdığımızda bile JavaScript olarak algılanıyor. Bu yüzden< yerine kodlanmış Html giriyoruz. \x3C-> Bu değer page soruce'da gönderildiği gibi görünüyor fakat çalışıyor.(Demekki tarayıcıya gnderilen kodlanmış String'i ile sadece Tag oluşturamıyoruz.)
      
      NOT: windows.jQuery Property'si beklenen jquery dosyasını bilgisayarda da arar varsa true döner.
      Not: Tarayıcı geçmişini sildiğimizde o anda ekranda olan sayfanın kullandığı değerler silinmiyor Cleaner ile denemedim.
      
     */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
