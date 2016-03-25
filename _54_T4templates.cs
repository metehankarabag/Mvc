using System.Linq;
using System.Web.Mvc;
using _54_T4templates.Models;

namespace _54_T4templates.Controllers
{
    /*
      T4 TEMPLATE: Bir Projeye eklediğimiz VIEW veya Controller'in varsayılan kodunu oluşturmak için Visual Studio kullanılan tt uzantılı dosyadır.
      31. derste spark View engine'i eklemiştik. Bu sadece VIEW için çalışan bir T4 template sanırsam . O derse View Engine olarak işledik bu derste T4 template olarak işliyoruz.
      
      T4 template'ler C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\ItemTemplates\CSharp\Web\MVC 4\CodeTemplates klasörü içinde bulunur. Bu klasör içinde bulunan tt tüm dosyaları düzenleyebilir veya yenilerini oluşturabiliriz. Buradaki dosyaları değiştirilirse visual Studio'nun varsalıyan kod oluşturma düzeni değişir. Yani oluşturulan her View veya Controller buradaki düzene göre oluşturulur. Değişikliklerin sadece bir uygulamada için geçerli olmasını istiyorsak, CodeTemplates klasörünü uygulamanın ROOT klasörüne kopyalayıp değişiklikleri buradaki dosyalarda yapmalıyız. Değişiklikleri yapıp VIEW oluşturduğumuzda Vistual Studio uygulamaya içinde belirlenen tt dosyalarını kullanır ve diğer uygulamaların kullandığı tt'ler etkilenmez.
      
      Not: Klasörü uygulamaya eklerken, değişiklikleri kayederken, Vistual Studio dosyaları çalıştırmak isteyecek. - CANCEL'a tıkla. Sorunu kökten çözmek için tt dosyalarının PROPERTY'isindeki Custom Tool değerini sil.
     */
    public class HomeController : Controller { public ActionResult Index() { SampleDBContext d = new SampleDBContext(); return View(d.Employees.ToList()); } } 
}
