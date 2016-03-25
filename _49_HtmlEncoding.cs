using System.Linq;
using System.Web.Mvc;
using _49_HtmlEncoding.Models;

namespace _49_HtmlEncoding.Controllers
{
    /*
      Html Encoding: ACSII karakterlerin HTML ENTITY eşitleri ile değiştirilmesi işlemidir. CROSS-SİTE SCRİPTİNG ATTACK'ı engellemek için SERVER'dan çıkacak string karakterler varsayılan olarak kodlanır. Tarayıcıya kodlanmış String'i gönderdiğimizde sayfa kaynağında kodlanmış hali görünür fakat yorumlanarak kullanıcıya gerçek hali yansıtılır. Tarayıcıya kodlanmamış string gönderseydik tarayıcı bu string'i kod gibi çalıştırıp sonucunu kullanıcıya gösterecekti.
      
      Kodlamayı engellemek için 3 yol var. 
      1. IHTMLString sınıfını kullanmak.(Helper'i oluştururken bunu kullandık.) 2. Raw() 
      3. <%= %> Aspx yazımı normalde <%: %> : yerine = kullanıp helper'i parametre olarak verdiğimizde kodlanmamış String döner.
      Oluşturduğumuz helper'e normal String dönderip VIEW'da 2. veya 3. uygulama şeklinde kullandığımızda da çalışıyor. 
     */
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult Details(int id) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
        public ActionResult Edit(int id) { SampleDBContext db = new SampleDBContext(); Employee e = db.Employees.Single(x => x.Id == id); return View(e); }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                SampleDBContext db = new SampleDBContext();
                Employee employeeFromDB = db.Employees.Single(x => x.Id == employee.Id);
                employeeFromDB.FullName = employee.FullName; /**/ employeeFromDB.Gender = employee.Gender;
                employeeFromDB.HireDate = employee.HireDate; /**/ employeeFromDB.Age = employee.Age;
                employeeFromDB.PersonalWebSite = employee.PersonalWebSite; /**/ employeeFromDB.Salary = employee.Salary;

                db.ObjectStateManager.ChangeObjectState(employeeFromDB, System.Data.EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = employee.Id });
            }
            return View(employee);
        }
    }
}
