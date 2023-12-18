using Microsoft.EntityFrameworkCore;
using RegistryBackend.Model;
using System.Collections.Generic;

namespace RegistryBackend.DatabaseSeeding
{
    public class DbInitializer
    {
        internal static void Initialize(RegistryDb dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            if (!dbContext.Sales.Any())
            {
                dbContext.Sales.AddRange(new List<Sale>()
                {
                    new Sale()
                    {
                        Id = 1,
                        PercentageOff = 0.15,
                    },
                    new Sale()
                    {
                        Id = 2,
                        PercentageOff = 0.30,
                    },
                    new Sale()
                    {
                        Id = 3,
                        PercentageOff = 0.45,
                    }
                });
            }

            if (!dbContext.Departements.Any())
            {
                dbContext.Departements.AddRange(new List<Departement>()
                {
                    new Departement()
                    {
                        Id = 1,
                        Name = "Boucherie",
                        Description = "Département des viandes",
                        Taxable = false
                    },
                    new Departement()
                    {
                        Id = 2,
                        Name = "Boulangerie",
                        Description = "Département de la boulangerie",
                        Taxable = false
                    },
                    new Departement()
                    {
                        Id = 3,
                        Name = "Fruits&Légumes",
                        Description = "Département des fruits et légumes",
                        Taxable = false
                    },
                    new Departement()
                    {
                        Id = 4,
                        Name = "Épicerie",
                        Description = "Département des produits emballés grand marché",
                        Taxable = true
                    },
                });
            }

            dbContext.SaveChanges();

            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(new List<Product>()
                {
                    new Product()
                    {
                        DepartementId = 1, 
                        SaleId = 1,
                        Name = "Ailes de poulet",
                        ImageURL = "https://www.groskash.com/5773/ailes-de-poulet-yesh-entre-800g-et-950g-casher-ihoud-klp-shritta-loubavitch.jpg",
                        Price = 10.99,
                        Description = ""
                    },
                    new Product()
                    {
                        DepartementId = 2,
                        Sale = null,
                        Name = "Pain blanc Villagio",
                        ImageURL = "https://marchenuvo.ca/cdn/shop/products/0006107777124_x700.jpg?v=1568958518",
                        Price = 3.99,
                        Description = ""
                    }
                    ,
                    new Product()
                    {
                        DepartementId = 4,
                        Sale = null,
                        Name = "Barres croquantes val nature",
                        ImageURL = "https://www.valnature.ca/wp-content/uploads/sites/3/2019/05/Val-Nature-Barres-Granola-Croquantes-Avoine-et-Miel-460x460.png",
                        Price = 4.99,
                        Description = ""
                    },
                    new Product()
                    {
                        DepartementId = 3,
                        Sale = null,
                        Name = "Bananes",
                        ImageURL = "https://images3.alphacoders.com/658/658610.jpg",
                        Price = 1.99,
                        Description = ""
                    },
                    new Product()
                    {
                        DepartementId = 4,
                        Sale = null,
                        Name = "Sardines",
                        ImageURL = "https://fishersupermarket.ph/wp-content/uploads/2020/11/4800134103044-600x600.jpg",
                        Price = 9.99,
                        Description = ""
                    },
                    new Product()
                    {
                        DepartementId = 1,
                        SaleId = 3,
                        Name = "Boeuf haché",
                        ImageURL = "https://thumbs.dreamstime.com/z/boeuf-hach%C3%A9-13135122.jpg",
                        Price = 13.99,
                        Description = ""
                    }
                });
            }

            if (!dbContext.Members.Any())
            {
                dbContext.Members.AddRange(new List<Member>()
                {
                    new Member()
                    {
                        FirstName = "Étienne",
                        LastName = "Guimond",
                        Email = "eguimon@uqac.ca",
                        UUID = Guid.NewGuid().ToString(),
                    }
                });
            }

            dbContext.SaveChanges();
        }
    }
}
