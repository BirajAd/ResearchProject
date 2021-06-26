using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPHost.Data;

namespace RPHost.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchController : ControllerBase
    {

        private readonly DataContext _context;
        public ResearchController(DataContext context)
        {
            _context = context;
        }

        // GET api/research
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var researches = await _context.Researches.ToListAsync();

            return Ok(researches);
        }

        // GET api/research/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value = await _context.Researches.FirstOrDefaultAsync(x => x.ResearchId == id);
            return Ok(value);
        }

        // POST api/research
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/research/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/research/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
