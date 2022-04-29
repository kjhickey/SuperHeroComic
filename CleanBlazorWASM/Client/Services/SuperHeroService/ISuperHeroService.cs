using CleanBlazorWASM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanBlazorWASM.Client.Services.SuperHeroService
{
    public interface ISuperHeroService
    {
	   List<Superhero> Heroes { get; set; }
	   List<Comic> Comics { get; set; }
	   Task GetComics();
	   Task GetSuperHeroes();
	   Task<Superhero> GetSingleHero(int id);
		Task CreateHero(Superhero hero);
		Task UpdateHero(Superhero hero);
		Task DeleteHero(int id);
        Task Search(string searchText);
    }
}
