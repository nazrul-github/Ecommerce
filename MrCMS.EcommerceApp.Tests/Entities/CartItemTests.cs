﻿using FluentAssertions;
using MrCMS.Web.Apps.Ecommerce.Entities;
using MrCMS.Web.Apps.Ecommerce.Entities.Cart;
using MrCMS.Web.Apps.Ecommerce.Models;
using Xunit;

namespace MrCMS.EcommerceApp.Tests.Entities
{
    public class CartItemTests
    {
        [Fact]
        public void CartItem_PricePreTax_ShouldBeProductPricePreTaxTimesQuantity()
        {
            var product = new StubProduct {PricePreTax = 10};
            var cartItem = new CartItem { Item = product, Quantity = 2 };

            var pricePreTax = cartItem.PricePreTax;

            pricePreTax.Should().Be(20);
        }
        [Fact]
        public void CartItem_Price_ShouldBeProductPriceTimesQuantity()
        {
            var product = new StubProduct {Price = 10};
            var cartItem = new CartItem { Item = product, Quantity = 2 };

            var price = cartItem.Price;

            price.Should().Be(20);
        }

        [Fact]
        public void CartItem_Saving_ShouldBeProductReducedByTimesQuantity()
        {
            var product = new StubProduct { ReducedBy = 10 };
            var cartItem = new CartItem { Item = product, Quantity = 2 };

            var saving = cartItem.Saving;

            saving.Should().Be(20);
        }

        [Fact]
        public void CartItem_Tax_ShouldBeProductTaxTimesQuantity()
        {
            var product = new StubProduct {Tax = 2};
            var cartItem = new CartItem { Item = product, Quantity = 2 };

            var tax = cartItem.Tax;

            tax.Should().Be(4);
        }

        [Fact]
        public void CartItem_CurrentlyAvailable_ShouldBeFalseIfStockLevelsAreTooLow()
        {
            var product = new StubProduct { Available = false};
            var cartItem = new CartItem { Item = product, Quantity = 2 };

            var currentlyAvailable = cartItem.CurrentlyAvailable;

            currentlyAvailable.Should().BeFalse();
        }

        [Fact]
        public void CartItem_CurrentlyAvailable_ShouldBeTrueIfProductIsAvailableForQuantity()
        {
            var product = new StubProduct { Available = true};
            var cartItem = new CartItem { Item = product, Quantity = 2 };

            var currentlyAvailable = cartItem.CurrentlyAvailable;

            currentlyAvailable.Should().BeTrue();
        }

        [Fact]
        public void CartItem_CurrentlyAvailable_ShouldBeTrueIfStockLevelsAreHighEnough()
        {
            var product = new StubProduct {  Available = true };
            var cartItem = new CartItem { Item = product, Quantity = 2 };

            var currentlyAvailable = cartItem.CurrentlyAvailable;

            currentlyAvailable.Should().BeTrue();
        }

        [Fact]
        public void CartItem_TaxRatePercentage_ShouldReturnTheTaxRateInPercentage()
        {
            var product = new StubProduct { TaxRatePercentage = 20 };
            var cartItem = new CartItem { Item = product, Quantity = 2 };

            var taxRatePercentage = cartItem.TaxRatePercentage;

            taxRatePercentage.Should().Be(20);
        }
    }

    public class StubProduct : ICanAddToCart
    {
        public string SKU { get; set; }
        public decimal TaxRatePercentage { get; set; }
        public decimal Price { get; set; }
        public decimal ReducedBy { get; set; }
        public decimal Tax { get; set; }
        public bool CanBuy(int quantity)
        {
            return Available;
        }

        public decimal PricePreTax { get; set; }
        public decimal Weight { get; set; }

        public bool Available { get; set; }
    }
}