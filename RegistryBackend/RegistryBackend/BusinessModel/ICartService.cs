using RegistryBackend.Model;

namespace RegistryBackend.BusinessModel
{
    public interface ICartService
    {
        public Cart GetCart();
        public Cart GetCart(Guid sessionGuid);
        public CartDTO GetCartForFront(Guid sessionGuid);
        public Task<Cart> AddItemToCart(Guid cartGuid, Product product);
        public Cart RemoveItemFromCart(Guid cartGuid, Product product);
        public Cart ClearCart(Guid cartGuid);
    }
}
