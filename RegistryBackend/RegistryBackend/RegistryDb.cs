using Microsoft.EntityFrameworkCore;
using RegistryBackend.Model;

namespace RegistryBackend
{
    public class RegistryDb : DbContext
    {
        public RegistryDb(DbContextOptions<RegistryDb> options) : base(options) { }
        public DbSet<Departement> Departements { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Receipt> Receipts { get; set; } = null!;
        public DbSet<ProductCart> ProductCarts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCart>()
                .HasKey(pc => new { pc.ProductId, pc.CartId });
            modelBuilder.Entity<ProductCart>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCarts)
                .HasForeignKey(bc => bc.ProductId);
            modelBuilder.Entity<ProductCart>()
                .HasOne(bc => bc.Cart)
                .WithMany(c => c.ProductCarts)
                .HasForeignKey(bc => bc.CartId);
        }
    }
}
