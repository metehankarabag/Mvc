using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;


namespace _20_PropertiesOnUpdateModel.Controllers
{
     /*
      Model daki bir Property'nin değerinin güncellenmesini engellemek için bir kaç yol var. Bu derste UpdateModel<t>() Methodunu Exclude ve Include parametreleri ile yapacağız. Yani güncellenecek veri method parametrelerinde otomaik olarak doldurulmayacak method içinde doldurulacak. Method parametrelerinde hangi satırın güncelleneceğini anlamak için sadece Id değerini isteyeceğiz. Bu Id değerini kullanarak veri tabanından güncellenecek satırı alıp bir Model örneği olarak UpdateModel<t>() verdiğimizde, Form'dan gelen veriler kullanılarak veri tabanındaki satır otomatik olarak eskileri ile değiştirilir. UpdateModel<t>() methodunun 10 overload'ı var. Bu Overload'lardaki Include ve Exclude parametreleri kullanıp ModelBinder'ın bağlanmasını istemediğimiz Property'lere bağlanmasını engelleyebiliriz.
      
      Fiddler'i aç > sayfada bir update isteği oluştur. Fiddler'de isteği kopyala composert sekmesine yapıştır verileri değiştir kontrol et.
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

        [HttpPost] [ActionName("Edit")]
        public ActionResult Edit_Post(int id)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            Employee employee = employeeBusinessLayer.Employees.Single(x => x.ID == id);
            UpdateModel(employee, new string[] { "ID", "Gender", "City", "DateOfBirth" });
            if (ModelState.IsValid) { employeeBusinessLayer.SaveEmployee(employee); return RedirectToAction("Index"); } return View(employee);
        }
        //[HttpPost][ActionName("Edit")]
        //public ActionResult Edit_Post(int id)
        //{
        //    EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //    Employee employee = employeeBusinessLayer.Employees.Single(x => x.ID == id);
        //    UpdateModel(employee, null, null, new string[] { "Name" });
        //    if (ModelState.IsValid) { employeeBusinessLayer.SaveEmployee(employee); return RedirectToAction("Index"); } return View(employee);
        //}
    }
}