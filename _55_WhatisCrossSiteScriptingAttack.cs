using System.Data;
using System.Linq;
using System.Web.Mvc;
using _55_WhatisCrossSiteScriptingAttack.Models;

namespace _55_WhatisCrossSiteScriptingAttack.Controllers
{
    /*
      Kullanıcının SERVER'e gönderdiği SCRIPT'in Server'dan çalıştırılabilir kod olarak çıkmasıdır. Server'a alınan Script Server'dan Script'in çalıştırılabilir bir ortama gönderilirse, kullanıcı bu ortamda istediği işi yapma yetkisi kazanır. Bu yüzden ya kullanıcının Server'a Script göndermesini engellemeliyiz yada Script'in Server'dan çalıştırılabileceği bir şekilde çıkmasını engellemeliyiz yada ikisini birlikte yapamlıyız.
      
      Varsayılan olarak Server'a Script/Html tag'ı gönderilemez. Kullanıcı Script göndermeye çalışırsa "A potentially dangerous Request.Form value was detected form the client(.....)" hatası alır.
      ValidateInput ATTRIBUTE'u Server'a postalanan verinin onaylama işlemini gerçekleştirir. Attribute'u bir PostAction methoduna false değeri vererek uygularsak, onaylama işlemi yapılmayacağı için Server'a Script gönderilebilir. Fakat varsayılan olarak Server'dan çıkan string değerler kodlandığı için Script'in çalıştırılabilir ortama gönderilse bile çalıştırılamaz. Yani Script'in Server'den kodlanmamış bir şekilde çıkmasını sağlarsak Script gönderildiği yerde çalışabilir.

      Kullanıcı Script'ini veritabanına kaydettiğinde ve başka bir kullanıcı bu tablodaki veriyi çektiğinde Script kullanıcnın bilgisayarında çalışır. Bu sayede Script'i gönderen kişi tüm kullanıcı Session verilerini alabilir. Böylece şifre girme gereği olmadan kullanıcının hesablarını kullanabilir.
     */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(db.Comments.ToList()); }
        public ActionResult Details(int id = 0) { Comment c = db.Comments.Single(x => x.Id == id); if (c == null) { return HttpNotFound(); } return View(c); }
        public ActionResult Create() { return View(); }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Comment comment)
        { if (ModelState.IsValid) { db.Comments.AddObject(comment); db.SaveChanges(); return RedirectToAction("Index"); } return View(comment); }
        public ActionResult Edit(int id = 0) { Comment c = db.Comments.Single(x => x.Id == id); if (c == null) { return HttpNotFound(); } return View(c); }
        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Attach(comment);
                db.ObjectStateManager.ChangeObjectState(comment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
        public ActionResult Delete(int id = 0) { Comment c = db.Comments.Single(x => x.Id == id); if (c == null) { return HttpNotFound(); } return View(c); }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Single(c => c.Id == id);
            db.Comments.DeleteObject(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}