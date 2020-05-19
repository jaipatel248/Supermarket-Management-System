using supermarket.Models;
using supermarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace supermarket.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        SuperMarketDB storeDB = new SuperMarketDB();
        const string PromoCode = "50";


        public ActionResult AddressAndPayment(float tot)
        {
            int ab = Convert.ToInt32(tot);
            System.Diagnostics.Debug.WriteLine(ab.Equals(0));

            if (ab.Equals(0))
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["tot"] = tot.ToString();
                return View();
            }
        }

        /*  [HttpPost]
          public ActionResult AddressAndPayment(FormCollection values)
          {
              var order = new Order();
              TryUpdateModel(order);

              try
              {
                  if (string.Equals(values["PromoCode"], PromoCode,
                      StringComparison.OrdinalIgnoreCase) == false)
                  {
                      return View(order);
                  }
                  else
                  {
                      order.Username = User.Identity.Name;
                      order.OrderDate = DateTime.Now;


                      storeDB.Orders.Add(order);
                      storeDB.SaveChanges();

                      var cart = ShoppingCart.GetCart(this.HttpContext);
                      cart.CreateOrder(order);

                      return RedirectToAction("Complete",
                          new { id = order.OrderId });
                  }
              }
              catch
              {

                  return View(order);
              }
          }*/

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
       
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    order.Total = Convert.ToInt32(TempData["tot"]);
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    TempData["Firstname"] = values["Firstname"];
                    TempData["Lastname"] = values["Lastname"];
            
            var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
        
        }

        public ActionResult Complete(int id)
        {

            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                ViewBag.fn = TempData["Firstname"];
                ViewBag.ln = TempData["Lastname"];
                
                var cart = ShoppingCart.GetCart(this.HttpContext);

                ViewBag.carts = true;
                var viewModel = new ShoppingCartViewModel
                {
                    CartItems = cart.GetCartItems(),
                    CartTotal = cart.GetTotal()
                };
                return View(viewModel);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Print()
        {

                return View();
            
        }
    }
}