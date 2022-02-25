using FptBookNew1.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


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
            return RedirectToAction("Error", "Admin");

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
        public ActionResult Create([Bind(Include = "bookID,bookName,categoryID,authorID,quantity,price,image,shortDesc,detailDesc")] book Book)
        {
            //System.Drawing.Image.FromFile(Path.GetFileName(file.FileName));

            //if (file != null && file.ContentLength > 0)
            //{

            //    string fileName = Path.GetFileName(file.FileName);
            //    string directory = Server.MapPath("~/assets/Mangas");
            //    if (!Directory.Exists(directory))
            //    {
            //        Directory.CreateDirectory(directory);
            //    }
            //    string path = Path.Combine(directory, fileName);
            //    file.SaveAs(path);

            //    //var fileName = Path.GetFileName(file.FileName);
            //    //var path = Path.Combine(Server.MapPath("~/Content/Uploads"));
            //    //file.SaveAs(path);
            //}
            //if (file != null && file.ContentLength > 0)
            //{
            //    string path = Path.Combine(Server.MapPath("~/assets/Mangas"), Path.GetFileName(file.FileName));
            //    file.SaveAs(path);

                if (ModelState.IsValid)
                {

                    db.books.Add(Book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            //}
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", Book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", Book.categoryID);
            return View(Book);
        }

        //[HttpPost]
        //public ActionResult UploadFiles(HttpPostedFileBase file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {


        //            if (file != null)
        //            {
        //                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
        //                file.SaveAs(path);

        //            }
        //            ViewBag.FileStatus = "File uploaded successfully.";
        //        }
        //        catch (Exception)
        //        {

        //            ViewBag.FileStatus = "Error while file uploading.";
        //        }

        //    }
        //    return View("Index");
        //}

        // GET: Books/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book Book = db.books.Find(id);
            if (Book == null)
            {
                return HttpNotFound();
            }
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", Book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", Book.categoryID);
            return View(Book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookID,bookName,categoryID,authorID,quantity,price,image,shortDesc,detailDesc")] book Book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", Book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", Book.categoryID);
            return View(Book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book Book = db.books.Find(id);
            if (Book == null)
            {
                return HttpNotFound();
            }
            return View(Book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            book Book = db.books.Find(id);
            db.books.Remove(Book);
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
