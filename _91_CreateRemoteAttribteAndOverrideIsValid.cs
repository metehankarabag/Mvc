using System.Data;
using System.Linq;
using System.Web.Mvc;
using _91_CreateRemoteAttribteAndOverrideIsValid.Models;


namespace _91_CreateRemoteAttribteAndOverrideIsValid.Controllers
{
    /*Common klasörü içinde RemoteAttribute Class'ından türeyen bir Class oluşturarak, IsValid() methodunda ModelState'i belirleyeceğiz. RemoteAttribute'u Base Class olarak kullanmamısın nedeni diğer işlerin hiç birine dokunmayı istemediğimizden. Tabi oluşturduğumuz Attribute'a bir den fazla kullanım şekli vermek için Constructor'ına overload'lar eklemek gerekiyor. Bu overload'larda yaptığımız tek şey değerleri alıp Base Contructor'ları çalıştırmak.
      
      Attribute'u uyguladığı Property tarayıcıda çalıştırıldığında kullanıcının hangi Controller içindeki hangi Action Methodun çalıştırmayı istediğini bilmediğimiz için Reflection kullanacağız. Kullanıcının Attribute Constructor'ına girdiği Action ve Controller değerleri. RemoteAttribute Class'ının RouteValueDictionary türündeki Read-Only RouteData Property'sinde tutulur. Bu Class'da string parametre alan ve object dönen Read-Only Index olduğu için Property'i index key değeri vererek kullanabiliriz. key olarak action verirsek method adını controller verirsek controller adını alırız.
      Şimdi yapmamız gereken 2 şey var. 1. Reflection ile uygulama Assmbly'si içindeki tüm type'ları alıp FirstOrDefult() methodu ile kullanıcının kullanmayı istediği Controlleri'i bulmak. Kullanıcının belirlediği Controller Assembly'de olmayabilir bu yüzden if kullanıyoruz. Controller varsa aynı işi Controller içinde Action Methodu bulmak için yapıyoruz.
      Method varsa, Controllerin bir örneğini oluşturup Methodu çalıştırmamız gerekiyor. Methoda tarayıcıdan Property'nin değeri gönderilecek, bu değer isValid() methodunun birinci parametresinde parametre ile Controller örneğini kullanarak methodu çalıştırıyoruz. 
       ModelState'i False olarak düşürecek 2 durum belirlemiş. Çalıştırılan methodun türü JsonResult'sa veya methoddan çıkan sonuç boolen'sa, boolen değere göre ModelState hata alacak veya başarılı olacak. Başarılı olması için ValidationResult Class'ının Static Read-Only ValidationResult türündeki Success field'ını kullanacağız. Hata vermesi için ValidationResult Class'ının Constructor'ına kullanıcının belirlediği hata mesajını vererek hata oluşturacağız.
      
     Kullanılan hiç bir if'in else'i olmadığı if koşullarından biri gerçekleşmesse en alttaki varsayılan değer döner.
      
     Not: oluşturduğumuz Attribute'a parametre olarak Delegate belirler ve delegate'a ait olan Method.Name Property'leri ile base Class Contructor'ına method adını verirsek Controller ve Action method adını almaktan kurdulabiliriz.(Attribute'a parametre olarak Static olamayan bir method veremiyoruz, modelbinder ile veriyi alamıyoruz. Mvc'nin çalışma yapısını bozuyor. Yani bazen string almak gerekir.)
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(db.Users.ToList()); }
        public JsonResult IsUserNameAvailable(string UserName) { return Json(!db.Users.Any(x => x.UserName == UserName), JsonRequestBehavior.AllowGet); }
        public ActionResult Details(int id = 0) { User user = db.Users.Single(u => u.Id == id); if (user == null) return HttpNotFound(); return View(user); }
        public ActionResult Create() { return View(); }
        [HttpPost]
        public ActionResult Create(User user)
        {
            //if (db.Users.Any(x => x.UserName == user.UserName))
            //{
            //    ModelState.AddModelError("UserName", "User Name is already in use");
            //}
            if (ModelState.IsValid)
            {
                db.Users.AddObject(user);
                db.SaveChanges(); return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Edit(int id = 0) { User user = db.Users.Single(u => u.Id == id); if (user == null)return HttpNotFound(); return View(user); }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(user);
                db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int id = 0) { User user = db.Users.Single(u => u.Id == id); if (user == null)return HttpNotFound(); return View(user); }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        { User user = db.Users.Single(u => u.Id == id); db.Users.DeleteObject(user); db.SaveChanges(); return RedirectToAction("Index"); }

        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}