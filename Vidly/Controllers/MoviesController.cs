using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

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
    }

}