namespace RegistryBackend.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public Guid SessionGuid { get; set; }
        public DateTime ExpiryDate { get; set; }
        public ICollection<ProductCart> ProductCarts { get; set; }
    }
}
