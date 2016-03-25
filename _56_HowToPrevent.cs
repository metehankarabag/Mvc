using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _56_HowToPrevent.Models;
using System.Text;

namespace _56_HowToPrevent.Controllers
{
    /*
      Kullanıcı tarayıcısında sadece belirlediğimiz Html Tag'larını çalıştırabilsin istiyoruz. Tag'ın tarayıcıda çalışması için Server'den kodlanmamış string olarak çıkması gerekir. Server'a gelen veriler kodlanmadığı için gelen veriyi olduğu gibi kodlanmamış string olarak çıkartırsak Script'ler de tarayıcıda çalışabilir. Bu yüzden Post Action methoda gelen tüm veriyi kodlayıp bazılarını kodlanmamış string olarak düzenlemek gerekir. Böylece kodlanmış veri VIEW'a orjinal veri muamelesi görür.
      
      HttpUtility CLASS'ının HtmlEnCode() Static methodu parametre olarak aldığı STRING'ı kodlanmış STRING'e çevirir. Kodlanan sting'i bir Filed'a aldıktan sonra field içindeki kodlanmış veriyi Replace() methodu ile ACSII karakterlerine çevirebiliriz. Böylece bir kısmı kodlanmış bir kısmı kodlanmamış string'i kodlanmamış string olarak VIEW'dan çıkartabiliriz.
      
      Not: String sürekli değişeceği için kodlanmış veriyi StringBuilder'da tutuyoruz. 
      
      Bu güvenlik açığının bir türü bu bir sürü farklı tür daha var MSDN den oku diyor.
      Not: Şifreleme alfabetik ve sayısal karakterlere yapılmıyor. Şifreleme her zaman sembol olarak kullanılan karakterlere uygulanıyor.
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
        {
            StringBuilder sbComments = new StringBuilder();
            sbComments.Append(HttpUtility.HtmlEncode(comment.Comments));
            sbComments.Replace("&lt;b&gt;", "<b>");
            sbComments.Replace("&lt;/b&gt;", "</b>");
            sbComments.Replace("&lt;u&gt;", "<u>");
            sbComments.Replace("&lt;/u&gt;", "</u>");
            comment.Comments = sbComments.ToString();

            StringBuilder sbName = new StringBuilder();
            sbName.Append(HttpUtility.HtmlEncode(comment.Name));
            sbName.Replace("&lt;b&gt;", "<b>");
            sbName.Replace("&lt;/b&gt;", "</b>");
            sbName.Replace("&lt;u&gt;", "<u>");
            sbName.Replace("&lt;/u&gt;", "</u>");
            comment.Name = sbName.ToString();

            if (ModelState.IsValid) { db.Comments.AddObject(comment); db.SaveChanges(); return RedirectToAction("Index"); }
            return View(comment);


        }
        public ActionResult Edit(int id = 0){ Comment c = db.Comments.Single(x => x.Id == id); if (c == null) { return HttpNotFound(); } return View(c); }

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