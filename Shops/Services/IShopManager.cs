using System.Collections.Generic;
using Shops.Models;

namespace Shops.Services
{
    public interface IShopManager
    {
        Customer AddCustomer(string name, decimal money);
        Shop AddShop(string name, Adress adress);
        Product GetProduct(Shop shop, Product product);

        Shop DeliveryGoods(Shop shop, List<Product> products);
        Shop AddProduct(Shop shop, Product product);
        Customer AddCustomersProduct(Customer customer, Product product);
        Shop FindShop(int id);
        Customer FindCustomer(string name);
        Product ChangePriceProduct(Shop shop, Product product, decimal newPrice);
        Shop FindShopWithCheapestProducts(List<Product> products);

        void PurchaseGoods(Shop shop, Customer customer);
    }
}