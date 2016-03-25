using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;

namespace _24_PostRequest.Controllers
{
    /*INDEX VIEW'da 
      PostRequest ile Server'a silinecek veriyi göndermek için Html.BeginForm() Helper'ını kullanabiliriz. Bu method tarayıcıda Form Tag2ı oluşturulur. Bu Form Tag'ı içindeki bir input nesnesi tetiklerinse form içindeki tüm Html Tag'ları ile Server'a Post'alanır ve istediğimiz işi yaparız. Oluşturulacak Form Tag'ının içine Tag eklemek için Using() Bloğunu kullamamız gerekir. Blog içinde kullandığımız tüm helper'ların Html çıktısı form üyesi olarak tarayıcıya gönderilir. Form Tag'ı Server'a gitmek için Action Attribute'undaki Url'i kullanır. Oluştracak Tag'ın Action Attribute'unun alacağı Url'ı BeginForm() methodunun Constructor parametrelerini kullanacağız. 
      Dersde her veri tabanı satırını bir Form içinde oluşturup, satırların içinde bulunduğu form'ın satır Id'si ile Delete Action methodu çalıştırmasını istiyoruz. Action Method çalıştığında aldığı satırı silecek. Action Attribute'un belirlemek için BeginForm() methodunu Constructor'larındaki parametreleri kullanıyoruz. Her satırı tarayıcıya yazdırmak için foreach kullanmayılız ve her satır için form tag'ı istiyorsak, BeginForm() methodunu foreach içinde kullanmalıyız. Böylece BeginForm() Constructor'lerında satır değerlerini kullanabiliriz.(Id değerini içeren bir Tag varsa Action'a eklemeye gerek kalmaz.)
      BeginFom() methodu FormExtensions Class'ının 13 overload olan üyesi. Class'da ayrıca BeginRouteForm() methodu var. 
      Overload'larda kullanılan parametreler. *> 1. Extension parametre. 2. ve 3. Action ve Controller Name, RouteValueCollection ve object türünde routeValues Propery'si, FormMethod -> çalıştırılacak Action'ın türü ve onject ve Idictionary<string, object> türünde html Attribute parametreleri var.
      RouteValue parametresi ile Action Methodun istediği parametreleri gönderebilriz.
      
      Not: BeginForm()'un parmetresiz Constructor'unu kullandığımızda, oluşturulacak form Tag'ı varsayılan olarak View'ı çalıştıran Action Methodun Post'una parametresiz gider.
      Not: Form Tag'ı Server'a Action Attribute'undaki Url'ı kullanarak gittiği için kullanıcı tarayıcıdaki Url'ini kullanarak Url'e etki etse bile işe yaramaz. 
     
      BEINGFORM() methodu parametreleri: 1.ACTION METOD, 2. CONTROLLER,3. Action method parametreleri alıyor.(6. overload ama 5 görünüyor.)
     */
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();
            return View(employees);
        }
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get() { return View(); }
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(Employee employee)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            if (ModelState.IsValid) { employeeBusinessLayer.AddEmployee(employee); return RedirectToAction("Index"); } else return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.ID == id);
            return View(employee);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(x => x.ID == id);
            UpdateModel<IEmployee>(employee);

            if (ModelState.IsValid) { employeeBusinessLayer.SaveEmployee(employee); return RedirectToAction("Index"); } return View(employee);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}