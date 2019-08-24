﻿using MyShop.Core.Contracts;
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
        IRepository<Customer> customers;
        public BasketController(IBasketService basketService, IOrderService OrderService,IRepository<Customer> customers)
        {
            this.basketService = basketService;
            this.orderService = OrderService;
            this.customers = customers;
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
        
        [Authorize]
        public ActionResult CheckOut()
        {
            Customer customer = customers.Collection().FirstOrDefault(c=>c.Email==User.Identity.Name);

            if (customer != null)
            {
                Order order = new Order()
                {
                    FirstName = customer.FirstName,
                    Surname = customer.LastName,
                    Email = customer.Email,
                    Street = customer.Street,
                    City = customer.City,
                    State = customer.State,
                    ZipCode = customer.ZipCode,
                };

                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(Order order)
        {
            var basketitems = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;

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