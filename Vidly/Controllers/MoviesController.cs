using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Movie
        public ActionResult Index()
        {
            return View(_context.Movies.Include(c => c.Genre).ToList<Movie>());
        }

        public ActionResult Details(int id)
        {
            Movie movie = _context.Movies.Include(c => c.Genre).SingleOrDefault<Movie>(c => c.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }


        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
                _context.Movies.Add(movie);
            else
            {
                var movieToSave = _context.Movies.Single(c => c.Id == movie.Id);

                movieToSave.Name = movie.Name;
                movieToSave.DateAdded = movie.DateAdded;
                movieToSave.DateReleased = movie.DateReleased;
                movieToSave.GenreId = movie.GenreId;
                movieToSave.NumbersInStock = movie.NumbersInStock;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult New()
        {
            return View("Edit", new MovieGenreViewModel
            {
                Genres = _context.Genres.ToList()
            });
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault<Movie>(m => m.Id == id);

            if (movie != null)
            {
                var viewmodel = new MovieGenreViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View(viewmodel);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }

}