using CleanBlazorWASM.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanBlazorWASM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
		private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
			_context = context;
        }

	   [HttpGet("comics")]
	   public async Task<ActionResult<List<Comic>>> GetComics()
	   {
			var comics = await _context.Comics.ToListAsync();
			return Ok(comics);
	   }

		[HttpGet]
		public async Task<ActionResult<List<Superhero>>> GetSuperHeroes()
		{
			var heroes = await _context.Superheros.Include(sh => sh.Comic).ToListAsync();
			return Ok(heroes);
		}

		[HttpGet("{id}")]
	   public async Task<ActionResult<List<Superhero>>> GetSingleHero(int id)
	   {
		  var hero = await _context.Superheros
				.Include(h => h.Comic)
				.FirstOrDefaultAsync(h => h.Id == id);

		  if(hero == null)
		  {
			 return NotFound("Sorry, no hero here. :/");
		  }
		  return Ok(hero);
	   }

		[HttpPost]
		public async Task<ActionResult<List<Superhero>>> CreateSuperHero(Superhero hero)
		{
			hero.Comic = null;
			_context.Superheros.Add(hero);
			await _context.SaveChangesAsync();
						
			return Ok(await GetDbHeroes());
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<List<Superhero>>> UpdateSuperHero(Superhero hero, int id)
		{
			var dbHero= await _context.Superheros
				.Include(sh => sh.Comic)
				.FirstOrDefaultAsync(sh => sh.Id == id);
			if (dbHero == null)
				return NotFound("Sorry, but no hero for you. :/");

			dbHero.FirstName = hero.FirstName;
			dbHero.LastName = hero.LastName;
			dbHero.HeroName = hero.HeroName;
			dbHero.ComicId = hero.ComicId;

			await _context.SaveChangesAsync();

			return Ok(await GetDbHeroes());
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<List<Superhero>>> DeleteSuperHero(Superhero hero, int id)
		{
			var dbHero = await _context.Superheros
				.Include(sh => sh.Comic)
				.FirstOrDefaultAsync(sh => sh.Id == id);
			if (dbHero == null)
				return NotFound("Sorry, but no hero for you. :/");

			_context.Superheros.Remove(dbHero);
			await _context.SaveChangesAsync();

			return Ok(await GetDbHeroes());
		}

		private async Task<List<Superhero>> GetDbHeroes()
        {
			return await _context.Superheros.Include(sh => sh.Comic).ToListAsync();
        }

		[HttpGet("search/{searchText}")]
		public async Task<ActionResult<List<Superhero>>> Search(string searchText)
        {
			var heroes = await _context.Superheros.Include(sh => sh.Comic)
				.Where(sh => sh.FirstName.Contains(searchText) 
				|| sh.LastName.Contains(searchText)
				|| sh.HeroName.Contains(searchText)
				|| sh.Comic.Name.Contains(searchText)
				)
				.ToListAsync();
			return Ok(heroes);
		}

	}
}
