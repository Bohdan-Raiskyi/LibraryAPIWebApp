using LibraryAPIWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BookCategoriesController : ControllerBase
{
    private readonly LibraryAPIContext _context;

    public BookCategoriesController(LibraryAPIContext context)
    {
        _context = context;
    }

    // GET: api/BookCategories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookCategory>>> GetBookCategories()
    {
        return await _context.BookCategories
            .Include(bc => bc.Book)
            .Include(bc => bc.Category)
            .ToListAsync();
    }

    // GET: api/BookCategories/5/2
    [HttpGet("{bookId}/{categoryId}")]
    public async Task<ActionResult<BookCategory>> GetBookCategory(int bookId, int categoryId)
    {
        var bookCategory = await _context.BookCategories
            .Include(bc => bc.Book)
            .Include(bc => bc.Category)
            .FirstOrDefaultAsync(bc => bc.BookId == bookId && bc.CategoryId == categoryId);

        if (bookCategory == null)
        {
            return NotFound();
        }

        return bookCategory;
    }

    // PUT: api/BookCategories/5/2
    [HttpPut("{bookId}/{categoryId}")]
    public async Task<IActionResult> PutBookCategory(int bookId, int categoryId, BookCategory bookCategory)
    {
        if (bookId != bookCategory.BookId || categoryId != bookCategory.CategoryId)
        {
            return BadRequest();
        }

        _context.Entry(bookCategory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookCategoryExists(bookId, categoryId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/BookCategories
    [HttpPost]
    public async Task<ActionResult<BookCategory>> PostBookCategory(BookCategory bookCategory)
    {
        // Перевірка чи існує категорія
        var category = await _context.Categories.FindAsync(bookCategory.CategoryId);
        if (category == null)
        {
            return NotFound(new { message = $"Категорії з ID {bookCategory.CategoryId} не знайдено." });
        }

        // Перевірка чи існує книга
        var book = await _context.Books.FindAsync(bookCategory.BookId);
        if (book == null)
        {
            return NotFound(new { message = $"Книгу з ID {bookCategory.BookId} не знайдено." });
        }

        // Перевірка чи такий зв’язок вже є
        if (await _context.BookCategories.AnyAsync(ub => ub.BookId == bookCategory.BookId && ub.CategoryId == bookCategory.CategoryId))
        {
            return Conflict(new { message = "Цей зв’язок вже існує." });
        }

        _context.BookCategories.Add(bookCategory);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (BookCategoryExists(bookCategory.BookId, bookCategory.CategoryId))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction(nameof(GetBookCategory), new { bookId = bookCategory.BookId, categoryId = bookCategory.CategoryId }, bookCategory);
    }

    // DELETE: api/BookCategories/5/2
    [HttpDelete("{bookId}/{categoryId}")]
    public async Task<IActionResult> DeleteBookCategory(int bookId, int categoryId)
    {
        var bookCategory = await _context.BookCategories
            .FirstOrDefaultAsync(bc => bc.BookId == bookId && bc.CategoryId == categoryId);

        if (bookCategory == null)
        {
            return NotFound();
        }

        _context.BookCategories.Remove(bookCategory);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookCategoryExists(int bookId, int categoryId)
    {
        return _context.BookCategories.Any(e => e.BookId == bookId && e.CategoryId == categoryId);
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using LibraryAPIWebApp.Models;

//namespace LibraryAPIWebApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BookCategoriesController : ControllerBase
//    {
//        private readonly LibraryAPIContext _context;

//        public BookCategoriesController(LibraryAPIContext context)
//        {
//            _context = context;
//        }

//        // GET: api/BookCategories
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<BookCategory>>> GetBookCategories()
//        {
//            return await _context.BookCategories.ToListAsync();
//        }

//        // GET: api/BookCategories/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<BookCategory>> GetBookCategory(int id)
//        {
//            var bookCategory = await _context.BookCategories.FindAsync(id);

//            if (bookCategory == null)
//            {
//                return NotFound();
//            }

//            return bookCategory;
//        }

//        // PUT: api/BookCategories/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutBookCategory(int id, BookCategory bookCategory)
//        {
//            if (id != bookCategory.BookId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(bookCategory).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!BookCategoryExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/BookCategories
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<BookCategory>> PostBookCategory(BookCategory bookCategory)
//        {
//            _context.BookCategories.Add(bookCategory);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (BookCategoryExists(bookCategory.BookId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetBookCategory", new { id = bookCategory.BookId }, bookCategory);
//        }

//        // DELETE: api/BookCategories/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBookCategory(int id)
//        {
//            var bookCategory = await _context.BookCategories.FindAsync(id);
//            if (bookCategory == null)
//            {
//                return NotFound();
//            }

//            _context.BookCategories.Remove(bookCategory);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool BookCategoryExists(int id)
//        {
//            return _context.BookCategories.Any(e => e.BookId == id);
//        }
//    }
//}
