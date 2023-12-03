using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tp2.Models;

namespace Tp2.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationdbContext _db;
        public GenreController(ApplicationdbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var genres = _db.genres.ToList();
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre genre)
        {
            _db.genres.Add(genre);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var genre = _db.genres
                .Include(g => g.Movies) // Include movies related to the genre
                .FirstOrDefault(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        public IActionResult Delete(int id)
        {
            var genre = _db.genres
                .Include(g => g.Movies)
                .FirstOrDefault(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _db.genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            _db.genres.Remove(genre);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
