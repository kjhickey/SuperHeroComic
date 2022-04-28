using CleanBlazorWASM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CleanBlazorWASM.Client.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
	   private readonly HttpClient _http;
	   private readonly NavigationManager _navigationManager;

		public SuperHeroService(HttpClient http, NavigationManager navigationManager)
	   {
		  _http = http;
		  _navigationManager = navigationManager;
	   }

	   public List<Superhero> Heroes { get; set; } = new List<Superhero>();
	   public List<Comic> Comics { get; set; } = new List<Comic>();

        public async Task CreateHero(Superhero hero)
        {
			var result = await _http.PostAsJsonAsync("api/superhero", hero);
			var response = await result.Content.ReadFromJsonAsync<List<Superhero>>();
			await SetHeroes(result);
		}

        public async Task DeleteHero(int id)
        {
            var result = await _http.DeleteAsync($"api/superhero/{id}");
            await SetHeroes(result);
        }

        private async Task SetHeroes(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Superhero>>();
			Heroes = response;
			_navigationManager.NavigateTo("superheroes");
        }

        public async Task GetComics()
	   {
			var result = await _http.GetFromJsonAsync<List<Comic>>("api/superhero/comics");
			if (result != null)
				Comics = result;
		}

	   public async Task<Superhero> GetSingleHero(int id)
	   {
			var result = await _http.GetFromJsonAsync<Superhero>($"api/superhero/{id}");
			if (result != null)
				return result;
			throw new Exception("Hero not found!");
		}

	   public async Task GetSuperHeroes()
	   {
		  var result = await _http.GetFromJsonAsync<List<Superhero>>("api/superhero");
		  if (result != null)
			 Heroes = result;
	   }

        public async Task UpdateHero(Superhero hero)
        {
			var result = await _http.PutAsJsonAsync($"api/superhero/{hero.Id}", hero);
			await SetHeroes(result);
		}
    }
}
