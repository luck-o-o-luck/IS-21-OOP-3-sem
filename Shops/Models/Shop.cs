using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class Shop
    {
        private List<Product> _products;

        public Shop(string name, Address address, int id)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("String is null or empty");

            ShopName = name;
            Address = address;
            Id = id;
            _products = new List<Product>();
        }

        public string ShopName { get; }
        public Address Address { get; }
        public int Id { get; }
        public IReadOnlyList<Product> Products => _products;

        public void AddShopsProducts(Product product)
        {
            _products.Add(product);
        }

        public bool ContainsAllProduct(IReadOnlyList<Product> products)
        {
            if (products.Any(product => _products.All(x => x.ProductName != product.ProductName)))
                return false;

            return true;
        }

        public bool ContainsEnoughProducts(IReadOnlyList<Product> products)
        {
            if (products.Any(product =>
                _products.All(x => x.ProductName != product.ProductName || x.Quantity < product.Quantity)))
                return false;

            return true;
        }
    }
}