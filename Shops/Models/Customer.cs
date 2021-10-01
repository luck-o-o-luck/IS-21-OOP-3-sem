using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Models
{
    public class Customer
    {
        private List<Product> _products;

        public Customer(string name, decimal money, int id)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("String is null or empty");

            Name = name;
            Money = money;
            Id = id;
            _products = new List<Product>();
        }

        public decimal Money { get; private set; }
        public int Id { get; }
        public string Name { get; }

        public IReadOnlyList<Product> Products => _products;

        public void AddCustomersProduct(Product product)
        {
            _products.Add(product);
        }

        public void DecreaseAmountMoney(decimal priceProducts)
        {
            if (Money < priceProducts)
                throw new ShopsException("This customer can't buy this product");

            Money -= priceProducts;
        }
    }
}