using FptBookNew1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FptBookNew1.Controllers
{
    public class HomeController : Controller
    {
        private ModelDatabase _db = new ModelDatabase();
        // GET: Home
        public ActionResult Index()
        {
            var DataBook = _db.books.ToList();
            return View(DataBook);
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Index(string searchstring)
        {
            Session["Admin"] = null;
            List<book> DataBook = new List<book>();
            DataBook = _db.books.Where(x => x.bookName.Contains(searchstring)).ToList();
            if (DataBook == null)
            {
                return RedirectToAction("Index");
            }
            return View(DataBook);
        }
    }
}
