namespace RegistryBackend.Model
{
    public class ProductDTO : Product
    {
        public double? SalePrice { get; set; }
        public int Quantity { get; set; }


        public ProductDTO(Product product)
        {
            Name = product.Name;
            DepartementId = product.DepartementId;
            Description = product.Description;
            Price = product.Price;
            ImageURL = product.ImageURL;
            Id = product.Id;
            SaleId = product.SaleId;
            Departement = product.Departement;
            Sale = product.Sale;
        }
    }
}
