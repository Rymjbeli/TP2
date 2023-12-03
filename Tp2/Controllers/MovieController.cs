using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tp2.Models;

namespace Tp2.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationdbContext _db;
        public MovieController(ApplicationdbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var movies = _db.movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public IActionResult Create()
        {
            ViewBag.Genres = new SelectList(_db.genres, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {            
                _db.movies.Add(movie);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
        
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _db.movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            
            }
            ViewBag.Genres = new SelectList(_db.genres, "Id", "Name");
            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
           
            _db.Update(movie);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var movie = _db.movies
                .Include(m => m.Genre)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _db.movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _db.movies.Remove(movie);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
