using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;

namespace _17_EditingaModel.Controllers
{
	/*
      12. Derste otomatik oluşturulan Create View'ı DropDownList() kullanarak düzenledik. Aynı işi 17. Derste otomatik oluşturulan Edit View için yaptık. View'lar arasındaki tek fark Edit View'da HiddenFOr() ile Employee'nin Id değerini Server'a göndermemizdir. Çünkü bu Id'e göre güncelleme gerçekleştirilecek. 
      
      13,14,15 ve 16 derslerde Create Post Action methodunun oluşturduk. Her derste method parametreleri farklı şekillerde alınıyor. 16. derste UpdateModel<T>() ve TryUpdateModel<t>() methodlarının ModelBinder'a bağlanmasını istemediğimiz Property'ler varsa kullanışlıdır demiştik. 19 dan 22. derse kadar ModelBinder'ın Property'lere bağlanmasını farklı şekillerle engelleyeceğiz.
     */
    /*18. Ders -> Edit Post methodunu oluşturduk.
      Herhangi bir özel durum yok güncellenecek veriyi Id değeri ile alıp tüm değerlerini güncelliyoruz.
      
      Derste ModelState'i Immadiate Windows'da çalıştırmış. -> Ctrl+D+I. Uygulamayı Debug Modda çalıştırıp Break Point ile durdurduğumuz yere kadar gerçekleşen tüm işlemleri Immediate Windows'u açıp değişken,property, method'ları kullanarak değerlerini görebilir veya test için değişitirebiliriz.
     
      ModelState Controller Class'ının ModelStateDictionary türündeki Read-Only Property'sidir. ModelStateDictionary Class'ın IEnumerable<T,TResult> gibi Interface'lerden türeyen bir Class'dır.Interface'lerde Generic Type olarak ModelState kullanılıyor ve Class direk Interface'lerden türediği için Class içinde uygulama yazmak zorundayız. Interface'lerin uygulamasını yazdığımız Add() methodlarını kullanarak Generic Type olarak belirlenmiş türe ait nesneleri ModelStateDictionary örneğine ekleyebiliriz. Yani ModelStateDictionary birden fazla alt nesnesi olan bir Class'dır. Böyle Class'larda alt Type'lara ulaşabilmemiz için en az bir Index gerekir ve string parametre isteyen ModelState türünde Read-Only bir index var. ModelState Property'sinin türü ModelStateDictionary olduğu için Index uygulayabiliriz. Yani ModelState Property'si içinde birden fazla ModelState nesnesi barındırabilen bir Property'dir.
      
      ModelState Class'ının 1. Read-Only Errors 2. Value Property'leri ve parametresiz bir Constuctor'ı var. Contructor'u parametresiz bir hata eklemek için Property'ler kullanırız. Controller Class'ının ModelState'e Property'si ModelState nesnesi döndüğü için bu Property'leri uygulayarak hata ile ilgili işlemler veya Model değerleri üzerinde çalışabiliriz.
      
      1. Errors: ModelState Property'sinden aldığımız nesneye hatalar eklemek/hata değerlerini almak, vs.. için kullanılır. Error Property'sinin türü ModelErrorCollection'dır. Bu Class'da sadece hata eklemek için kullanabileceğimiz 2 Add() methodu var. Yani bu Class'dan veri alamayız. Fakat Class Collection<ModelError>'den türüyor. Yani ModelErrorCollection Class'ı da alt nesneleri olan bir Class. Ayrıca Class içinde bir Index de yok. Fakat Class direk Interface'den türemediği için uygulama barındırmak zorunda değil. Varsayılan uygulamayı belirleyen Base Class içinde Generic Type türünde int parametre bekleyen bir tane Index var ve Generic Type olarak ModelError belirlenmiş. Bu Class içindeki Property'leri kullanarak hata bilgilerini alabiliriz.
      2. Value: ModelState Property'sinden aldığımız ModelState nesnesinin değelerini barındırır/verir. Value Property'sinin türü ValueProviderResult Property'sidir. ValueProviderResult Class'ında Protected Set kullanan 3 Property ve biri parametresiz diğeri 3 parametre bekleyen 2 Contructor var. Constructor parametrelerine kullanılan Field'lar ile Property'ler aynı. Property'ler -> string AttemtedValue, CultureInfo Culture,object RawValue 
            1. AttemtedValue: ModelState'ı hataya düşüren değeri verir.
            2. Culture: değerin nerenin düzenine göre ayarlandığı.
            3. RawValue: Ham veriyi verir.
     */

    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();
            return View(employees);
        }
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get() { return View(); }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(Employee employee)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            if (ModelState.IsValid) { employeeBusinessLayer.AddEmmployee(employee); return RedirectToAction("Index"); } else return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.ID == id);
            return View(employee);
        }
        
    }
}