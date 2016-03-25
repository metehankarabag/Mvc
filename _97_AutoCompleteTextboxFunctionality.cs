using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _97_AutoCompleteTextboxFunctionality.Models;

namespace _97_AutoCompleteTextboxFunctionality.Controllers
{
    /*
      Kullanıcının TextBox'a girdiği değere göre veritabanından değer alacağız. Bunu için bir Select sorgusu kullanırız. Bir sütundaki değere göre veri almayı istiyorsak sorguya Where ekleriz ve sütundaki değeri tam olarak girmesekte koşulun doğru olabilmesi için = yerine like kullanırız. Derste bu işi ado.net kullanarak yapılmamış Linq kullanarak yapılmış
      Aynı işi Linq ile yapmak için Where() methodu içinde String Class'ının Instance Methodu olan ve string parametre bekleyen StartsWith() methodunu kullanıyoruz ve parametre olarak kullanıcın girdiği değeri veriyoruz. Methodun parametre olarak aldığı değer uygulandığı String'ın başlangıc değerlerine uyuyorsa method true döner ve  where methodu true dönen satırları bir IQueryable<T> örneği içinde döner.
      Veritabanından aldığımız sonuçları 2 farklı iş işin kullanılacak.
      1. sonuçlar ile View'da gösterilecek 
       BeginForm() içinde TextBox ve input nesnesi var. input onaylandığında TextBox içindeki değeri Server'da kullanabiliyoruz. Burada dikkatimi çeken BegionForm() methodunda çalıştırılacak Metod adı belirlenmemiş. varsayılan olan Index anlamına geliyor bu sanırım istek türüde Post olduğu için Post Index Action çalışıyor.
     
      2. sonuçlar textBox'ın altındaki bir açılır pencerede gösterilecek
      Bu iş için JavaScript kullanmak zorundayız. Çünkü TextBox'a bir değer girer girmez değer Server'a postalanmalı ve Server'dan gelen cevap ile Textbox'ın altında açılır bir pencere oluşturulmalı. Bu iş için hazır JavaScript'ler kullanacağız.
      
      http://jqueryui.com/download/ Bu site'de tüm UI WİGGET'ler var. Burdan AutoComplate'i indiriyoruz. İndirilen dosyadan javaScript, css dosyaları ve image klasörünü programa atıyoruz.(hepsini değil.) Bu kütüphandeki methodun nasıl kullanılacağına dair hiç bir bilgimiz yok. Bu yüzden sitedeki Api Documentation sayfasına giriyoruz. -> soldaki listeden -> Wiget'e tıklayıp -> AutoComplate'i seçiyoruz. Bu sayfada indirdiğimiz JavaScirpt dosyasındaki methodun nasıl kullanılabileceğine dair bütün ayrıntılar var.
      
      autocomplate() methodu bir input'a uygulanacak. Input'un tarayıcıda bulunabilmesi için TextBox()'da Input'a id özelliği vermeliyiz. Bu Input nesnesine değer girildiğinde method otomatik olarak tetiklenir. Method'un source Property'si bir liste istiyor. Bu liste üyeleri methodun uygulandığı Input'un altında görünecek açılır pencerenin üyeleri olacak. Bu değerlerin veritabanından gelmesini istediğimiz için parametre olarak Action() Helper'ı veriyoruz. Action() helper'e de parametre olarak çalıştırılacak ActionMethod'un adını veriyoruz. Method string parametre beklemeli ve parametre adı "term" olmalı çünkü değeri gönderen javaScript bu isimle gönderiyor. Ayrıca Action Methodun dönüş türü JsonResult olmalı çünkü alınan değer javaScript tarayından kullanılacak. JsonResult ile veritabanından veri göndermek tehlikelidir demiştik. Bu yüzden veri tabanından sadece Name özelliklerini çekiyoruz.
      minLength: özelliği de kaç karakter sonra metodun çalıştırılacağını belirler.
      
      Not: Students Property'si ilk önce veri tabanından tüm verileri alıyor alınan verilere Where() methodunu uyguluyoruz. Yani ihtiyacımız olmamasına rağmen her seferinde veri tabanından istemediğimiz verileri çekiyoruz.
    */

    /* 98 What is JavaScript Minification
      http://jscompress.com sitesinde scripti oraya yaz ve boşlukları silcek idleri küçültecek dosya boyutunu düşürecek buda min java dosyası oluyor.
      Web sitede daha hızlı yüklenmesi ve zor okunması için min kullanılır bir problem oldu ve debug yapamıyorsak min olmayanı kullanıp problemi düzeltip tekrar min oluşturup kullanabiliriz.
     */
    public class HomeController : Controller
    {
        public ActionResult Index(){SampleDBContext db = new SampleDBContext();return View(db.Students);
        }

        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            SampleDBContext db = new SampleDBContext();
            List<Student> students;
            if (string.IsNullOrEmpty(searchTerm))students = db.Students.ToList();
            else students = db.Students.Where(s => s.Name.StartsWith(searchTerm)).ToList();
            
            return View(students);
        }

        public JsonResult GetStudents(string term)
        {
            SampleDBContext db = new SampleDBContext();
            List<string> students = db.Students.Where(s => s.Name.StartsWith(term)).Select(x => x.Name).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}
