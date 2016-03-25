using System.Web.Mvc;
using _41_UsingDatatypeAndDisplayColumnAttributes.Models;

namespace _41_UsingDatatypeAndDisplayColumnAttributes.Controllers
{
    /*
     DataTypeAttribute: Parametre olarak DataType Enum'undan bir tür alır ve Property verisinin türünü belirler. Bu Attribute'u Property'i göstermek için oluşturulacak Html Tag'ını etkileyebilir. Parametre olarak DataType.Url verdiğimizde veri a Tag'ı içinde gösterilir.       
      DataType.Currency mitarını sonuna varsayılan ülke parambirimi işareti eklenir.
      Web.Config dosyasında belirli bir ülkenin para birimi sembolunu kullanmak istiyorsak <globalization culture="en-gb"/> Attribute'unu kullanabiliriz.
      Not: DisplayTextFor() Helper'ı Model içindeki Complex Type'ları göstermek için kullanılır. Diplay1 View'ın da kullanılmış Model olarak Company örneği alıyor fakat Class'ın Employee türündeki Property'ini tarayıcıya gösteriyor. Varsayılan olarak bütün Property'leri göstermez Sadece Class Seviyesinde uygulanmış [DisplayColumn("FullName")] Attibute'u ile belirlenmış Sütun değerlerini gösterir. Bu Helper Complex Type Property'lerine uygulanmış Attribute'lardan etkilenmiyor.
     */
    /* 42 Opening a Page In New Browser Window
      View klasörü içinde Editor ve Display templates klasörü açıp içine adı bir veri türü olan View'lar açtığımızda bu veri türünü kullanan Property'lerin tarayıcıdaki ayarlarını belirleyebiliyoruz. -> kullanılacak Tag örneğin. Bir Property'e Datatype Attribute'u uygulayıp parametre olarak DataType.Url verdiğimizde Property türü Url olur. Bu klasör içinde Url View'ı oluşturup oluşturulacak <a> Tag'ını ayarlayabiliriz. Fakat tüm Url türleri aynı View'ı kullanır yani aynı ayarla çalışır. Bunu düzeltmek için UIHint Attribute'unu kullanabiliriz Parametre olarak kullanmayı istediğimiz View adını vermeliyiz.
    */
    public class HomeController : Controller
    {
        public ActionResult Details(int id) { Company company = new Company(); return View(company); }
    }
}
