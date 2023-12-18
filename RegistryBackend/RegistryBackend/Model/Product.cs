namespace RegistryBackend.Model
{
    public class Product
    {
        public int Id { get; set; }
        public int DepartementId { get; set; }
        public int? SaleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public Departement Departement { get; set; } = null!;
        public Sale? Sale { get; set; }
        public ICollection<ProductCart> ProductCarts { get; set; }
    }
}
