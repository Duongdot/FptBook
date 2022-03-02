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
        //public ActionResult Create([Bind(Include = "bookID,bookName,categoryID,authorID,quantity,price,image,shortDesc,detailDesc")] book Book, HttpPostedFileBase file)
        //{
        public ActionResult Create(HttpPostedFileBase image, book Book)
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
                //db.books.Add(Book);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                var check = db.books.FirstOrDefault(x => x.bookName.Equals(Book.bookName));
                if (check == null && image != null && image.ContentLength > 0)
                {
                    string pic = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/assets/img/Mangas"), pic);
                    image.SaveAs(path);
                    Book.image = pic;
                    db.books.Add(Book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "This Book is already exist";
                    return View();
                }
            }
            //}
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", Book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", Book.categoryID);
            return View(Book);
        }


        // GET: Books/Edit/5
        public ActionResult Edit(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //book Book = db.books.Find(id);
            //Session["imgPath"] = "~/assets/img/Mangas/" + Book.image;
            //if (Book == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", Book.authorID);
            //ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", Book.categoryID);
            //return View(Book);
            if (Session["UserNameAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                book Book = db.books.Find(id);
                Session["imgPath"] = "~/assets/img/Mangas/" + Book.image;
                if (Book == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AuthorID = new SelectList(db.authors, "AuthorID", "AuthorName", Book.authorID);
                ViewBag.CategoryID = new SelectList(db.categories, "CategoryID", "CategoryName", Book.categoryID);
                return View(Book);
            }
            return View("Error");
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "bookID,bookName,categoryID,authorID,quantity,price,image,shortDesc,detailDesc")] book Book)
        //{
        public ActionResult Edit(HttpPostedFileBase image, book Book)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(Book).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                if (image != null && image.ContentLength > 0)
                {
                    string pic = Path.GetFileName(image.FileName);
                    string path = Path.Combine(Server.MapPath("~/assets/img/Mangas/"), pic);
                    string oldPath = Request.MapPath(Session["imgPath"].ToString());
                    image.SaveAs(path);

                    Book.image = pic;

                    db.Entry(Book).State = EntityState.Modified;
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.books.Attach(Book);

                    db.Entry(Book).Property(a => a.price).IsModified = true;
                    db.Entry(Book).Property(a => a.quantity).IsModified = true;
                    db.Entry(Book).Property(a => a.authorID).IsModified = true;
                    db.Entry(Book).Property(a => a.categoryID).IsModified = true;
                    db.Entry(Book).Property(a => a.bookName).IsModified = true;
                    db.Entry(Book).Property(a => a.shortDesc).IsModified = true;
                    db.Entry(Book).Property(a => a.detailDesc).IsModified = true;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.authorID = new SelectList(db.authors, "authorID", "authorName", Book.authorID);
            ViewBag.categoryID = new SelectList(db.categories, "categoryID", "categoryName", Book.categoryID);
            return View(Book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["UserNameAdmin"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                book Book = db.books.Find(id);
                Session["imgOldPath"] = "~/assets/img/Mangas/" + Book.image;
                if (Book == null)
                {
                    return HttpNotFound();
                }
                return View(Book);
            }
            return View("Error");
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            //book Book = db.books.Find(id);
            //db.books.Remove(Book);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            //string oldPath = Path.GetFullPath(("~/assets/img/Mangas"));
            string oldPath = Request.MapPath(Session["imgOldPath"].ToString());
            book Book = db.books.Find(id);
            db.books.Remove(Book);
            //if (System.IO.File.Exists(oldPath))
            //{
            System.IO.File.Delete(oldPath);
            //}
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
