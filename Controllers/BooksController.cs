using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FptBookNew1.Models;

namespace FptBookNew1.Controllers
{
    public class BooksController : Controller
    {
        private ModelDatabase db = new ModelDatabase();

        // GET: Books
        public ActionResult Index()
        {
            if (Session["UserNameAdmin"] != null)
            {
                var books = db.books.Include(b => b.author).Include(b => b.category);
                return View(books.ToList());
                //return View();
            }
            //return View("Error");
            return RedirectToAction("Error","Admin");

        }

        // GET: Books/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName");
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookID,bookName,categoryID,authorID,quantity,price,image,shortDesc,detailDesc")] book book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", book.categoryID);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", book.categoryID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookID,bookName,categoryID,authorID,quantity,price,image,shortDesc,detailDesc")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", book.categoryID);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            book book = db.books.Find(id);
            db.books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
