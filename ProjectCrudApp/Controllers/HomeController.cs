using ProjectCrudApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCrudApp.Controllers
{
    public class HomeController : Controller
    {

        CrudAppEntities2 db = new CrudAppEntities2();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.students.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(student s)
        {
            if (ModelState.IsValid == true)
            {
                db.students.Add(s);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    RedirectToAction("Index");
                    TempData["createMessage"] = "<script>alert('Data inserted')</script>";

                }
                else
                {
                    TempData["createMessage"] = "<script>alert('Data not inserted')</script>";
                }
            }
            return RedirectToAction("Index");
            
        }


        public ActionResult Edit(int id)
        {
            var row = db.students.Where(model => model.id == id).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(student s)
        {
            if (ModelState.IsValid == true)
            {
                db.Entry(s).State= System.Data.Entity.EntityState.Modified;
               int a= db.SaveChanges();
                if (a > 0) {
                    return RedirectToAction("Index");
                    TempData["EditMessage"] = "<script>alert('Record Edited !!')</script>";

                }
                else
                {
                    TempData["EditMessage"] = "<script>alert('Record not Edited')</script>";
                }
              
            }
            return View();
        }

        public ActionResult Delete(int id) {
            var row = db.students.Where(model => model.id == id).FirstOrDefault();
            if (row != null) {
                db.Entry(row).State = System.Data.Entity.EntityState.Deleted;
                int  a= db.SaveChanges();
                if (a > 0)
                {
            
                    TempData["DeleteMessage"] = "<script>alert('Record Deleted !!')</script>";

                }
                else
                {
                    TempData["DeleteMessage"] = "<script>alert('Record not Deleted')</script>";
                }



            }

            return RedirectToAction("Index");
        }

    }
}