using RegistryBackend.BusinessModel;
using RegistryBackend.Model;

namespace RegistryBackend.Business
{
    public class CheckoutService : ICheckoutService
    {
        private readonly RegistryDb _context;
        private readonly ICartService _cartService;
        private readonly ISaleService _saleService;
        public CheckoutService(RegistryDb context, ICartService cartService, ISaleService saleService)
        {
            _context = context;
            _cartService = cartService;
            _saleService = saleService;
        }

        public Receipt CheckOut(Guid cartGuid)
        {
            return CheckOut(cartGuid, "");
        }

        public Receipt CheckOut(Guid cartGuid, string email)
        {
            Cart cart = _cartService.GetCart(cartGuid);
            Member? member = _context.Members.FirstOrDefault(m => m.Email == email);
            Receipt receipt = new Receipt();
            receipt.Id = 0;
            receipt.Cart = cart;

            foreach (ProductCart pc in receipt.Cart.ProductCarts)
            {
                ProductDTO computedProduct = _saleService.ComputeSingleReducedPrice(pc.Product);
                computedProduct.Quantity = pc.Quantity;
                if (computedProduct.SaleId.HasValue)
                {
                    ComputePrice(computedProduct, computedProduct.SalePrice, computedProduct.Quantity, receipt);
                }
                else
                {
                    ComputePrice(computedProduct, computedProduct.Price, computedProduct.Quantity, receipt);
                }
            }

            receipt.SousTotal = Math.Round((receipt.SousTotal * 100) / 100, 2);
            receipt.Tps = Math.Round((receipt.Tps * 100) / 100, 2);
            receipt.Tvq = Math.Round((receipt.Tvq * 100) / 100, 2);

            receipt.Total = receipt.SousTotal + receipt.Tps + receipt.Tvq;

            if (member != null)
            {
                receipt.Member = member;
                receipt.Total -= _saleService.ApplyMemberDiscount(receipt.Total);
            }

            receipt.Total = Math.Round((receipt.Total * 100) / 100, 2);

            return receipt;
        }

        public Receipt GetReceiptForFront(Receipt receipt)
        {
            ReceiptDTO receiptToSend = new ReceiptDTO(receipt);
            receiptToSend.Cart = _cartService.GetCartForFront(receipt.Cart.SessionGuid);
            if (receiptToSend.Member != null)
            {
                receiptToSend.MemberDiscount = _saleService.ApplyMemberDiscount(receiptToSend.Total);
            }
            return receiptToSend;
        }

        public Receipt Pay(Guid sessionGuid)
        {
            return Pay(sessionGuid, "");
        }

        public Receipt Pay(Guid sessionGuid, string email)
        {
            Receipt receipt = CheckOut(sessionGuid, email);
            _context.Receipts.Add(receipt);
            _context.SaveChanges();
            return receipt;
        }

        private void ComputePrice(Product product, double? price, int quantity, Receipt receipt)
        {
            double priceToCalculate = price.Value * quantity;

            receipt.SousTotal += priceToCalculate;

            if (product.Departement.Taxable)
            {
                receipt.Tps += priceToCalculate * 0.05;
                receipt.Tvq += priceToCalculate * 0.09975;
            }
        }

    }
}
