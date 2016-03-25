using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;

namespace _18_UpdatingData.Controllers
{
     /*
      Model içindeki bir Property'nin Edit View içinde görüntülenmesini fakat değiştirilememesini istiyoruz. EditorFor() tarayıcıda TextBox oluşturur. Bu yüzden değerinin değiştirilmesini istemediğimiz Property için başka bir Helper kullanmalıyız. Property Adını göstermek için LabelFor() Html Helper'ı kullanmışsız. Fakat LableFor() Property değerini gösteremez. Bu yüzden değeri göstermek için DisplayFor() Html Helper'ı kullanıyoruz. Fakat DisplayFor() helper tarayıcıda herhangi bir Tag oluşturmadığı için Form Server'a postalandığında ModelBinder değere bağlanamaz. Yani DisplayFor() ile gösterdiğimiz Property'i Required bir Property ise değer gelmemiş gibi göründüğü için hata verir. Bu Sorunu çözmek için HiddenInput() Html Helper'ı da uygulamalıyız.Bu Helper tarayıcıda Type'ı Hidden olan bir input nesnesi oluşturduğu için kullanıcı değeri göremez. Fakat Property'den aldığı değeri bir Tag içinde Server'a gönderebilir. Zaten değeri kullanıcıya DiplayFor() ile gönderiyoruz.
      
      HIDDENFOR() ile SERVER'e önemli veriler gönderilmez. Çünkü FIDDLER gibi bir program kullanılarak değiştirilebilir.
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
            if (ModelState.IsValid) { employeeBusinessLayer.AddEmmployee(employee); return RedirectToAction("Index"); } else return View();
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(emp => emp.ID == ID);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                employeeBusinessLayer.SaveEmployee(employee);

                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}