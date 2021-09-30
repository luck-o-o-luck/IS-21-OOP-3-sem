using Shops.Tools;

namespace Shops.Models
{
    public class Adress
    {
        public Adress(string street, int numberHouse)
        {
            if (string.IsNullOrEmpty(street))
                throw new ShopsException("String is null or empty");

            Street = street;
            NumberHouse = numberHouse;
        }

        public string Street { get; }
        public int NumberHouse { get; }
    }
}