using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.Web.Controllers
{
    public class CategoryController : Controller
    {
        // TempData ile Category listeleme
        /*public ActionResult Select(string title)
        {
            if (title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager categoryManager = new CategoryManager();
            Category cat = categoryManager.GetCategoryByTitle(title);

            //Category cat = categoryManager.GetCategoryById(id.Value); // Bunu kullanabilmek için, Select action ında int id olması gerekir.

            if (cat == null)
            {
                return HttpNotFound();
                //return RedirectToAction("Index", "Home"); Gelmemişse buraya yönlendirme de yapabilirdik.
            }
            // Başka bi categori listeleme sayfası olmadığı için HomeController da göstermemiz gerekiyor orada listelendiği için bu yüzden tempdata ya atıp diğer controller a verileri taşıyoruz.
            TempData["modelCategory"] = cat.Notes;
            return RedirectToAction("Index", "Home");
        }*/


    }
}