using System;
using Checkout.Models;
using NUnit.Framework;

namespace Checkout.Tests
{
    //Item Unit      Special
    //Price     Price
    //--------------------------
    //A     50       3 for 130
    //B     30       2 for 45
    //C     20
    //D     15

    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void ItShouldThrowException()
        {
            var checkout = new Checkout();

            Assert.Throws<Exception>(() => checkout.Scan(null));
        }

        [Test]
        public void A_ShouldReturn50()
        {
            var checkout = new Checkout();

            var item = new Item
            {
                Name = "A",
                Price = 50
            };

            checkout.Scan(item);

            var result = checkout.Total();

            Assert.AreEqual(50, result);
        }

        [Test]
        public void ShouldCalculateTotal()
        {
            var checkout = new Checkout();

            checkout.Scan(new Item { Price = 50, Name = "A" });
            checkout.Scan(new Item { Price = 30, Name = "B" });
            checkout.Scan(new Item { Price = 20, Name = "C" });
            checkout.Scan(new Item { Price = 15, Name = "D" });

            var result = checkout.Total();

            Assert.AreEqual(115, result);
        }

        [Test]
        public void ShouldApplyDiscount_For_A()
        {
            var checkout = new Checkout();

            checkout.AddDiscount(new Discount { Name = "A", Quantity = 3, Value = 20});
            checkout.Scan(new Item { Price = 50, Name = "A" });
            checkout.Scan(new Item { Price = 50, Name = "A" });
            checkout.Scan(new Item { Price = 50, Name = "A" });

            var result = checkout.Total();

            Assert.AreEqual(130, result);
        }

        [Test]
        public void ShouldApplyDiscount_For_B()
        {
            var checkout = new Checkout();

            checkout.AddDiscount(new Discount { Name = "B", Quantity = 2, Value = 15 });
            checkout.Scan(new Item { Price = 30, Name = "B" });
            checkout.Scan(new Item { Price = 30, Name = "B" });

            var result = checkout.Total();

            Assert.AreEqual(45, result);
        }
    }
}