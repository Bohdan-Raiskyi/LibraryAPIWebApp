using LibraryAPIWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BookAuthorsController : ControllerBase
{
    private readonly LibraryAPIContext _context;

    public BookAuthorsController(LibraryAPIContext context)
    {
        _context = context;
    }

    // GET: api/BookAuthors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookAuthor>>> GetBookAuthors()
    {
        return await _context.BookAuthors
            .Include(ba => ba.Book)
            .Include(ba => ba.Author)
            .ToListAsync();
    }

    // GET: api/BookAuthors/5/3
    [HttpGet("{bookId}/{authorId}")]
    public async Task<ActionResult<BookAuthor>> GetBookAuthor(int bookId, int authorId)
    {
        var bookAuthor = await _context.BookAuthors
            .Include(ba => ba.Book)
            .Include(ba => ba.Author)
            .FirstOrDefaultAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId);

        if (bookAuthor == null)
        {
            return NotFound();
        }

        return bookAuthor;
    }

    // PUT: api/BookAuthors/5/3
    [HttpPut("{bookId}/{authorId}")]
    public async Task<IActionResult> PutBookAuthor(int bookId, int authorId, BookAuthor bookAuthor)
    {
        if (bookId != bookAuthor.BookId || authorId != bookAuthor.AuthorId)
        {
            return BadRequest();
        }

        _context.Entry(bookAuthor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookAuthorExists(bookId, authorId))
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

    // POST: api/BookAuthors
    [HttpPost]
    public async Task<ActionResult<BookAuthor>> PostBookAuthor(BookAuthor bookAuthor)
    {
        // Перевірка чи існує користувач
        var author = await _context.Users.FindAsync(bookAuthor.AuthorId);
        if (author == null)
        {
            return NotFound(new { message = $"Автора з ID {bookAuthor.AuthorId} не знайдено." });
        }

        // Перевірка чи існує книга
        var book = await _context.Books.FindAsync(bookAuthor.BookId);
        if (book == null)
        {
            return NotFound(new { message = $"Книгу з ID {bookAuthor.BookId} не знайдено." });
        }

        // Перевірка чи такий зв’язок вже є
        if (await _context.BookAuthors.AnyAsync(ub => ub.BookId == bookAuthor.BookId && ub.AuthorId == bookAuthor.AuthorId))
        {
            return Conflict(new { message = "Цей зв’язок вже існує." });
        }

        _context.BookAuthors.Add(bookAuthor);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (BookAuthorExists(bookAuthor.BookId, bookAuthor.AuthorId))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction(nameof(GetBookAuthor), new { bookId = bookAuthor.BookId, authorId = bookAuthor.AuthorId }, bookAuthor);
    }

    // DELETE: api/BookAuthors/5/3
    [HttpDelete("{bookId}/{authorId}")]
    public async Task<IActionResult> DeleteBookAuthor(int bookId, int authorId)
    {
        var bookAuthor = await _context.BookAuthors
            .FirstOrDefaultAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId);

        if (bookAuthor == null)
        {
            return NotFound();
        }

        _context.BookAuthors.Remove(bookAuthor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookAuthorExists(int bookId, int authorId)
    {
        return _context.BookAuthors.Any(e => e.BookId == bookId && e.AuthorId == authorId);
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
//    public class BookAuthorsController : ControllerBase
//    {
//        private readonly LibraryAPIContext _context;

//        public BookAuthorsController(LibraryAPIContext context)
//        {
//            _context = context;
//        }

//        // GET: api/BookAuthors
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<BookAuthor>>> GetBookAuthors()
//        {
//            return await _context.BookAuthors.ToListAsync();
//        }

//        // GET: api/BookAuthors/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<BookAuthor>> GetBookAuthor(int id)
//        {
//            var bookAuthor = await _context.BookAuthors.FindAsync(id);

//            if (bookAuthor == null)
//            {
//                return NotFound();
//            }

//            return bookAuthor;
//        }

//        // PUT: api/BookAuthors/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutBookAuthor(int id, BookAuthor bookAuthor)
//        {
//            if (id != bookAuthor.BookId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(bookAuthor).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!BookAuthorExists(id))
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

//        // POST: api/BookAuthors
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<BookAuthor>> PostBookAuthor(BookAuthor bookAuthor)
//        {
//            _context.BookAuthors.Add(bookAuthor);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (BookAuthorExists(bookAuthor.BookId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetBookAuthor", new { id = bookAuthor.BookId }, bookAuthor);
//        }

//        // DELETE: api/BookAuthors/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBookAuthor(int id)
//        {
//            var bookAuthor = await _context.BookAuthors.FindAsync(id);
//            if (bookAuthor == null)
//            {
//                return NotFound();
//            }

//            _context.BookAuthors.Remove(bookAuthor);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool BookAuthorExists(int id)
//        {
//            return _context.BookAuthors.Any(e => e.BookId == id);
//        }
//    }
//}
