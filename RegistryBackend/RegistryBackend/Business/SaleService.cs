using RegistryBackend.BusinessModel;
using RegistryBackend.Model;

namespace RegistryBackend.Business
{
    public class SaleService : ISaleService
    {
        private RegistryDb _context;

        public SaleService(RegistryDb context)
        {
            _context = context;
        }

        public IEnumerable<ProductDTO> ComputeReducedPriceForList(IEnumerable<Product> products)
        {
            ICollection<ProductDTO> computedProducts = new List<ProductDTO>();

            foreach (Product product in products)
            {
                computedProducts.Add(ComputeSingleReducedPrice(product));
            }

            return computedProducts;
        }

        public ProductDTO ComputeSingleReducedPrice(Product product)
        {
            ProductDTO result = new ProductDTO(product);
            if (result.SaleId.HasValue)
            {
                result.SalePrice = Math.Round(((result.Price - (result.Price * result.Sale?.PercentageOff).Value) * 100) / 100, 2);
            }
            return result;
        }

        public double ApplyMemberDiscount(double amountToDiscount)
        {
            return amountToDiscount * 0.05;
        }
    }
}
