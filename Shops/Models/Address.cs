using Shops.Tools;

namespace Shops.Models
{
    public class Address
    {
        public Address(string street, int numberHouse)
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