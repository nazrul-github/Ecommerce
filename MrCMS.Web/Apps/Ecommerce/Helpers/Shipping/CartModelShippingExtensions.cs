using System.Collections.Generic;
using System.Linq;
using MrCMS.Web.Apps.Ecommerce.Models;

namespace MrCMS.Web.Apps.Ecommerce.Helpers.Shipping
{
    public static class CartModelShippingExtensions
    {
        public static IEnumerable<CartItemData> ShippableItems(this CartModel cartModel)
        {
            return cartModel.Items.Where(item => item.RequiresShipping);
        }

        public static decimal ShippableTotalPreDiscount(this CartModel cartModel)
        {
            return cartModel.ShippableItems().Sum(item => item.Price);
        }

        public static decimal ShippableSubTotalPreDiscount(this CartModel cartModel)
        {
            return cartModel.ShippableItems().Sum(item => item.PricePreTax);
        }

        public static decimal ShippableCalculationTotal(this CartModel cartModel)
        {
            return cartModel.ShippableTotalPreDiscount() - cartModel.OrderTotalDiscount;
        }

        public static decimal ShippableCalculationSubTotal(this CartModel cartModel)
        {
            return cartModel.ShippableSubTotalPreDiscount() - cartModel.OrderTotalDiscount;
        }
    }
}