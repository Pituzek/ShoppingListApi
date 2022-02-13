using ShoppingListApi.Models;
using System;
using Xunit;


namespace ShoppingListApi.Tests
{
    public class ProgressiveTaxedPolicyTests
    {
        [Theory]
        [InlineData(2, 2)]
        [InlineData(9.99, 9.99)]
        [InlineData(10, 10)]
        [InlineData(10.01, 10.1101)]
        [InlineData(101, 111.1)]
        [InlineData(1001, 1201.2)]
        [InlineData(100001, 200002)]
        public void Apply_ReturnsUnchangedPrice(decimal priceBeforeTaxes, decimal expectedPriceAfterTaxes)
        {
            var progressiveTaxPolicy = new ProgressiveTaxedPolicy();

            var priceAfterTaxes = progressiveTaxPolicy.Apply(priceBeforeTaxes);

            Assert.Equal(expectedPriceAfterTaxes, priceAfterTaxes, 4);
        }

        [Fact]
        public void Apply_ReturnsPriceAfterProgressiveTaxes()
        {
            const decimal priceBeforeTaxes = 2;
            var progressiveTaxPolicy = new ProgressiveTaxedPolicy();

            var priceAfterTaxes = progressiveTaxPolicy.Apply(priceBeforeTaxes);

            decimal expectedPriceAfterTaxes = 2;
            Assert.Equal(expectedPriceAfterTaxes, priceAfterTaxes);
        }
    }
}
