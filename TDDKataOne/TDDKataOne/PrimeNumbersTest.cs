using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace TDDKataOne
{
    [TestFixture]
    public class PrimeNumbersTest
    {
        [Test]
        public void it_should_generate_non_empty_list()
        {
            var numbers = GetPrimeNumbers();
            numbers.Should().NotBeEmpty();
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(17)]
        [Test]
        public void it_should_contain_number(int number)
        {
            var numbers = GetPrimeNumbers();
            numbers.Should().Contain(x => x.Equals(number));
        }

        [Test]
        public void it_should_not_contain_any_even_number_except_2()
        {
            var numbers = GetPrimeNumbers();
            numbers.Should().NotContain(x => x != 2 && x%2 == 0);
        }

        [Test]
        public void it_should_not_contain_composite_numbers()
        {
            var numbers = GetPrimeNumbers();
            numbers.Should().NotContain(x => !IsPrimeNumber(x));
        }

        [Test]
        public void it_should_not_contain_numbers_over_1000()
        {
            var numbers = GetPrimeNumbers();
            numbers.Should().NotContain(x => x >= 1000);
        }

        private bool IsPrimeNumber(int number)
        {
            for (int i = 2; i < number-1; i++)
            {
                if (number%i == 0)
                {
                    Console.WriteLine($"{number} is divisible by {i}");
                    return false;
                }
            }
            return true;
        }

        private static int[] GetPrimeNumbers()
        {
            int[] numbers = new PrimeNumberGenerator().Generate();
            return numbers;
        }
    }
}
