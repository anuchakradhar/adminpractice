using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using index.Models;

namespace index.Controllers
{
    public class HomeController : Controller
    {
        itemdbEntities db = new itemdbEntities();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Menu()
        {
            var list = db.Items.ToList();
            //ViewBag.Message = "Your contact page.";

            return View(list);
        }

        public ActionResult SubItemList(int id)
        {
            var list = db.SubItems.Where(x => x.item_id == id).ToList();
            ViewBag.Iname = db.Items.Find(id).item_name;
            return View(list);
        }

        public ActionResult Book()
        {
            return View();
        }
    }
}