using System.Web.Mvc;
using _36_Html.TextBoxAndHtml.TextBoxFor.Models;

namespace _36_Html.TextBoxAndHtml.TextBoxFor.Controllers
{
    /*
     */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Company company = new Company("Pragim");
            ViewBag.Departments = new SelectList(company.Departments, "Id", "Name");
            ViewBag.CompanyName = company.CompanyName;

            return View();
        }
        public ActionResult Index1() { Company company = new Company("Pragim"); return View(company); }

    }
}
