using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FptBookNew1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["UserName"] == Session["UserName"] && Session["UserNameAdmin"] != null)
            {
                return View();
            }
            //return View("Error");
            return RedirectToAction("Error");
        }
        //public ActionResult ManagementBooks()
        //{
        //    return View();
        //}
        //public ActionResult ManagementAuthor()
        //{
        //    return View();
        //}
        //public ActionResult ManagementCategory()
        //{
        //    return View();
        //}
        public ActionResult Error()
        {
            return View();
        }
    }
}