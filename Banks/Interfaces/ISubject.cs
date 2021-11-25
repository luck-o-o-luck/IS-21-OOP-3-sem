using Banks.Models.Clients;

namespace Banks.Interfaces
{
    public interface ISubject
    {
        void Attach(Client client);
        void Detach(Client client);
        void Notify(string updateValue, decimal newValue);
    }
}