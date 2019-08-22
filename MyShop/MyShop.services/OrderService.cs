using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> OrderContext;

        public OrderService(IRepository<Order> OrderContext)
        {
            this.OrderContext = OrderContext;
        }

        public void CreateOrder(Order BaseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach(var item in basketItems)
            {
                BaseOrder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                });
            }
            OrderContext.Insert(BaseOrder);
            OrderContext.Commit();
        }
    }
}
