using Microsoft.Identity.Client;
using RegistryBackend.Model;

namespace RegistryBackend.BusinessModel
{
    public interface ICheckoutService
    {
        public Receipt CheckOut(Guid sessionGuid);
        public Receipt CheckOut(Guid cartGuid, string email);
        public Receipt GetReceiptForFront(Receipt receipt);
        public Receipt Pay(Guid sessionGuid);
        public Receipt Pay(Guid sessionGuid, string email);
    }
}
