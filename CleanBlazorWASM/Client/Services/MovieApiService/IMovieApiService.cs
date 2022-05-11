
namespace CleanBlazorWASM.Client.Services.MovieApiService
{
    public interface IMovieApiService
    {
        Task<MovieApiResponse> MovieSearch(string query);
    }
}