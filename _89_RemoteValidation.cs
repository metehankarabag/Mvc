using System.Data;
using System.Linq;
using System.Web.Mvc;
using _89_RemoteValidation.Models;


namespace _89_RemoteValidation.Controllers
{
    /*
      RemoteAttribute, ValidationAttribute Class'ından ve IClientValidatable Interface'inden türemiş bir Class'dır ve bir Action method çalıştırmamızı sağlayan 4 Constructor'ı var. Yani Attribute'un uygulandığı Property Client Side'da her kullanıldığında belirlenen Action methodu sayfadan bağımsız bir şekilde tetikler. Property'i tarayıcıda her kullanıldığında Server'a Get Request yapar. Action Method çalışır ve sonucu tarayıcıda çalışmakta olan View'a gönderilir. Yani Action Method tarayıcıda yeni bir View göstermez çalışmada olan View'a değer gönderir. Action methodun çalışmakta olan tarayıcıya değer gönderebilmesi için dönüş türünün JsonResult olması gerekir. JsonResult Action method verisinin JavaScript Object Notation(Json) formatına çevirilmesi ile oluşur. Veri tarayıcıya gönderildikten sonra validation bu değeri kullanılarak tarayıcıda çalıştırılır.
      
      Json() methodunun 6 overload'ı var. Parametrelerini anlamadım fakat 2. overload'ını kullanıyoruz. 1. parametre JavaScript'a çevirilip gönderilecek veridir, 2. parametre JsonRequestBehavior Enumun'dan bir değer alır ve 2 değeri var( AllowGet/DenyGet). Bu değerler JsonResult dönen Action Methodun GetRequest ile çalıştırılıp çalıştırılamayacağını belirler. 
      1. Not: DenyGet değeri verilmişse ve Property Action'ı tetiklerse, sayfa donar.
      2. Not: Json() method parametre olarak aldığı veriyi hiç bir kontrole sokmadan JavaScript Object Notation(Json) formatına çevirerek tarayıcıya gönderdiği için Get Request ile çalışan bir Action metodun JsonResult dönmesi tehlikelidir. Çünkü tarayıcıya gönderilecek veri tabanından gelir. Post Request ile çalışan bir Action'ın dönüş türü olarak JsonResult kullanabiliriz. Çünkü kullanıcıdan alınan veri geçerli sayfaya tekrar yazdırılır. Bu yüzden sorun yok.
      
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
        { if (ModelState.IsValid) { db.Users.AddObject(user); db.SaveChanges(); return RedirectToAction("Index"); } return View(user); }

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