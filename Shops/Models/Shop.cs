using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Models
{
    public class Shop
    {
        private List<Product> _products;

        public Shop(string name, Adress location, int id)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("String is null or empty");

            ShopName = name;
            Location = location;
            Id = id;
            _products = new List<Product>();
        }

        public string ShopName { get; }
        public Adress Location { get; }
        public int Id { get; }
        public IReadOnlyList<Product> Products => _products;

        public void AddShopsProducts(Product product)
        {
            _products.Add(product);
        }
    }
}