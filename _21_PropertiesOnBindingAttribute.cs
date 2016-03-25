using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;

namespace _21_PropertiesOnBindingAttribute.Controllers
{
	/*
      Bu derste güncellenecek Model Class'ını tekrar Action methodun parametresine alıyoruz. Fakat bu sefer parametreye Bind Attribute'unu uyguluyoruz. Bind Attribute'unun 3 Property'si var. Bu Property'ler Model Property'lerinden hangilerinin kullanılıp kullanılamayacağını belirler. Bind Attribute'unun Exclude Property'si içinde belirlediğimiz Model Property'lerine ModelBinder hiç bağlanamaz. Yani ModelBinder ile bağlantısını kestiğimiz Property Required ise ModelBinder bu Property'e hiç bağlanmadığından ModelState'in bu Property için değeri False olur ve hata mesajımı alırız. Bu yüzden REQUIRED Attribute'sini EMPLOYEE Class'dan kaldırmalıyız.
      UPDATEMODEL()'da böyle sorun olmadı çünkü MODELBINDER tüm CONTROL'lerden değerleri aldı. Bu methodu sadece Property'lerin güncellenmesini öner. Zaten Methodu isValide Propert'sinden gelen değere göre çalıştırıyoruz.
     
      Property değerini doldurmak için Id değeri ile veritabanından Property'nin değerini almalıyız. Bu işi yapıp Name değerini aldığımızda bile Required Property hata verir demekki bu iş method çalışmadan önce oluyor. Name Property'sine verdiğimiz değer ModelState'ı etkilemiyor.
      
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
        public ActionResult Edit_Post([Bind(Include="Id,Gender,DateOfBrith")]Employee employee)
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employee.Name = employeeBusinessLayer.Employees.Single(x => x.ID == employee.ID).Name;
            if (ModelState.IsValid) { employeeBusinessLayer.SaveEmployee(employee); return RedirectToAction("Index"); } return View(employee);
        }
    }
}