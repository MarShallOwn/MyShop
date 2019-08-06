using MyShop.Core.ViewModels;
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
        void AddToBasket(HttpContextBase httpcontext, string productId);
        void RemoveFromBasket(HttpContextBase httpcontext, string itemId);
        List<BasketItemViewModel> GetBasketItem(HttpContextBase httpcontext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpcontext);
    }
}
