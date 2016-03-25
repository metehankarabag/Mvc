using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _34_DropDownList.Models;

namespace _34_DropDownList.Controllers
{   /*
      Daha önce yazmıştım
     */
    public class HomeController : Controller
    {
        SampleDBContext db = new SampleDBContext();
      //public ActionResult Index() { ViewBag.Departments = new SelectList(db.tblDepartment, "Id", "Name","1"); return View(); }
        public ActionResult Index()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (Department department in db.tblDepartment)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = department.Name,
                    Value = department.Id.ToString(),
                    Selected = department.IsSelected.HasValue ? department.IsSelected.Value : false
                };
                selectListItems.Add(selectListItem);
            }

            ViewBag.Departments = selectListItems;
            return View();
        }

    }
}
