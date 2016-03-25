using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using _38_CheckBoxList.Models;

namespace _38_CheckBoxList.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index() { SampleDBContext db = new SampleDBContext(); return View(db.Cities); }

        [HttpPost]
        public string Index(IEnumerable<City> cities)
        {
            if (cities.Count(x => x.IsSelected) == 0) return "You have not selected any City";
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You selected - ");
                foreach (City city in cities) if (city.IsSelected) sb.Append(city.Name + ", ");                
                sb.Remove(sb.ToString().LastIndexOf(","), 1);
                return sb.ToString();
            }
        }
    }
}
    

