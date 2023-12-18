using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistryBackend.BusinessModel;
using RegistryBackend.Model;

namespace RegistryBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : RegistryControllerBase
    {
        private readonly ISaleService _saleService;
        public ProductController(RegistryDb context, ISaleService saleservice) : base(context)
        {
            _saleService = saleservice;
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _saleService.ComputeReducedPriceForList(_context.Products.Include(prod => prod.Departement).Include(prod => prod.Sale).ToList());
        }

        [HttpGet]
        [Route("GetByDepartements")]
        public IEnumerable<Product> GetProductsByDepartement(int id)
        {
            return _context.Products.Where(dep => dep.Departement.Id == id).Include(prod => prod.Departement).Include(prod => prod.Sale);
        }

        [HttpGet]
        [Route("Sales")]
        public IEnumerable<Product> GetProductsInSale()
        {
            return _context.Products.Where(product => product.Sale != null).Include(prod => prod.Departement).Include(prod => prod.Sale);
        }
    }
}
