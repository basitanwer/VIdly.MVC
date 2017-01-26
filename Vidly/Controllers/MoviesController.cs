using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View(FillMovies());
        }

        public IEnumerable<Movie> FillMovies()
        {
            return new List<Movie>()
            {
                new Movie() {Id = 1, Name = "Toy Story"},
                new Movie() {Id = 2, Name = "Finding Nemo"}
            };
        }
    }

}