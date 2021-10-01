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
            _shopManager.AddShop("Пятерочка", new Address("улица Декабристов", 14));
            _shopManager.AddProduct(_shopManager.FindShop(1), new Product("Банан", 123, 12));
            Assert.AreEqual(_shopManager.FindShop(1).Products[0].ProductName, "Банан");
            Assert.AreEqual(_shopManager.FindShop(1).Products[0].Price, 123);
            Assert.AreEqual(_shopManager.FindShop(1).Products[0].Quantity, 12);
        }

        [Test]
        public void AddProductToShop_ChangePriceProductInTheShop()
        {
            _shopManager = new ShopManager();
            _shopManager.AddShop("Пятерочка", new Address("улица Декабристов", 14));
            
            _shopManager.AddProduct(_shopManager.FindShop(1), new Product("Банан", 123, 12));
            Assert.AreEqual(_shopManager.FindShop(1).Products[0].Price, 123);
            
            _shopManager.ChangePriceProduct(_shopManager.FindShop(1), new Product("Банан"), 23);
            
            Assert.AreEqual(_shopManager.FindShop(1).Products[0].Price, 23);
        }

        [Test]
        public void ShopWithCheapestPrice_ShopIsCorrect()
        {
            _shopManager = new ShopManager();
            
            _shopManager.AddShop("Пятерочка", new Address("улица Декабристов", 14));
            _shopManager.AddProduct(_shopManager.FindShop(1), new Product("Банан", 123, 12));
            
            _shopManager.AddShop("Пятерочка", new Address("большая Пушкарская", 12));
            _shopManager.AddProduct(_shopManager.FindShop(2), new Product("Банан", 23, 12));
            
            _shopManager.AddShop("Пятерочка", new Address("улица Саблинская", 11));
            _shopManager.AddProduct(_shopManager.FindShop(3), new Product("Банан", 123, 12));

            List<Product> products = new List<Product>();
            products.Add(new Product("Банан"));

            Assert.AreEqual(_shopManager.FindShopWithCheapestProducts(products), _shopManager.FindShop(2));
        }

        [Test]
        public void PurchaseOfProducts_TheNumberOfProductsAndMoneyHasChanged()
        {
            _shopManager = new ShopManager();
            _shopManager.AddShop("Пятерочка", new Address("улица Декабристов", 14));
            _shopManager.AddProduct(_shopManager.FindShop(1), new Product("Банан", 123, 12));

            _shopManager.AddCustomer("Васютинская Ксения", 12345);

            _shopManager.AddCustomersProduct(_shopManager.FindCustomer("Васютинская Ксения"), new Product("Банан", 2));
            _shopManager.PurchaseGoods(_shopManager.FindShop(1), _shopManager.FindCustomer("Васютинская Ксения"));
            
            Assert.AreEqual(_shopManager.FindShop(1).Products[0].Quantity, 10);
            Assert.AreEqual(_shopManager.FindCustomer("Васютинская Ксения").Money, 12099);
        }
        
    }
}