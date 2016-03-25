using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _9_GenerateHyperLink.Models;

namespace _9_GenerateHyperLink.Controllers
{
    /*
      LinkExtensions Class'ının ActionLink() Extension methodunun Html çıktısı <a> Tag'ı dır.Bu Tag tarayıcıdan tetiklendiğinde sayfayı Server'a Post'alar ve bir Action method çalıştırır. Action method View'ı çalıştırır ve tarayıcıda yeni View gösterilir. Asp.net'de hyperlink ile aynı işi yapar.
     ActionLink() methodunun 10 overload'ı var. Parametresiz overload'ı yok. 
      
     Overload'larda kullanılan parametrelerin -> birinci parametre Extension parametresi
     String linkText: Tıklanacak yazı.
     String actionName: Çalıştırılacak Action methodın adı.
     RouteValueDictionary/object routeValues: parametre olarak anonymous Method veriyoruz. Method Property'leri Link'e QueryString'ler olarak eklenir. Çalıştırılacak Action Method parametre bekliyorsa bu QueryString'leri kullanır.(Method parametre adı ile anonymous Property adının aynı olması gerekir.)
     String controllerName: Oluşturulan <a> Tag'ın başka bir Controller'deki Action methodu tetiklemesini istiyorsak kullanırız.
     object/IDictionary<string, object> htmlAttributes: Oluşturulacak Tag'a eklenecek Html özellikleri. 
     string protocol: Oluşturulan <a> Tag'ının Action methodu çalıştırmak için kullanacağı Http,Https...
     string hostName: Galiba dışarıdaki bir siteyi yazamıyoruz.
     string fragment: Bir sayfadaki Çapayı işaret edior. 
     
     
    */
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeContext employeeContext = new EmployeeContext();

            List<Employee> employees = employeeContext.Employees.ToList();

            return View(employees);
        }

        public ActionResult Details(int id)
        {
            EmployeeContext employeeContext = new EmployeeContext();
            
            Employee employee = employeeContext.Employees.Single(emp => emp.EmployeeId == id);

            return View(employee);
        }
    }
}
