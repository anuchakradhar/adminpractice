using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using index.Models;
using Website.DAL.StaticHelper;

namespace index.Controllers
{
    public class AdminController : Controller
    {
        itemdbEntities db = new itemdbEntities();
        // GET: Admin
        public ActionResult Index()
        {
            var list = db.Items.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateItem(Item model)
        {
            if (model.is_deleted == null)
            {
                model.is_deleted = false;
            }
            if (model.is_active == null)
            {
                model.is_active = false;
            }

            if (model.ImgFile != null)
            {
                var filetype = ".jpg,.png,.pdf,.doc,.docx";
                string[] AllowExt = filetype.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string path = "~/uploads/itemImage/";
                var res = UploadHelper.UploadFile(model.ImgFile, path, AllowExt);
                if (res.Status)
                {
                    model.image_path = path;
                    model.image_name = res.FileName;
                }
            }

            db.Items.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = db.Items.Where(x => x.item_id == id).FirstOrDefault();
            if (data != null)
            {
                TempData["itemid"] = id;
                TempData.Keep();
                return View(data);
            }
            return View();
        }

        public ActionResult EditItem(Item model)
        {
            int itemID = (int)TempData["itemid"];
            var data = db.Items.Where(x => x.item_id == itemID).FirstOrDefault();
            if (model.is_deleted == null)
            {
                model.is_deleted = false;
            }
            if (model.is_active == null)
            {
                model.is_active = false;
            }


            if (data != null)
            {
                if (model.ImgFile != null)
                {
                    var filetype = ".jpg,.png,.pdf,.doc,.docx";
                    string[] AllowExt = filetype.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    string path = "~/uploads/itemImage/";
                    var res = UploadHelper.UploadFile(model.ImgFile, path, AllowExt);
                    if (res.Status)
                    {
                        data.image_path = path;
                        data.image_name = res.FileName;
                    }
                }
                data.item_name = model.item_name;
                data.display_sequence_number = model.display_sequence_number;
                data.is_active = model.is_active;
                data.is_deleted = model.is_deleted;
                data.created_date = model.created_date;
               
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var data = db.Items.Find(id);
            return View(data);
        }

        public ActionResult DeleteItem(Item model)
        {
            var itemid = db.Items.Where(x => x.item_id == model.item_id).FirstOrDefault();
            if (itemid != null)
            {
                var list = db.SubItems.Where(x => x.item_id == model.item_id).ToList();
                db.SubItems.RemoveRange(list);
                db.Items.Remove(itemid);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult SubItem()
        {
            var list = db.SubItems.ToList();
            return View(list);
        }

        public ActionResult AddSubItem()
        {
            ViewBag.ItemID = new SelectList(db.Items.ToList(), "item_id", "item_name");
            return View();
        }

        [HttpPost]
        public ActionResult AddSubItem(SubItem model)
        {
            db.SubItems.Add(model);
            db.SaveChanges();
            return RedirectToAction("SubItem", "Admin");
        }

        public ActionResult EditSubItem(int id)
        {
            var data = db.SubItems.Where(x => x.sub_id == id).FirstOrDefault();
            ViewBag.ItemID = new SelectList(db.Items.ToList(), "item_id", "item_name", data.item_id);
            
            if (data != null)
            {
                TempData["subitemid"] = id;
                TempData.Keep();
                return View(data);
            }
            return View();
        }

        public ActionResult EditSubItemm(SubItem model)
        {
            int subitemID = (int)TempData["subitemid"];
            var data = db.SubItems.Where(x => x.sub_id == subitemID).FirstOrDefault();
            if (model.is_deleted == null)
            {
                model.is_deleted = false;
            }
            if (model.is_active == null)
            {
                model.is_active = false;
            }
            if (data != null)
            {
                data.item_id = model.item_id;
                data.sub_item_name = model.sub_item_name;
                data.description = model.description;
                data.price = model.price;
                data.is_active = model.is_active;
                data.is_deleted = model.is_deleted;
                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("SubItem");
        }

        
        public ActionResult DeleteSubItem(long id)
        {
            var subitemid = db.SubItems.Where(x => x.sub_id == id).FirstOrDefault();
            if (subitemid != null)
            {
                db.SubItems.Remove(subitemid);
                db.SaveChanges();
            }
            return RedirectToAction("SubItem", "Admin");
        }
    }
}