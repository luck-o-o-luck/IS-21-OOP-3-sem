using System;
using Shops.Tools;

namespace Shops.Models
{
    public class Address
    {
        public Address(string street, int numberHouse)
        {
            if (string.IsNullOrEmpty(street))
                throw new ShopsException("String is null or empty");
            if (numberHouse < 1)
                throw new ShopsException("Wrong number of house");

            Street = street;
            NumberHouse = numberHouse;
        }

        public string Street { get; }
        public int NumberHouse { get; }

        public override bool Equals(object obj)
        {
            var otherAddress = obj as Address;

            if (otherAddress is null)
                return false;

            return otherAddress.Street == Street && otherAddress.NumberHouse == NumberHouse;
        }

        public override int GetHashCode()
        {
            return (Street + " " + Convert.ToString(NumberHouse)).GetHashCode();
        }
    }
}