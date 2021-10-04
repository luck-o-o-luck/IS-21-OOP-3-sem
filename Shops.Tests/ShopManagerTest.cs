using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Models;
using Shops.Services;
using Shops.Tools;
using NUnit.Framework;

namespace Shops.Tests
{
    public class Tests
    {
        private IShopManager _shopManager;
        
        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _shopManager = null;
        }

        [Test]
        public void AddProductToShop_ShopHasProduct()
        {
            _shopManager = new ShopManager();
            Shop shop1 = _shopManager.AddShop("Пятерочка", new Address("улица Декабристов", 14));
            _shopManager.AddShopsProduct(_shopManager.FindShop(1), new Product("Банан", 123, 12));
            Assert.AreEqual(shop1.Products[0].ProductName, "Банан");
            Assert.AreEqual(shop1.Products[0].Price, 123);
            Assert.AreEqual(shop1.Products[0].Quantity, 12);
        }

        [Test]
        public void AddProductToShop_ChangePriceProductInTheShop()
        {
            _shopManager = new ShopManager();
            Shop shop1 = _shopManager.AddShop("Пятерочка", new Address("улица Декабристов", 14));
            
            _shopManager.AddShopsProduct(_shopManager.FindShop(1), new Product("Банан", 123, 12));
            Assert.AreEqual(shop1.Products[0].Price, 123);
            
            _shopManager.ChangePriceProduct(shop1, new Product("Банан"), 23);
            
            Assert.AreEqual(shop1.Products[0].Price, 23);
        }

        [Test]
        public void ShopWithCheapestPrice_ShopIsCorrect()
        {
            _shopManager = new ShopManager();
            
            Shop shop1 = _shopManager.AddShop("Пятерочка", new Address("улица Декабристов", 14));
            _shopManager.AddShopsProduct(_shopManager.FindShop(1), new Product("Банан", 123, 12));
            
            Shop shop2 = _shopManager.AddShop("Пятерочка", new Address("большая Пушкарская", 12));
            _shopManager.AddShopsProduct(_shopManager.FindShop(2), new Product("Банан", 23, 12));
            
            Shop shop3 = _shopManager.AddShop("Пятерочка", new Address("улица Саблинская", 11));
            _shopManager.AddShopsProduct(_shopManager.FindShop(3), new Product("Банан", 123, 12));

            List<Product> products = new List<Product>();
            products.Add(new Product("Банан"));

            Assert.AreEqual(_shopManager.FindShopWithCheapestProducts(products), shop2);
        }

        [Test]
        public void PurchaseOfProducts_TheNumberOfProductsAndMoneyHasChanged()
        {
            _shopManager = new ShopManager();
            Shop shop1 = _shopManager.AddShop("Пятерочка", new Address("улица Декабристов", 14));
            _shopManager.AddShopsProduct(_shopManager.FindShop(1), new Product("Банан", 123, 12));

            Customer customer = _shopManager.AddCustomer("Васютинская Ксения", 12345);

            _shopManager.AddCustomersProduct(customer, new Product("Банан", 2));
            _shopManager.PurchaseGoods(shop1, customer);
            
            Assert.AreEqual(shop1.Products[0].Quantity, 10);
            Assert.AreEqual(customer.Money, 12099);
        }
        
    }
}