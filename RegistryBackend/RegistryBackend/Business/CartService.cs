using Microsoft.EntityFrameworkCore;
using RegistryBackend.BusinessModel;
using RegistryBackend.Model;

namespace RegistryBackend.Business
{
    public class CartService : ICartService
    {
        private readonly RegistryDb _context;
        private readonly ISaleService _saleService;
        public CartService(RegistryDb context, ISaleService saleService)
        {
            _context = context;
            _saleService = saleService;
        }

        public Cart GetCart()
        {
            return PrepareCartForFront(GetCart(CreateCart()));
        }

        public Cart GetCart(Guid sessionGuid)
        {
            Cart cart = _context.Carts.Include(cart => cart.ProductCarts)
                .ThenInclude(productCart => productCart.Product)
                .ThenInclude(product => product.Departement)
                .Include(cart => cart.ProductCarts)
                .ThenInclude(productCart => productCart.Product)
                .ThenInclude(product => product.Sale)
                .First(cart => cart.SessionGuid == sessionGuid);
            return cart;
        }

        public CartDTO GetCartForFront(Guid sessionGuid)
        {
            Cart cart = _context.Carts.Include(cart => cart.ProductCarts)
                .ThenInclude(productCart => productCart.Product)
                .ThenInclude(product => product.Departement)
                .Include(cart => cart.ProductCarts)
                .ThenInclude(productCart => productCart.Product)
                .ThenInclude(product => product.Sale)
                .First(cart => cart.SessionGuid == sessionGuid);

            return PrepareCartForFront(cart);
        }

        public async Task<Cart> AddItemToCart(Guid cartGuid, Product product)
        {
            Cart cart = GetCart(cartGuid);

            if (cart.ProductCarts.Any(pc => pc.CartId == cart.Id && pc.ProductId == product.Id))
            {
                cart.ProductCarts.First(pc => pc.CartId == cart.Id && pc.ProductId == product.Id).Quantity += 1;
            }
            else
            {
                cart.ProductCarts.Add(new ProductCart()
                {
                    Cart = cart,
                    CartId = cart.Id,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = 1
                });
            }

            _context.Update(cart);
            await _context.SaveChangesAsync();
            return PrepareCartForFront(cart);
        }

        public Cart RemoveItemFromCart(Guid cartGuid, Product product)
        {
            Cart cart = GetCart(cartGuid);
            if (cart.ProductCarts.Any(pc => pc.ProductId == product.Id))
            {
                cart.ProductCarts.First(pc => pc.ProductId == product.Id).Quantity -= 1;
                if (cart.ProductCarts.First(pc => pc.ProductId == product.Id).Quantity == 0)
                {
                    cart.ProductCarts.Remove(cart.ProductCarts.First(pc => pc.ProductId == product.Id));
                }
            }

            _context.Carts.Update(cart);
            _context.SaveChanges();

            return PrepareCartForFront(cart);
        }

        public Cart ClearCart(Guid cartGuid)
        {
            Cart cart = GetCart(cartGuid);
            foreach (ProductCart pc in cart.ProductCarts)
            {
                cart.ProductCarts.Remove(pc);
            }
            _context.Update(cart);
            _context.SaveChanges();
            return PrepareCartForFront(cart);
        }

        private CartDTO PrepareCartForFront(Cart cart)
        {

            CartDTO cartToSend = new CartDTO()
            {
                Products = new List<ProductDTO>(),
                SessionGuid = cart.SessionGuid,
                Id = cart.Id
            };

            foreach (ProductCart pc in cart.ProductCarts)
            {
                ProductDTO newProduct = _saleService.ComputeSingleReducedPrice(pc.Product);
                newProduct.Quantity = pc.Quantity;
                cartToSend.Products.Add(newProduct);
            }

            return cartToSend;
        }

        private Guid CreateCart()
        {
            Cart cart = new Cart
            {
                SessionGuid = Guid.NewGuid(),
                ExpiryDate = DateTime.Now + TimeSpan.FromDays(1),
                ProductCarts = new List<ProductCart>()
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();

            return cart.SessionGuid;
        }
    }
}
