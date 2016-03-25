using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _66_CheckOrUnCheckAllUsingJQuery.Models;

namespace _66_CheckOrUnCheckAllUsingJQuery.Controllers
{
    /*
     Tablo Header'daki ChechBox seçildiğinde diğer Checbox'ların da seçilmesi için Server'a gelmeye gerek yok. Bu yüzden Jquery kullanarak bu işi yapıyoruz.
     */
    public class HomeController : Controller
    {
        SampleDBContext db = new SampleDBContext();
        public ActionResult Index(){return View(db.Employees.ToList());}
        [HttpPost]
        public ActionResult Delete(IEnumerable<int> employeeIdsToDelete)
        {
            db.Employees.Where(x => employeeIdsToDelete.Contains(x.ID)).ToList().ForEach(db.Employees.DeleteObject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
