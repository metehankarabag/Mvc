using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer;

namespace _13_UsingFormCollection.Controllers
{
    /*FormCollection Class
      FormCollection, NameValueCollection Class'ından türedeği için Server'a gönderilen Form'un içindeki Tag'ları Tag'ların Id ve değeri ile birlikte barındırabilen bir CLASS'dır. NameValueCollection Class'ında biri Read-Only diğeri değer alabilen 2 tane INDEX olduğu için FormCollection örneğine Index değeri verip Tag'lara ulaşabiliriz. Index'ler bir nesne içindeki alt nesneleri parametre olarak aldığı değeri kullanarak arar. FormCollection Class'ına Index uygulayarak Tag'lara ve değerlerine ulaştığımız için büyük ihtimalle Form verilerini tarayıcıdan Server'a almak için FormCollection Class'ın NameValueCollection parametre isteyen Contructor'ı kullanılıyor.
      1. Not: Form'daki değerleri alma ve FormColllection'ı doldurma işini ModelBinder yapar.
      FormCollectıon'ın dezavantajı girilen değerleri tek tek almasıdır. Bu zaman ve emek kaybı demek.
      
      2. Not: NameValueCollection'a eklenen key'ler Form içindeki Html tag'larının Id'leri değerleride tagların değerleridir ve key değerleri büyük küçük harf'e duyarlı değildir. 
      3. Not: Read-Only Index'deme Index parametresinden bulunan alt nesneye değer atılamayacak demek. Bunu daha önce yazmıştım ama kafamı karıştırıyor. Girilecek değerin türü Index türünü Index türüne göremi yoksa Index parametresinin türüne gremi belirliyoruz dikkat et.
     */FORMCOLLECTION'ın dezavantajı girilen değerleri tek tek almasıdır. Bu zaman ve emek kaybı demek.
      
     
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
        public ActionResult Create(FormCollection formCollection)
        {
            Employee employee = new Employee();
            employee.Name = formCollection["Name"];
            employee.Gender = formCollection["Gender"];
            employee.City = formCollection["City"];
            employee.DateOfBirth = Convert.ToDateTime(formCollection["DateOfBirth"]);
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            employeeBusinessLayer.AddEmmployee(employee);

            return RedirectToAction("Index");
        }
    }

}
