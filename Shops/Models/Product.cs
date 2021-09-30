using Shops.Tools;

namespace Shops.Models
{
    public class Product
    {
        public Product(string name, decimal price, int quantity)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("String is null or empty");

            ProductName = name;
            Price = price;
            Quantity = quantity;
        }

        public Product(string name, int quantity)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("String is null or empty");

            ProductName = name;
            Quantity = quantity;
        }

        public Product(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ShopsException("String is null or empty");

            ProductName = name;
        }

        public string ProductName { get; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; } = 0;

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ShopsException("The number can't be less than zero");

            this.Price = newPrice;
        }

        public void ChangeQuantityDelivery(int quantity)
        {
            if (quantity < 0)
                throw new ShopsException("The number can't be less than zero");

            this.Quantity += quantity;
        }

        public void ChangeQuantityWhenBuying(int quantity)
        {
            if (this.Quantity < quantity)
                throw new ShopsException("The number can't be more than we have");

            this.Quantity -= quantity;
        }
    }
}