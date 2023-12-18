using Microsoft.AspNetCore.Mvc;
using RegistryBackend;
using RegistryBackend.BusinessModel;
using RegistryBackend.Model;

namespace RegistryBackend.Controllers;
    [ApiController]
    [Route("api/[controller]")]
    public class DepartementController : RegistryControllerBase
    {


        public DepartementController(RegistryDb context) : base(context) { }

        [HttpGet]
        public IEnumerable<Departement> GetDepartements()
        {
            return _context.Departements;
        }
    }
