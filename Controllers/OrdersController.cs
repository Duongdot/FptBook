using FptBookNew1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FptBookNew1.Controllers
{
    public class OrdersController : Controller
    {
        private ModelDatabase _db = new ModelDatabase();
        // GET: Orders
        public ActionResult Index()
        {
            if (Session["UserNameAdmin"] != null)
            {
                var orders = _db.orders.Include(o => o.account);
                return View(orders.ToList());
            }
            return View("Error");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                order order = _db.orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return View("Error");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}