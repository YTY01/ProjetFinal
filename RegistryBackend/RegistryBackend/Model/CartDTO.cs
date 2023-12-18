namespace RegistryBackend.Model
{
    public class CartDTO : Cart
    {
        public ICollection<ProductDTO> Products { get; set; }
    }
}
