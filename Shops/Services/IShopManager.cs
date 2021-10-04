using System.Collections.Generic;
using Shops.Models;

namespace Shops.Services
{
    public interface IShopManager
    {
        Customer AddCustomer(string name, decimal money);
        Shop AddShop(string name, Address address);
        Product GetProduct(Shop shop, string name);

        Shop DeliveryGoods(Shop shop, List<Product> products);
        Shop AddShopsProduct(Shop shop, Product product);
        Customer AddCustomersProduct(Customer customer, Product product);
        Shop FindShop(int id);
        Customer FindCustomer(string name);
        Product ChangePriceProduct(Shop shop, Product product, decimal newPrice);
        Shop FindShopWithCheapestProducts(List<Product> products);

        void PurchaseGoods(Shop shop, Customer customer);
    }
}