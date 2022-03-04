﻿using FptBookNew1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FptBookNew1.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ModelDatabase db = new ModelDatabase();
        //GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;

        }

        public ActionResult AddtoCart(string id)
        {
            var pro = db.books.SingleOrDefault(s => s.bookID == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult UpdateQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_pro = form["ID_Product"];
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);

        }
        public ActionResult Delete(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.DeleteCart(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;

            Cart cart = Session["Cart"] as Cart;

            if (cart != null)
                total_item = cart.TotalQuantity();
            ViewBag.TotalItem = total_item;

            return PartialView("BagCart");
        }
        public ActionResult Checkout(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                order _order = new order();
                _order.orderDate = DateTime.Now;
                _order.username = form["Username"];
                _order.address = form["Address"];
                _order.phone = form["Phone"];
                _order.totalPrice = Convert.ToInt32(form["TotalPrice"]);
                db.orders.Add(_order);

                foreach (var item in cart.Items)
                {
                    orderDetail orderDetail = new orderDetail();
                    orderDetail.orderID = _order.orderID;
                    orderDetail.bookID = item._shopping_product.bookID;
                    orderDetail.quantity = item._shopping_quantity;
                    orderDetail.amountPrice = item._shopping_product.price * item._shopping_quantity;

                    var pro = db.books.SingleOrDefault(s => s.bookID == orderDetail.bookID);

                    pro.quantity -= orderDetail.quantity;
                    db.books.Attach(pro);
                    db.Entry(pro).Property(a => a.quantity).IsModified = true;

                    db.orderDetails.Add(orderDetail);
                }

                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckoutSuccess", "ShoppingCart", new { id = _order.orderID });
            }
            catch
            {
                return Content("Error checkout, Check information again and your must sign in");
            }
        }
        public ActionResult CheckoutSuccess(int? id)
        {
            if (Session["UserName"] != null)
            {
                var order = db.orders.Find(id);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return View("ErrorCart");
        }

        public ActionResult OrderHistory(string id)
        {
            if (Session["Username"] != null)
            {
                var orderHis = db.orders.ToList().Where(s => s.username == id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (orderHis == null)
                {
                    return HttpNotFound();
                }
                return View(orderHis);
            }
            return View("ErrorCart");
        }
    }
}