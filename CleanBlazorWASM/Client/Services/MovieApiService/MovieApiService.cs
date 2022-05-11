using System.Net.Http.Json;

namespace CleanBlazorWASM.Client.Services.MovieApiService
{
    public class MovieApiService : IMovieApiService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey = "";

        public MovieApiService()
        {
            _http = new HttpClient();
            
        }

        public async Task<MovieApiResponse> MovieSearch(string query)
        {
            //_http.BaseAddress = new Uri("https://api.themoviedb.org/3");
            //_http.
            string url = $"https://api.themoviedb.org/3/search/movie?api_key=68b58daee5a8b336fa099bc87a598c13&language=en-US&query={query}&page=1&include_adult=false";
            return await _http.GetFromJsonAsync<MovieApiResponse>(url);
        }
    }
}
