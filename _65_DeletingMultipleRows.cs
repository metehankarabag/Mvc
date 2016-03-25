using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _65_DeletingMultipleRows.Models;

namespace _65_DeletingMultipleRows.Controllers
{
    /* Index View'ın Html düzenini EditorForModel() methodu ile belirledik. EditorForModel() methodunun oluşturacağı Html'i EditorTemplates klasörü içinde oluştruduğumuz Employee View'ı ile düzenliyoruz. Employee Editor template'de tablo Body için checkBox ekleyerek tablo satırlarını düzenledik. Yani Employee View'ı Model'de olan her satır yanına bir ChechBox eklemek için oluşturduk. Bu CheckBox'lar Value olarak bir Employee'nin Id'sini alıyor. 
      
      Index View'da HtmlBeginForm() Server'a postalandığında Delete Action'ı çalıştırır. Delete Action'da parametre olarak employeeIdsToDelete bekliyor. Bu bizim CheckBox'larımızın Id değeri. Input formu onayladıktan sonra seçilmiş her CheckBox'ın value değeri Server'a gönderilir. ModelBinder CheckBox adı ile parametre adı aynı olduğu için CheckBox'ların uygun değerini parametreye atar. 
      
      Contains() methodu parametre olarak aldığı değeri tüm ChechBox'larda arar ve bulursa true döner. where() methodu Contains()'i her true dönderen kayıtı alır ve döner ve bu kayıtları sileriz.
     
     */
    public class HomeController : Controller
    {
        SampleDBContext db = new SampleDBContext();
        public ActionResult Index() { return View(db.Employees.ToList()); }
        [HttpPost]
        public ActionResult Delete(IEnumerable<int> employeeIdsToDelete)
        {
            db.Employees.Where(x => employeeIdsToDelete.Contains(x.ID)).ToList().ForEach(db.Employees.DeleteObject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
