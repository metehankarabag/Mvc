using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace _22_PreventingItUsingInterface.Controllers
{
	/*
      Interface kullanarak istenmeyen güncelleştirmeleri UpdateModel<t>() kullanarak önleyebiliriz. Güncellenmesini istemediğimiz Property'i barındırmayan bir Interface oluşturup Model Class'ı türetebiliriz ve Eksik Property'i bu Class'a ekleyebiliriz. updateModel<t>() Generic bir method olduğundan dolayı Generic Type'ı Interface olarak belirleyip, parametre olarak Class örneği verebiliriz. -> Interface a = new DerivedClass(). Böylece Method güncellenecek Model Property'lerini Interface'den alır ve istemediğimiz güncelleştirme gerçekleştirilmez.
      
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
    }
}