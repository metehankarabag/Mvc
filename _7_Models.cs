using System.Web.Mvc;
using _7_Models.Models;

namespace _7_Models.Controllers
{
    /*MODEL
      Modellerin kullanım amacı VIEW'ların kullancağı veriyi bir .net ortamından almasını sağlamaktır. View'ın veri kaynağı olarak bu ortamı belirlediğimizde Strongly Typed View kullanmış oluruz. Çünkü bu ortamın hangi türde veri alıp verdiğini kendimiz belirliyoruz ve veri her nerden geliyorsa gelsin bu ortama giremiyorsa View'da kullanılamaz. Bu sadeyede tamamen güvenli oluyor. Ayrıca bu ortam .net tarafından tanımlandığı için ortamı yanlış kullanma durumuda olmuyor. Bu ortamı oluşturmak için CLASS kullanıyoruz veri türlerini belirlemek için ise PROPERTY. Gelen verileri bu Class'ın bir örneğine alıp View'a gönderiyoruz. 
     Bir VIEW'ın sabit veri kaynağını(türünü) belirlemek için VIEW'da ilk satıra @model Veri_kaynagı_olacak_türün_tamAdı nı View'da yazmak yeterlidir.
     Not: View'a herhangi bir ortam gönderilmesse Null referance Exception hatası alırız.
    */
    public class EmployeeController : Controller
    {
        public ActionResult Details()
        {
            Employee employee = new Employee() { EmployeeId = 101, Name = "Jhon", Gender = "Male", City = "London", };
            return View(employee);
        }
    }
}
