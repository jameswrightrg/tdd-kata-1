using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TDDKataOne
{
    [TestFixture]
    class ShoppingCartTest
    {
        [Test]
        public void applies_5_percent_discount()
        {
            var shoppingCart = new List<Tuple<decimal, int>>
            {
                new Tuple<decimal, int>(100.01m, 1)
            };
            var calculator = new NetValueCalculator();
            var value = calculator.GetNetValue(shoppingCart);
            value.Should().BeApproximately(95.01m, 0.005m);
        }

        [Test]
        public void applies_10_percent_discount()
        {
            var shoppingCart = new List<Tuple<decimal, int>>
            {
                new Tuple<decimal, int>(200.01m, 1)
            };
            var calculator = new NetValueCalculator();
            var value = calculator.GetNetValue(shoppingCart);
            value.Should().BeApproximately(180.01m, 0.005m);
        }

        [Test]
        public void zero_items_gives_zero_value()
        {
            var shoppingCart = new List<Tuple<decimal, int>>
            {
                new Tuple<decimal, int>(100, 0)
            };
            var calculator = new NetValueCalculator();
            var value = calculator.GetNetValue(shoppingCart);
            value.Should().BeApproximately(0, 0.005m);
        }

        [Test]
        public void should_handle_null_shopping_carts()
        {
            var calculator = new NetValueCalculator();
            var value = calculator.GetNetValue(null);
            value.Should().BeApproximately(0, 0.005m);
        }

        [Test]
        public void should_sum_all_items_in_the_shopping_cart()
        {
            var shoppingCart = new List<Tuple<decimal, int>>
            {
                new Tuple<decimal, int>(10, 1),
                new Tuple<decimal, int>(10, 1)
            };

            var calculator = new NetValueCalculator();
            var value = calculator.GetNetValue(shoppingCart);
            value.Should().BeApproximately(20, 0.005m);
        }


    }

    internal class NetValueCalculator
    {
        public decimal GetNetValue(List<Tuple<decimal, int>> shoppingCart)
        {
            if (shoppingCart == null)
            {
                return 0;
            }
            var grossValue = shoppingCart.Sum(t => t.Item1*t.Item2);
            if (grossValue > 200)
            {
                return grossValue*0.90m;
            }
            if (grossValue > 100)
            {
                return grossValue*0.95m;
            }
            return grossValue;
        }
    }
}
