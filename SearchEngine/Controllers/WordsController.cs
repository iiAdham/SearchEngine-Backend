using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Data;
using SearchEngine.Model;

namespace SearchEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Words/search?keyword=somevalue
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Word>>> SearchWords([FromQuery] string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest("Keyword is required.");
            }

            var results = await _context.Words
                .Where(w => w.Keyword.StartsWith(keyword))
                .ToListAsync();

            return Ok(results);
        }
    }
}
