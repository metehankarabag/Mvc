using System.Web.Mvc;

namespace _4_Controllers.Controllers
{
    /*
      Uygulama kodunun yazıldığı Class'lardır. Client'ın Server ile iletişime geçmek için bu Class'ın Action methodlarını kullanır. Fakat uygulama ilk çalıştığında herhangi bir Controllerin ve bu Controllerin bir Action methodu Url'de yazmamasına rağmen bir Controller Class'ının Action methodu çalıştır. Bunun nedeni uygulamanın App_Start klasörü içindeki RouteConfig CLASS'ının RegisterRoutes() STATIC methodu Client'ın kullanacağı URl'i düzenlemesidir. Bu method Global.asax'ın Application_Start() methodunda kullanıldığı için Client' isteği ile uygulama her çalıştığında RegisterRoutes() methoduda çalışır ve kullanılacak Url düzenlenir. Burada yapılan ayarlara göre kullancı URL'e bir controller ve Action method yazmamaışsa, bizim varsayılan olarak verdiğimiz Controller ve Action method kullanılır. 
      RegisterRoutes() methodunun RouteCollection türünde nesne bekleyen tek overload'ı var. Bu methodu Application_Start() Event'ında kullanabilmek için Rautetable Class'ının üyesi olan RouteCollection türündeki Routes Read-Only Property'sini methoda parametre olarak vermişiz. RouteCollection CLASS'ının MapRoute() methodu yönlendirmenin ayarlandığı methoddur. Bu method RegisterRoutes() methodu içinde kullanılmış. Method parametreleri, 
      name: yönlendirme ayarlarını temsil eden isim, 
      Url: Methodu tetikleyebilecek URL yapısını ve Url'in tarayıcıda nasıl görüneceğini belirlediğimiz parametredir. Kullanıcının gönderdiği Url şeması, bu parametrede belirlediğimiz Url şemasına benziyor ise method çalışır, benzemiyorsa method çalışmaz. MapRoute() methodunu RegisterRoutes() methodu içinde bir kaç kez kullanabiliriz. Kullanıcının gönderdiği Url şeması ilk hangi methoddaki şema ile eşleşirse o method çalışır diğerleri çalışmaz.
      defaults: Eşleşme sağlandığında Url'in bölümlerine verilecek varsayılan değerleri belirlediğimiz parametre. Parametrenin türü Object olduğu için Anonymous Method oluşturup, method içinde varsayılan değerleri dinamik Property'e veriyoruz. Url parametrinde belirlediğimiz, Property isimleri bölüm adlarına uygun olmalı.
      
      1. Not: Url şemasını belirlerken, {} içinde belirlediğimiz Url bölümleri, Client'ın kullandığı Url'inde kullanmasa bile eşleşme sağlanabilir. Url eşleşmesi sağlanmışsa, parantez içinedeki bölümler için defaults parametresindeki değer kullanılır. Yine eşleşme sağlanmışsa, {} parantezler belirlediğimiz bölümler tarayıcıda görünmez. Fakat bir {} kullanmadan bir Url bölümüne değer vermişsek, eşleşmenin sağlanabilmesi için kullancının Url'i belirlenen bölüm için verdiğimiz değeri kullanmalıdır ve bölüm tarayıcıda görüntülenir.
      2. Not: RouteCollection Class'ında da RouseBase türünde bir Read-Only bir Index olduğu için Routes Property'sine Index değeri verebiliriz.
      Not: Yukarıdaki herşeyi aklımda kalanlara göre yazdım. Eksik veya yanlış olabilir.
    */
    public class HomeController : Controller
    {
        public string Index(string id, string name)
        {
            return "id= " + id +" name = " + name /*" Name = "+Request.QueryString["name"]*/; 
           
        }
    }
}
