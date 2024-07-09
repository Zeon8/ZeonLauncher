using Launcher.Common.Models;
using Launcher.Web.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Launcher.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdatesController : ControllerBase
    {
        private readonly LauncherWebContext _context;

        public UpdatesController(LauncherWebContext context)
        {
            _context = context;
        }

        // GET: api/<UpdatesController>
        [HttpGet]
        public IEnumerable<Patch> Get()
        {
            return _context.Updates;
        }
    }
}
