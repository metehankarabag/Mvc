using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using _73_OutputCacheAttribute.Models;

namespace _73_OutputCacheAttribute.Controllers
{
   /*
      OutputCache ATTRIBUTE: Bir Action Method'ın View'a veya direk tarayıcıya gönderdiği değerleri ön bellekte sağlar. qView'da Action ile ChildAction methodu çağarır ve bu methoda OutPutCache Attribute'unu uygularsak sadece ChildAction'ın View'a eklediği kısım ön belleğe alınacağı için Partial Cache uygulamış oluruz.
      PARTIAL CACHING'i uygulamak için CHİLDACTİON ATTRIBUTE uygulanmış ACTION METHOD'lar kullanılabilir.
    */
    public class HomeController : Controller
    {
        private SampleDBContext db = new SampleDBContext();

        [OutputCache(Duration = 10)]
        public ActionResult Index() { System.Threading.Thread.Sleep(3000); return View(db.Employees.ToList()); }

        [ChildActionOnly]
        [OutputCache(Duration = 10)]
        public string GetEmployeeCount() { return "Employee Count = " + db.Employees.Count().ToString() + "@ " + DateTime.Now.ToString(); }

        protected override void Dispose(bool disposing) { db.Dispose(); base.Dispose(disposing); }
    }
}