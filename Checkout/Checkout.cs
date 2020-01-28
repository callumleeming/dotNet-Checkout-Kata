using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Models;

namespace Checkout
{
    public class Checkout
    {
        private List<Item> _items;
        private List<Discount> _discounts;

        public Checkout()
        {
            _discounts = new List<Discount>();
            _items = new List<Item>();
        }

        public void Scan(Item item)
        {
            if (item == null)
            {
                throw new Exception("Item is null.");
            }

            _items.Add(item);
        }

        public void AddDiscount(Discount discount)
        {
            _discounts.Add(discount);
        }

        private int CalculateDiscount(Discount discount, List<Item> items)
        {
            int itemCount = items.Count(item => item.Name == discount.Name);
            return (itemCount / discount.Quantity) * discount.Value;
        }

        public int Total()
        {
            var totalPrice = _items.Sum(item => item.Price);
            var totalDiscount = _discounts.Sum(discount => CalculateDiscount(discount, _items));

            return totalPrice - totalDiscount;
        }
    }
}
