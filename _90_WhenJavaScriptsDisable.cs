using System.Data;
using System.Linq;
using System.Web.Mvc;
using _90_WhenJavaScriptsDisable.Models;


namespace _90_WhenJavaScriptsDisable.Controllers
{
    /*
      Remote Attribute parametre olarak aldığı methodu Client'dan tetiklemek için Axaj kullanır. Tarayıcının Axaj kullanarak Action methodu çalıştırabilmesi için JavaScript kullanması gerekir ve tarayıcıda javaScript kapatılmışssa Axaj'ı kullanmaaz. Bu yüzden Server Side Validation uygulamamız gerekir. Oluşturacağımız Validation, Server'a Post'alanan değeri kontrol edip, istenmeyen durumlarda ModelState'i False'a düşürmelidir. Bu işi 2 şekilde yapabiliriz.
      1. Post işleminin çalıştırdığı Action Method içinde değeri kontrol edip gerektiğinde Controller Classının ModelState'ine False değeri vereceğiz. Property'nin türü ModelStateDictionary'dir ve bu Class'ın üyesi olan AddModelError() methodu ModelState'e hata eklemek için kullanılır. AddModelError() methodunun 2 overload'ı var. 1. hataya neden olacak Property'nin adı 2. parametre hata mesajı veya bir Exception nesnesi 
      
      Mvc'nin yapısı gereği ModelState'i ile ilgili ayarları Controller'da yapmamız gerekir. Çünkü Model'a gelen veriyi onaylamak ve ModelState'i belirlemek için Model Class'larda Attribute'ları kullanılır. ModelState'i belirlemek Attribute'ların işidir. Bu yüzden Action Method'da yapıdığımız işi Custom Attribute oluşturarak yapmamız gerekir. bu da 2. yoldur. Sonraki derste bunu yapacağız.
      
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
            if (db.Users.Any(x => x.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "User Name is already in use");
            }
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