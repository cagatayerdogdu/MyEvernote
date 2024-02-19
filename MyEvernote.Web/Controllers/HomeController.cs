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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            /* Bu kısım dbcontext ilk halindeyken ki test ettiğimiz komutlar.
            BusinessLayer.DBCreatedOn dBCreatedOn = new BusinessLayer.DBCreatedOn();
            dBCreatedOn.Insert_Test();
            dBCreatedOn.Update_Test();
            dBCreatedOn.Delete_Test();
            dBCreatedOn.Insert_Comment();
            */         

            /*CategoryControllerden TempData ile geliyor olsaydık bu alttaki blok kullanılacaktı.*/
            /*if (TempData["modelCategory"] != null)
            {
                return View(TempData["modelCategory"] as List<Note>);
            }*/

            NoteManager note_manager = new NoteManager();
            return View(note_manager.GetAllNote());
        }
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager categoryManager = new CategoryManager();
            Category cat = categoryManager.GetCategoryById(id.Value);

            //Category cat = categoryManager.GetCategoryById(id.Value); // Bunu kullanabilmek için, Select action ında int id olması gerekir. 

            if (cat == null)
            {
                return HttpNotFound();
                //return RedirectToAction("Index", "Home"); Gelmemişse buraya yönlendirme de yapabilirdik.
            }
            return View("Index", cat.Notes);
        }
    }
}