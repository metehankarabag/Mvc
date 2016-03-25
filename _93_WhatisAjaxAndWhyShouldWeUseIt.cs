using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _93_WhatisAjaxAndWhyShouldWeUseIt.Models;

namespace _93_WhatisAjaxAndWhyShouldWeUseIt.Controllers
{
    /*Bir önceki derste yazdığım için burada Avantajlarını ve Dezavantajlarını yazıyorum. 
      
      AVANTAJ'ları
      1. Ajax Server'a sayfanın bir bölümünün gönderilmesini avantajı sayfanın diğer bölümlerinin Server'da olmamasıdır. Çünkü tüm sayfa Server'a gönderilirse, Server tüm sayfa 
işleme sokar. Bu yüzden işlem bitirene kadar başka istek yapılmasını engeller. Yani işlem 10 sn'e sürerse kullanıcı 10 sn'e beklemek zorunda. Ajax ile sayfanın bir kısmını Server'a gönderdiğimizde, Server tüm sayfa üzerinde çalışmadığı için yeni istek gelmesini engellemez.(Yeni isteğin Ajac ile Server'a gönderilen alandan gelemeyeceğini düşünüyordum ama gelebiliyor.) Yani Ajax kullanılmayan sayfa Synchronous Request-Response Cycle ile kullanan Asynchronous ile çalışır.
      2. NETWORK trafiğini azaldır ve performans'ı arttırır. Yani Ajax, uygulamanın sadece gerekli veriyi göndermesine ve almasına izin verir.
      3. Ekran sıçramaları olamaz.
      
      DEZAVANTAJ'ları
      1. Tüm sayfa güncellenmediği için yapılan işlemler LINK'i değiştirmez. İlk Link değişmediği için BookMark yapılamaz. Çünkü link çalıştırıldığında sayfanın ilk hali görünecek.
      2. AJAX JavaScript'i temel alarak çalışır. Kullanıcı tarayıcısında JavaScript'i engellerse AJAX çalışmaz.
      3. Debug zordur. Neden zor olduğunu anlayamadım.
      4. Arama motorları uygulamanın AJAX kullanan sayfalarını INDEX'leyemez.
      
     Ajax kullanan uygulamalar
     1. Youtube, Google, vs.., otomatik arama tamamlaması kullanmak için
     2. Gmail, otomatik kaydetmeyi gerçekleştirmek için ve REMOTEVALIDATION uygulamaak için AXAJ kullanır. 
        Yani CLIENT'da kullanıcı adının varlığını kontrol etmek için
     3. Facebook sayfayı aşağı indirirken veri yüklemek için AXAJ kullanır.
      
     Yani Ajax genellikle AutoComplate, AutoSave, Remote Validation için kullanılır.
      
     */
    public class HomeController : Controller
    {
        SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(); }
        public PartialViewResult All() { List<Student> model = db.Students.ToList(); return PartialView("_Student", model); }
        public PartialViewResult Top3()
        { List<Student> model = db.Students.OrderByDescending(x => x.TotalMarks).Take(3).ToList(); return PartialView("_Student", model); }
        public PartialViewResult Bottom3() { List<Student> m = db.Students.OrderBy(x => x.TotalMarks).Take(3).ToList(); return PartialView("_Student", m); }
    }
}
