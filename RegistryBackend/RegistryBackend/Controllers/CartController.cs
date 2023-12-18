using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistryBackend.Business;
using RegistryBackend.BusinessModel;
using RegistryBackend.Model;

namespace RegistryBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : RegistryControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(RegistryDb context, ICartService cartService) : base(context)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("Get")]
        public Cart GetCart(Guid sessionGuid)
        {
            return _cartService.GetCartForFront(sessionGuid);
        }

        [HttpGet]
        [Route("Create")]
        public Cart CreateCart()
        {
            return _cartService.GetCart();
        }

        [HttpPost]
        [Route("AddItem")]
        [AllowAnonymous]
        public async Task<Cart> AddItemToCart([FromBody] JsonElement json)
        {
            CartDTO? parsed = JsonSerializer.Deserialize<CartDTO>(json.GetRawText());
            
            Product productToAdd = _context.Products.First(product => product.Id == parsed.Id);
            return await _cartService.AddItemToCart(parsed.SessionGuid, productToAdd);
        }

        [HttpDelete]
        [Route("RemoveItem/{sessionGuid}&{id}")]
        [AllowAnonymous]
        public Cart RemoveItemFromCart(Guid sessionGuid, int id)
        {
            Product productToDelete = _context.Products.First(product => product.Id == id);
            return _cartService.RemoveItemFromCart(sessionGuid, productToDelete);
        }

        [HttpDelete]
        [Route("Clear/{sessionGuid}")]
        [AllowAnonymous]
        public Cart ClearCart(Guid sessionGuid)
        {
            return _cartService.ClearCart(sessionGuid);
        }
    }
}
