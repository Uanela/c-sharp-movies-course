using Frontend.Entities;

namespace Frontend.Clients
{
    public class MoviesClient(HttpClient httpClient)
    {
        public async Task<Movie[]> GetMoviesAsync() => await httpClient.GetFromJsonAsync<Movie[]>("movies") ?? [];

        public async Task<Movie> GetMovieAsync(int id) => await httpClient.GetFromJsonAsync<Movie>($"movies/{id}") ?? throw new Exception("Movie not found");
    

    public async Task AddMovieAsync(Movie movie) => await httpClient.PostAsJsonAsync("movies", movie);

    public async Task UpdateMovieAsync(int id, Movie movie) {
        movie.Genre = null;
        await httpClient.PatchAsJsonAsync($"movies/{id}", movie);
    }

    public async Task DeleteMovieAsync(int id) => await httpClient.DeleteAsync($"movies/{id}");
    }
}
