using Banks.Tools;

namespace Banks.Models
{
    public class Address
    {
        public Address(string street, int numberHouse, int numberApartment)
        {
            if (string.IsNullOrEmpty(street))
                throw new BanksException("String is null or empty");
            if (numberHouse < 1)
                throw new BanksException("Wrong number of house");
            if (numberApartment < 1)
                throw new BanksException("Wrong number of apartment");

            Street = street;
            NumberHouse = numberHouse;
            NumberApartment = numberApartment;
        }

        public string Street { get; }
        public int NumberHouse { get; }
        public int NumberApartment { get; }
    }
}