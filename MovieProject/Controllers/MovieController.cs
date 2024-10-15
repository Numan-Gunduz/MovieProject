using Microsoft.AspNetCore.Mvc;
using MovieProject.Services;

namespace MovieProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTopMoviesAsync();
            return View(movies);
        }
    }
}