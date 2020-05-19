using supermarket.Models;
using supermarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace supermarket.Controllers
{
    public class ShoppingCartController : Controller
    {
        SuperMarketDB storeDB = new SuperMarketDB();

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);


            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            //int Qty = Convert.ToInt32(qty);

            var addedItem = storeDB.Items
                .Single(item => item.ItemId == id);


            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedItem);


            return RedirectToAction("Index");
        }

        public ActionResult Clearcart()
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);


            cart.EmptyCart();

            return RedirectToAction("Index","Home");

        }
        public ActionResult Clearcart2()
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);


            cart.EmptyCart();

            return RedirectToAction("Index");

        }


        public ActionResult RemoveFromCart(int id)
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);


            

            int itemCount = cart.RemoveFromCart(id);
            return RedirectToAction("Index");

        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var rd = ControllerContext.ParentActionViewContext.RouteData;
            var cart = ShoppingCart.GetCart(this.HttpContext);
            if (rd.GetRequiredString("action").Equals("Complete")) 
            { ViewData["CartCount"] = 0;
                ViewBag.cart = 1;

            }
            else{ 
            ViewData["CartCount"] = cart.GetCount();
                ViewBag.cart = 0;

            }
            return PartialView("CartSummary");
        }
    }
}