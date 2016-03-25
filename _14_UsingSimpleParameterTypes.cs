using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace _14_UsingSimpleParameterTypes.Controllers
{
    /*
     Server'a Postalanan Form içindeki Html Tag'larının Id değelerini Action method parametrelerine yazarak Tag'ların değerlerini Action metod'a alabiliriz. Bu değer atamaları MODEL BINDER ile otomatik olarak yapılır. Bu kullanımda 30 40 tane veri alınacaksa zor bir iş olur, tavsiye edilmiyor.
     Parametre isimleri ile Tag Id'leri aynı olmalı.
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
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string name, string gender, string city, DateTime dateOfBirth)
        {
            Employee employee = new Employee();
            employee.Name = name;
            employee.Gender = gender;
            employee.City = city;
            employee.DateOfBirth = dateOfBirth;
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.AddEmmployee(employee);
            return RedirectToAction("Index");
        }
    }

}
