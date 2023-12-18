using Microsoft.AspNetCore.Mvc;

namespace RegistryBackend.BusinessModel
{
    [ApiController]
    public class RegistryControllerBase : ControllerBase
    {
        protected readonly RegistryDb _context;
        public RegistryControllerBase(RegistryDb context)
        {
                _context = context;
        }
    }
}
