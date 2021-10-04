using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Models;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager : IShopManager
    {
        private List<Shop> _shops;
        private List<Customer> _customers;
        private int _customerId = 1;
        private int _shopId = 1;

        public ShopManager()
        {
            _customers = new List<Customer>();
            _shops = new List<Shop>();
        }

        public bool ShopsProductExists(Shop shop, string name) => shop.Products.Any(product => product.ProductName == name);
        public bool CustomersProductExists(Customer customer, string name) => customer.Products.Any(product => product.ProductName == name);
        public bool AddressExists(Address address) => _shops.Any(shop => shop.Address.Equals(address));

        public Customer AddCustomer(string name, decimal money)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("Customer should has name");

            var customer = new Customer(name, money, _customerId);
            _customerId++;

            _customers.Add(customer);

            return customer;
        }

        public Shop AddShop(string name, Address address)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("Customer should has name");
            if (AddressExists(address))
                throw new ShopsException("Address exists");

            var shop = new Shop(name, address, _shopId);
            _shopId++;

            _shops.Add(shop);

            return shop;
        }

        public Product GetProduct(Shop shop, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("String is null or empty");
            if (shop is null)
                throw new ShopsException("Didn't enter the shop");

            Product selectedProduct = shop.Products.Single(x => x.ProductName == name);

            return selectedProduct;
        }

        public Shop DeliveryGoods(Shop shop, List<Product> products)
        {
            foreach (Product product in products)
            {
                Product selectedProduct = shop.Products.SingleOrDefault(x => x.ProductName == product.ProductName);
                if (selectedProduct is null)
                {
                    shop.AddShopsProducts(product);
                    continue;
                }

                selectedProduct.ChangeQuantityDelivery(product.Quantity);
            }

            return shop;
        }

        public Shop AddShopsProduct(Shop shop, Product product)
        {
            if (ShopsProductExists(shop, product.ProductName))
                throw new ShopsException("This product already exists");

            shop.AddShopsProducts(product);

            return shop;
        }

        public Customer AddCustomersProduct(Customer customer, Product product)
        {
            if (CustomersProductExists(customer, product.ProductName))
                throw new ShopsException("This product already exists");

            customer.AddCustomersProduct(product);

            return customer;
        }

        public Shop FindShop(int id)
        {
            Shop selectedShop = _shops.SingleOrDefault(shop => shop.Id == id);

            if (selectedShop is null)
                throw new ShopsException("This shop doesn't exists");

            return selectedShop;
        }

        public Customer FindCustomer(string name)
        {
            if (_customers.All(customer => customer.Name != name))
                throw new ShopsException("This customer doesn't exists");

            return _customers.Single(customer => customer.Name == name);
        }

        public Product ChangePriceProduct(Shop shop, Product product, decimal newPrice)
        {
            if (product is null)
                throw new ShopsException("Didn't enter the product");
            if (shop is null)
                throw new ShopsException("Didn't enter the shop");

            Product selectedProduct = shop.Products.Single(x => x.ProductName == product.ProductName);
            selectedProduct.ChangePrice(newPrice);

            return selectedProduct;
        }

        public Shop FindShopWithCheapestProducts(List<Product> products)
        {
            if (products is null)
                throw new ShopsException("List doesn't exists");

            var selectedShops = _shops.Where(shop => shop.ContainsAllProduct(products)).ToList();

            if (selectedShops.Count == 0)
                throw new ShopsException("Shop with this products doesn't exists");

            decimal sum = decimal.MaxValue;
            Shop selectedShop = null;

            foreach (Shop shop in selectedShops)
            {
                decimal sumProducts = products.Sum(product => shop.Products.Single(x => x.ProductName == product.ProductName).Price);

                if (sumProducts >= sum) continue;
                sum = sumProducts;
                selectedShop = shop;
            }

            return selectedShop;
        }

        public void PurchaseGoods(Shop shop, Customer customer)
        {
            if (!shop.ContainsAllProduct(customer.Products))
                throw new ShopsException("The store does not have the right product");

            if (!shop.ContainsEnoughProducts(customer.Products))
                throw new ShopsException("There aren't enough products in the store or there are none");

            decimal sum = customer.Products.Sum(product => product.Quantity * shop.Products.Single(x => x.ProductName == product.ProductName).Price);

            customer.DecreaseAmountMoney(sum);

            foreach (Product product in customer.Products)
            {
                Product selectedProduct = shop.Products.Single(x => x.ProductName == product.ProductName);
                selectedProduct.ChangeQuantityWhenBuying(product.Quantity);
            }
        }
    }
}