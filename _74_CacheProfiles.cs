using System;
using System.Linq;
using System.Web.Mvc;
using _74_CacheProfiles.Common;
using _74_CacheProfiles.Models;

namespace _74_CacheProfiles.Controllers
{
    /*
      Geçen derste OutputCache ayarlarını kodda belirtmiştik bunun dezavantajları var. Bunlardan kurtulmak için ayarları Web.Config'de CacheProfile oluşturarak belirlemek gerekir.
      <system.web> -> <caching> -> <outputCacheSettings> -> <outputCacheProfiles> -> <clear/> + <add name="1MinuteCache" duration="60" varyByParam="none"/> 
      
      OutputCache Attribute'unun CacheProfile Property'sine parametre olarak oluşturduğumuz CacheProfile'ın adını verdiğimizde, Attribute bu profildeki ayarları kullanır. Fakat Profile'daki Duration değeri alınamıyor. Bu yüzden Attribute'un uygulandığı Action method çalıştırıldığında -> Duration must be positive number hatası alıyoruz. Bu sorundan kurtulmak için Custom Attribute Class'ı oluşturmak gerekir. Attribute Class'ı oluşturmak için bir Attribute Class'ından türetip, kullanıcının Class'a gönderdiği değerleri Base Attribute olarak kullandığımız Class'ın Property'lerine atmakmız gerekir. Kullanıcı Class'a sadece Cache Profile adını gönderecek, Class içinde bu adı kullanarak Web.Config dosyasındaki Cache Profile'a ulaşıp tüm değerlerini Base Attribute Class'ına gireceğiz.
      
      Config dosyasından veri çekmek için WebConfigurationManager Static Class'ının GetSection() Object dönen Static Method'una parametre olarak Web.Config dosyası içindeki bölüme giden yolu veriyoruz. aldığımız sonucu OutputCacheSettingSection'a Cast edip bu Class'ın bir örneğini oluşturuyoruz. OutputCacheSettingSection Class'ındaki Property'leri alıp Base Classs'ın Property'lerine atarak işi bitiriyoruz.
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        [PartialCache("1MinuteCache")]
        public ActionResult Index() { System.Threading.Thread.Sleep(3000); return View(db.Employees.ToList()); }
        [OutputCache(CacheProfile = "1MinuteCache")]
        public string GetEmployeeCount() { return "Employee Count = " + db.Employees.Count().ToString() + "@ " + DateTime.Now.ToString(); }

        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}