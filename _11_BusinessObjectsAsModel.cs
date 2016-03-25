using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace _11_BusinessObjectsAsModel.Controllers
{
/*
      Veri tabanından veri alan ve veri üzerinde gerçekleşecek işlemleri yapan uygulamaya Bussines Object denir. Bu uygulamayı, uygulamanın Dll'ini istediğimiz uygulamaya ekleyerek kullanabiliriz. Yani projede Models adında bir klasör olacak o klasör içinde veri tabanı işlerini yapacaz diye bir şey yok. Önemli olan düzene uymak.
      Dll'ı Property'e ekledikten sonra Strongly Typed View oluşturmak için kullanıdğımız açılır pencerede Dll'i görebiliyoruz.
      Not: Oluşturduğumuz Business Object'ın kullanacağı ConnectionString'ı Dll'in atıldığı uygulamada belirliyoruz. Dll içinde çalışacağı uygulamanın ayarlarını kullanacağı için böyle olması gerekiyor tahminimce.
     */
    /* 12 Creating a View To Insert Data -> Bu dersin amacı otomatik oluşturulan View'ı düzenlemek. 
       SelectExtensions Class'ının DropdownList() Extension methodu tarayıcıda <select> Tag'ı oluşturuyor. Parametre olarak girdiğimiz SelectListItem'lar ise <option> Tag'ı oluşturuyor.
      DropdownList() methodunun 8 overload'ı var. 1., 2. ve 3.  parametreler tüm overload'larda aynı -> 1. Extension parametre, 2. Tag'ın Id değeri, 3. parametre liste itemler.
      string optionLabel: hiç bir şey seçili değilken gösterilecek yazı.
      object/IDictionary<string,object> htmlAttribute
      
      Not: DropDownList'e List item girmeden çalıştırırsak hata alırız.
    */
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.Employees.ToList();
            return View(employees);
        }
    }
}
