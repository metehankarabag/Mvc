using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _99_WhatisContentDeliveryNetwork.Controllers
{
     /*
      CDN dünyadaki tüm bilgisayarların NETWORK'üdür. Bir WEB uygulamasında JQUERY kütüphanelerini kullanmaının 2 yolu var. 1. Dosyaları kopyalayıp refenranslarını programda kullanmaktır. 2. Yol CDN'den kütüphanelere referans vermektir.
      Yani internet yüklemiş JQUERY dosyaları var. LOCAL'daki bir dosya yerine internetteki bir dosyayı kullanmak mantılık olabilir.
      
      Yararları
      1. CACHING: Uygulamamızda kullandığımız bir Jquery verisiyonu daha önceden kullanıcının ziyaret ettiğin bir siteden Pc'sine indirilmişse, dosya tekrar indirilmeyecek. 
      2. JQUERY dosyasınını depoladığımız SERVER'den çok uzakdaki bir kullanıcı uygulamayı çalıştırdığında, dosya uzun bir yol alır. CDN kullanırsak, kullanıcıya en yakın olan SERVER'den dosya indirilir.
      3. Dosyalar CND'den indirileceği için uygulamanın NETWORK trafiğini azaltır.
      4. Referans'a olarak kullanılan bir Host'dan aynı anda indirilebilecek Component 2'dir. yani bir bileşen 1 sn'de iniyorsa, 4 bilesen varsa 2 sn'de iner. Bileşenleri farklı hostlara dağatarak bunu çoğaltabiliriz. Aşağıda 3 tane host var hepsinden 1 sn de 2 bilesen indirirsek sn'de 6 tane indirebiliriz. Kendi hostumuzuda eklersek 8 olur.
      
      http://code.jquery.com/jquery-1.7.1.min.js
      http://axaj.aspnetcdn.com/ajax/jQuery/jquery-1.7.1.min.js
      http://axaj.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js
     */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
