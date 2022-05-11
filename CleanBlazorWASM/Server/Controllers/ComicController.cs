using CleanBlazorWASM.Server.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CleanBlazorWASM.Server.Controllers
{
    [Route("api/comics")]
    [ApiController]
    public class ComicController : ControllerBase
    {
        private readonly DataContext _context;

        public ComicController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var comics = await _context.Comics.ToListAsync();
            return Ok(comics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comic>> GetSingleComic(int id)
        {
            var comic = await _context.Comics
                  .FirstOrDefaultAsync(h => h.Id == id);

            if (comic == null)
            {
                return NotFound("Sorry, no comic here. :/");
            }
            return Ok(comic);
        }

		[HttpPost]
		public async Task<ActionResult<List<Comic>>> CreateComic(Comic comic)
		{
			_context.Comics.Add(comic);
			await _context.SaveChangesAsync();

			return Ok(await GetDbComics());
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<List<Comic>>> UpdateComic(Comic comic, int id)
		{
			var dbComic = await _context.Comics
				.FirstOrDefaultAsync(sh => sh.Id == id);
			if (dbComic == null)
				return NotFound("Sorry, but no comic for you. :/");

			dbComic.Name = comic.Name;

			await _context.SaveChangesAsync();

			return Ok(await GetDbComics());
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<List<Comic>>> DeleteComic(Comic comic, int id)
		{
			var dbComic = await _context.Comics
				.FirstOrDefaultAsync(sh => sh.Id == id);
			if (dbComic == null)
				return NotFound("Sorry, but no comic for you. :/");

			_context.Comics.Remove(dbComic);
			await _context.SaveChangesAsync();

			return Ok(await GetDbComics());
		}

		private async Task<List<Comic>> GetDbComics()
		{
			return await _context.Comics.ToListAsync();
		}

		
	}
}
