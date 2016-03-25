using System.Web.Mvc;

namespace _67_ActionSelectors.Controllers
{
    /*
      ACTION SELECTOR ACTION METOHD'lara uygulanan ATTRIBUTE'lerdir. Hangi Actio methodun çalıştırılacağını kontrol etmek veya etkilemek için kullanılır.
      Çok kullanılanları
      ActionName selector: ACTION methodu kendi adından farklı bir adla çalışmasını sağlar. Yani çalışma anında bu methodun adı değişir. Bu Action method dönüşte View() methodunu kullanıyorsa ve methodda parametre olarak çalıştırılacak view'ın adı belirtilmemişse, varsayılan olarak Method adında bir View açlıştırılır. View çalışma zamanında arandığı için ve methodun çalışma zamanında adığı değişti için Attribute'daki değer geçerlidir. 
      AcceptVerbs selector: kullanıcının Server'a yaptığı isteğin türünde göre Action methodun çalışabilirliğinin belirlenmesini sağlar. Varsayılanı Get Request'dir. AcceptVeras Attribute'unun Contructor'ı parametre olarak HttpVerps Enumu alır. Bu Enumdaki üyeler Action methodun çalışabileceği istek türünü belirler.
     */
    /*68. ders
      NONACTION ATTRIBUTE CONTROLLER içindeki bir methodun ACTION olma özelliğini engelleyen ATTRIBUTE'dir.
      Yani kullanıcı URL'den METHOD'u çalıştıramaz.
      Aynı işe CONTROLLER'deki methodu PRIVATE yaparakda ulaşabiliriz.
      İş yapmak için oluşturduğumuz methodları MODEL'de saklamalıyız. Zorunda kalmadıkca bunu kullanmak kötüdür.
     */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
