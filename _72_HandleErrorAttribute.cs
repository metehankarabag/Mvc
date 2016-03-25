using System;
using System.Web.Mvc;

namespace _72_HandleErrorAttribute.Controllers
{
    /*
      Uygulamanın çalışma anında bir hata olursa, tarayıcıda Yellow Screen of Dead penceresi görünür. HandleError Attirbute uygulamanın Action methodlarında bir hata gerçekleştiğinde tarayıcıya uygulamadaki Error View göstermemizi sağlar. HandleError Attribute'unun 3 farklı kullanım seviyesi var. (Application -> Controller -> Action Method)
      
      Asp.net'de de yaptığımız gibi Application düzeyinde gerçerli Custom Error ayarları Global.asax dosyasında belirlenir. Yani HandleError'u Globel.asax dosyasında uygulamalıyız. FilterConfig Class'ının RegisterGlobalFilters() static methodu HandleError Attribute'unu kullandığımız methoddur. Global.asax dosyasında Methoda parametre olarak GlobalFilterCollection nesnesi vermemiz gerekiyor. GlobalFilter Class'ının GlobalFilterCollection türündeki Filters Read-Only Propertisini veriyoruz. RegisterGlobalFilters() Class'ında yaptığımız için alınan filitre örneğine yeni birini eklemek. GlobalFilterCollection Class'ının Add() methodu object türünde bir parametre alır ve parametre olarak HandleErrorAttribute Class'ının bir örneğini veriyoruz. Yani FilterConfig Class'ındaki methodu kullanarak tüm filitreleri uygulama düzeyinde yapabiliriz.

      1. Not: Çalıştırılacak View ya hatanın gerçekleştiği Controllerin klasöründe yada Shared folder içinde olmalıdır.
      2. Not: Uygulamanın Custom Error sayfasını kullanabilmesi için CustomError'u web.congif'de açmamız gerekir.  -> <system.web> -> <customErrors mode = "On">. CustomError modu açıkken, hata sonucu ortaya çıkacak Yellow Screen of Dead ekranı tarayıcıda görünmez. RegisterGlobalFilter() methodundan HandleError Attribute'unu silersek, bir hata olduğunda Error View da görüntülenmez. Bu durumda programda bir hata olursa Yellow Screen of Dead ekranında hataya neden olan kodlar yerinde Web.Config dosyasındaki ayarlar görünür. Çünkü son hatanın nedeni hata için CustomError kullanacağımı söylememiz fakat kullanmamız.
      3. Not: HandleError Attribute'u Action methodlardaki hatayı gösterir fakat kullanıcı yanlış Url yazarsa, yani uygulamadan kaynaklanmayan bir hata olursa, bunu etkilemez. Bu yüzden Web.Config'de customErrors içinde Error eklemeliyiz. Bu hata hata koduna göre bir Controller'daki Action methodu çalıştrır ve Action method View'ı tarayıcıya yazdırır.(hata.aspx mantığı ile aynı ) -> <error statusCode="404" redirect="~/Error/NotFound"/>
     
     */
    public class HomeController : Controller { public ActionResult Index() { throw new Exception("Someting went wrong"); } }
}
