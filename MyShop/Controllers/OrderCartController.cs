﻿using Binding.Models;
using NGLayer.Models.Data;
using NGLayer.Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class OrderCartController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var myCart = Session["myCart"] as List<CartOrderViewModel> ?? new List<CartOrderViewModel>();
            if (myCart.Count == 0 || Session["myCart"] ==null)
            {
                ViewBag.Message = "Ваша корзина пуста";
                
            }
            decimal total = 0m;
            foreach (var item in myCart)
            {
                total += item.TotalPrice;
            }
            ViewBag.WholeTotal = total;
            return View(myCart);
        }
        public ActionResult OrderCartPartial()
        {
            CartOrderViewModel model = new CartOrderViewModel();
            int quantity = 0;
            
            decimal price = 0m;
            if(Session["myCart"]!=null)
            {
                var listOrder = (List<CartOrderViewModel>)Session["myCart"]; 
                foreach(var item in listOrder)
                {
                    quantity += item.Quantity;
                    price += item.Quantity * item.Price;
                }
                model.Quantity = quantity;
                model.Price = price;
            }
            else
            {
                model.Quantity = 0;
                model.Price = 0;
            }
           
            return PartialView(model);
        }

        public ActionResult AddtomycartPartial(int id)
        {
            List<CartOrderViewModel> cartorderVM = Session["myCart"] as List<CartOrderViewModel> ?? new List<CartOrderViewModel>();
            CartOrderViewModel model = new CartOrderViewModel();
            using (Db db = new Db())
            {
                Products product = db.product.Find(id);
                var productIsExists = cartorderVM.FirstOrDefault(x => x.ProductId == id);
                //if product isn't exists in the cart
                if (productIsExists==null)
                {
                    cartorderVM.Add(new CartOrderViewModel()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = 1,
                        ImageProduct = product.ImageString
                    });
                }
                //if  product is already in cart, then we will increment quantity
                else
                {
                    productIsExists.Quantity++;

                }
                int quantity = 0;
                decimal price = 0m;
                foreach (var item in cartorderVM)
                {
                    quantity += item.Quantity;
                    price += item.Price * item.Quantity;
                }
                model.Quantity = quantity;
                model.Price = price;
                Session["myCart"] = cartorderVM;
            }
            return PartialView(model);
        }
        public string TestMethod(int id)
        {
            return ("Это ид товара " + id);
        }
        public JsonResult IncreaseProductCart(int productId)
        {
            List<CartOrderViewModel> cartorderVM = Session["myCart"] as List<CartOrderViewModel>;
            using (Db db = new Db())
            {
                CartOrderViewModel model = cartorderVM.FirstOrDefault(x => x.ProductId == productId);
                model.Quantity++;
                var result = new
                {
                    quantity = model.Quantity,
                    price = model.Price
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
        }
        public JsonResult DecreaseProductCart(int productId)
        {
            List<CartOrderViewModel> cartorderVM = Session["myCart"] as List<CartOrderViewModel>;
            using (Db db = new Db())
            {
                CartOrderViewModel model = cartorderVM.FirstOrDefault(x => x.ProductId == productId);
                if (model.Quantity > 1)
                {
                    model.Quantity--;
                }
                else 
                {
                    model.Quantity = 0;
                    cartorderVM.Remove(model);
                }
                
                var result = new
                {
                    quantity = model.Quantity,
                    price = model.Price
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        public void DeletePosition(int productId)
        {
            List<CartOrderViewModel> cartorderVM = Session["myCart"] as List<CartOrderViewModel>;
            using (Db db = new Db())
            {
                CartOrderViewModel model = cartorderVM.FirstOrDefault(x => x.ProductId == productId);
                cartorderVM.Remove(model);

                
            }

           
           
        }
    }
}