using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _10_WorkingWithMultipleTables.Models;

namespace _10_WorkingWithMultipleTables.Controllers
{
    /*
      Department Index View'da oluşturduğumuz ActionLink()'ler ile Department'a üye olan Employee'leri gösteren başka bir Controller'daki Action methoud çalıştırıyor. Çalıştırılacak Action method parametre olarak DepartmentId alıyor. Bu değere Department Index View'ın oluşturacağı <a> Tagından gelidiği için departmentId değerlerini <a> Tagına eklememiz gerekiyor. Bu yüzden ActionLink() methodunun routeValue parametresini kullanmamız gerekir. Bu parametreye değer olarak Anonymous method vermeliyiz. Oluşturduğumuz dinamik Property adı ile çalıştırılacak Action methodun beklediği parmaetre adı aynı olmalı. Çünkü ActionLink() property'i QueryString olarak Url'in sonuna ekleyecek ActionMethod'da değerini Url'in donundaki QueryString'den alacak. 
     Employee'leri gösteren View'da da ActionLink()'ler var bu Action Link'ler ile Employee bilgilerini gösterdiğimiz başka bir Action'ı çalıştıracağız. Aynı mantığı bunada uyguluyoruz.
     
      Deparment Index View'dan başka bir Controller'deki Action'ı çalıştırdığımız için ActionLink()'ın 7. overload'ını kullanmak gerekir. Son Property'i oluşturulacak Tag'a uygulanacak Css'i belirler. Null verebeliriz. Bu parametreyi kullanmassak yanlış overload'ı kullanmış oluruz fakat hata almayız ama status barsa Id yerine Lenght grünür. 
      Veri göndermeseydik 4. overload'ı kullanabilirdik. veri göndermeseydik ve Contoller da aynı olsaydı 2. overload'ı kullanabilirdik.
    */
    public class EmployeeController : Controller
    {
        public ActionResult Index(int departmentId) 
        {
            EmployeeContext employeeContext = new EmployeeContext();
            List<Employee> employees = employeeContext.Employees.Where(emp => emp.DepartmentId == departmentId).ToList();
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
