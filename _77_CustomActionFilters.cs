using System;
using System.Web.Mvc;
using _77_CustomActionFilters.Common;

namespace _77_CustomActionFilters.Controllers
{
    /*
      4 farklı türde ACTION FILDER var.
      1. AUTHORIZATION FILTERS: Bu tür FILTER'lar IAUTHORIZATIONFILTER INTERFACE'i uygular ve diğer FILTER'lardan önce çalışır. Örnek Authorize ve ReuiredHttps
        Bu INTERFACE'de kullanabileceğimiz OnAuthorization() methodu var. Parametre olarak AuthorizatıonContext CLASS örneği istiyor.

      2. ACTION FILTERS: Bu türler IACTIONFILTER INTERFACE'i uygular.
         BU INTERFACE'de 2 method var 
            1. ONACTIONEXECUTED(): ACTION çalıştırıldıktan hemen sonra çalışır.
            2. ONACTIONEXECUTING(): ACTION çalıştırılmadan hemen önce çalışır.
     
     3. RESULT FILTERS: Bu türler IRESULTFILTER INTERFACE'i uygular. Örnek OUTPUTCACHEATTRİBUTE
        Bu INTERFACE'de de üsteki ile aynı mantıkta 2 method var.  Sonuç dönmeden önce çalışan ve dönükten sonra çalışan.
     
     4. EXCEPTION FILTERS - Bu türler IEXCEPTİONFİLTER INTERFACE'i uygular. Örnek HANDLEERRORATTRİBUTE
        INTERFACE'nin ONEXCEPTION() methodu var.
      
     Not: ACTIONFILTERATTRIBUTE CLASS'ı 2. ve 3. türleri kapsar.
     Kendi FILTER'imizi yapmak için bir CLASS oluşturduk.
     Bir CLASS oluşturduk.
     Bir method oluşturup, hataları olayları bir txt dosyasına kaydedeceğiz.
     Bunun için FILE sınıfının AppendAllText() methodunu kullanıyoruz 
        1. parametre olarak dosyanın aranacağı/yoksa oluşturulacağı yol 2. parametre olarak yazılacak içerik.
        (Bizim durumumuzda method dosya SERVER'de ulaşacağı için HTTPCONTEXT CLASS'ını kullanıyoruz.)
     
     OnActionExecuting() methodnuda çalıştırılacak ACTION METHOD'u falan bu dosyaya yazdıracağız. Bunun için CONTROLLER'E falan ulaşmamız gerekiyor.
     Parametre olarak kullanılan CLASS örneğinden bunu yapabiliriz.
      Örneğin  ActionDescriptor/ControllerDescriptor, ActionName/ControllerName  PROPERTY'leri var. 
      En dışda ActionDescriptor bundan sonra ACTIONNAME ile adı alabilir yada ControllerDescriptor ile CONTROLLER'e geçebiliriz.
     
     OnResultExecuting(): methoddan çalıştırılacak ACTION METHOD'a veya CONTROLLER'e ulaşacak PROPERTY'lerimizi yok. 
        Çünkü burada sadece çalışma sonucu ile ilgili bilgiler var. Tamamen bağımzı bir yer.
        Burdan isimlere ulşabilmek için ROUTING CLASS'ının ROUTEDATE PROPERTY'sini kullanıyoruz. 
        VALUES INDEX'i le URL belirlerken kullanığımız isimler ile CONTROLLER veya ACTION METHOD ismine ulaşabiliriz.
        (App_Start klasöründeki dosyadan daha iyi anlaşılabilir.)
        Kısaca ROUTEDATA PROPERTY'sine VALUES uyguluyoruz. 
        Parametre olarak URL'deki bir bölüm adı yazıyoruz. 
        URL ve bölümlerini kendimiz belirleseydik. Bölüm adı olarak istediğimizi verebilirdir.  Fakat CLASS hazır oluduğu için varsayılan isim.
      
     6 method var 5. aynı isi farklı şekilde yapıyor. 1. de mesajları yazdırmak için
     */
    public class HomeController : Controller
    {
        [TrackExecutionTime]
        public string Index(){return "Index Action Invoked";}
        [TrackExecutionTime]
        public string Welcome(){throw new Exception("Exception ocuured");}

    }
}
