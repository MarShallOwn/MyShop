using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {

        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService basketService, IOrderService OrderService)
        {
            this.basketService = basketService;
            this.orderService = OrderService;
        }

        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);

            return View(model);
        }

        public ActionResult AddToBasket(string ProductId)
        {
            basketService.AddToBasket(this.HttpContext, ProductId);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string itemId)
        {
            basketService.RemoveFromBasket(this.HttpContext, itemId);

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            var basketitems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";

            // Process payment

            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, basketitems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Thankyou", new { OrderId = order.Id });
        }

        public ActionResult Thankyou(string OrderId)
        {
            ViewBag.OrderId = OrderId;

            return View();
        }

    }
}