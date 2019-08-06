using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
    public interface IBasketService
    {
        public void AddToBasket(HttpContextBase httpcontext, string productId);
        public void RemoveFromBasket(HttpContextBase httpcontext, string itemId);
    }
}
