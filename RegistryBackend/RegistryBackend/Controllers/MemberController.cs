using Microsoft.AspNetCore.Mvc;
using RegistryBackend.BusinessModel;
using RegistryBackend.Model;

namespace RegistryBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : RegistryControllerBase
    {
        public MemberController(RegistryDb context) : base(context) { }

        [HttpGet]
        [Route("{email}")]
        public IQueryable<Member> GetMemberByEmail(string email)
        {
            return _context.Members.Where(member => member.Email == email);
        }
    }
}
