using FptBookNew1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FptBookNew1.Controllers
{
    public class AccountController : Controller
    {
        private ModelDatabase _db = new ModelDatabase();
        // GET: Home
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(account _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.accounts.FirstOrDefault(s => s.username == _user.username);
                if (check == null)
                {
                    _user.password = GetMD5(_user.password);
                    //_db.Configuration.ValidateOnSaveEnabled = false;
                    _db.accounts.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.ErrorMessage = "User name and Password wrong";
                    return View();
                }
            }
            return View();


        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var _Password = GetMD5(password);
                var data = _db.accounts.Where(s => s.username.Equals(username) && s.password.Equals(_Password)).ToList();
                if (data.Count() > 0)
                {
                    if (data.FirstOrDefault().state == 0)
                    {
                        Session["UserName"] = data.FirstOrDefault().username;
                        //Session["UserName"] = "UserName";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //add session
                        Session["UserNameAdmin"] = data.FirstOrDefault().username;
                        //Session["UserNameAdmin"] = "Admin";
                        return RedirectToAction("Index", "Admin");
                    }

                }
                else
                {
                    //ViewBag.Error = "User name and Password wrong";
                    ViewBag.ErrorMessage = "User name and Password wrong";
                    //return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Editinfor()
        {
            var UserName = Session["UserName"];
            account obj = _db.accounts.ToList().Find(x => x.username.Equals(UserName));
            if (obj == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View(obj);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditInfor()
        //{
        //    var user = Session["UserName"];
        //    account obj = _db.accounts.ToList().Find(x => x.username.Equals(user));
        //    if (obj == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(obj);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editinfor(account obj)
        {
            account tmp = _db.accounts.ToList().Find(x => x.username == obj.username); //find the customer in a list have the same username with the username input
            if (tmp != null)  //if find out the customer
            {
                tmp.username = obj.username;
                tmp.fullname = obj.fullname;
                tmp.password = GetMD5(obj.password);
                tmp.phone = obj.phone;
                tmp.email = obj.email;
                tmp.state = obj.state = 0;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }


        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}