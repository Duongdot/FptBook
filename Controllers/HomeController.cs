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
        
    }
}
