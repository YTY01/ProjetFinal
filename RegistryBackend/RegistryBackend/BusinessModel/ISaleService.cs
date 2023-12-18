using RegistryBackend.Model;

namespace RegistryBackend.BusinessModel
{
    public interface ISaleService
    {  
        public IEnumerable<ProductDTO> ComputeReducedPriceForList(IEnumerable<Product> product);
        public ProductDTO ComputeSingleReducedPrice(Product product);
        public double ApplyMemberDiscount(double amountToDiscount);
    }
}
