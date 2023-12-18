using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using RegistryBackend.BusinessModel;
using RegistryBackend.Model;
using System.Text.Json;

namespace RegistryBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : RegistryControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(RegistryDb context, ICheckoutService checkoutService) : base (context) 
        {
            _checkoutService = checkoutService;
        }

        [HttpGet]
        [Route("{sessionGuid}")]
        public Receipt Checkout(Guid sessionGuid)
        {
            return _checkoutService.GetReceiptForFront(_checkoutService.CheckOut(sessionGuid));
        }

        [HttpGet]
        [Route("{sessionGuid}&{email}")]
        public Receipt Checkout(Guid sessionGuid, string email)
        {
            return _checkoutService.GetReceiptForFront(_checkoutService.CheckOut(sessionGuid, email));
        }

        [HttpPost]
        [Route("Pay/{sessionGuid}")]
        public Receipt Pay(Guid sessionGuid)
        {
            return _checkoutService.GetReceiptForFront(_checkoutService.Pay(sessionGuid));
        }

        [HttpPost]
        [Route("Pay/{sessionGuid}&{id}")]
        public Receipt Pay(Guid sessionGuid, Guid id)
        {
            Member? member = _context.Members.FirstOrDefault(m => m.UUID == id.ToString());
            if (member == null)
                return _checkoutService.GetReceiptForFront(_checkoutService.Pay(sessionGuid));
            return _checkoutService.GetReceiptForFront(_checkoutService.Pay(sessionGuid, member.Email));
        }
    }
}
