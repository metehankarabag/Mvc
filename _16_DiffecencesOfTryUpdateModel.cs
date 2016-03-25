using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace _16_DiffecencesOfTryUpdateModel.Controllers
{
    /*
      UpdateModel() kullandığımızda veri girmenin zorunlu olduğu bir alana veri gönderilmesse hata verir. Çünkü bu method void döner.
      TryUpdateModeL() kullandığımızda method boolen döndüğü için hata almayız. Bu methodun döndüğü değer isValid'i etkiler.
      
      Bu methodlar ModelBinder'ın açıkça kullanılmış hali. Yani kullanmamıza gerek yok. Gelmesi gereken bir değer almadığınde varsayılan olaram ModelBinder aynı işi yapar.(Employee'i Action method parametresin ekleyip dene) Bu methodlar Postalanmış Form Tag'larından gelen veriye ModelBinder'ın bağlanmasını istemiyorsak kullanışlıdır. Mesela kullanıcı adını değiştirmek istiyor biz istemiyoruz. Binder property'e bağlanmıyorsa, kullanıcı değer gönderse bile değer işleme girmez.
      
      Not: DateTime Struct'ı değer girilmeden kullanılırsa hata verir. Null olduğunda hata vermemesi için ? ekliyoruz. Bu derste veri tabanına NULL DATE time girebilmek için BusinessLayer'da 2 değişiklik yaptık. 
      1. STRUCT(Value Type) olan DATETIME'i ? ile NULLABLE yaptık ve Veri tabanında veriyi alırken DATETIME'a çevirmeden önce if kontrolü uyguluyoruz.       
      2. Veri tabanındada Stored Procedure'u null veri gelebilir şekilde ayarlıyoruz. DATABASE = Mvc11 
      
      Employee CLASS'ına Required ATTRIBUTE'u eklemek için EntityFramework indiriyoruz.
    */
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();
            return View(employees);
        }
        [HttpGet] [ActionName("Create")] public ActionResult Create_Get() { return View(); }

        [HttpPost] [ActionName("Create")]
        public ActionResult Create_Post()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();

            Employee employee = new Employee();
            TryUpdateModel(employee);
            if (ModelState.IsValid)
            {
                employeeBusinessLayer.AddEmmployee(employee);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}