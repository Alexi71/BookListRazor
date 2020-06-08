using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Book.ToList() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Book.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                return Json(new { success = false, message = "Error while deleting" });
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete was successful" });
        }
    }
}
