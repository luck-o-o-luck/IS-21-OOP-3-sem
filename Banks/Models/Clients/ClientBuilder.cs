using Backups.Tools;

namespace Banks.Models.Clients
{
    public class ClientBuilder
    {
        private string _fullName;
        private string _passport;
        private string _numberPhone;
        private Address _address;
        private Client _client;

        public ClientBuilder SetFullName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new BackupsException("Name is null");

            _fullName = name;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            if (string.IsNullOrEmpty(passport))
                throw new BackupsException("Passport is null");

            _passport = passport;
            return this;
        }

        public ClientBuilder SetNumberPhone(string numberPhone)
        {
            if (string.IsNullOrEmpty(numberPhone))
                throw new BackupsException("Phone number is null");

            _numberPhone = numberPhone;
            return this;
        }

        public ClientBuilder SetAddress(Address address)
        {
            _address = address ?? throw new BackupsException("Address is null");
            return this;
        }

        public Client Create()
        {
            if (string.IsNullOrEmpty(_fullName))
                throw new BackupsException("Name doesn't exist");
            if (string.IsNullOrEmpty(_passport))
                throw new BackupsException("Passport doesn't exist");

            _client = new Client(_fullName, _passport, _numberPhone, _address);
            return _client;
        }
    }
}