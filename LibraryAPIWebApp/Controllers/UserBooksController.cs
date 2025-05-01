using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPIWebApp.Models;

namespace LibraryAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBooksController : ControllerBase
    {
        private readonly LibraryAPIContext _context;

        public UserBooksController(LibraryAPIContext context)
        {
            _context = context;
        }

        // GET: api/UserBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBook>>> GetUserBooks()
        {
            return await _context.UserBooks.ToListAsync();
        }

        // GET: api/UserBooks/1/2
        [HttpGet("{userId:int}/{bookId:int}")]
        public async Task<ActionResult<UserBook>> GetUserBook(int userId, int bookId)
        {
            var userBook = await _context.UserBooks.FindAsync(bookId, userId);
            if (userBook == null)
                return NotFound();

            return userBook;
        }

        // PUT: api/UserBooks/1/2
        [HttpPut("{userId:int}/{bookId:int}")]
        public async Task<IActionResult> PutUserBook(int userId, int bookId, UserBook userBook)
        {
            if (userId != userBook.UserId || bookId != userBook.BookId)
                return BadRequest("IDs in the body and route do not match.");

            _context.Entry(userBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBookExists(userId, bookId))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/UserBooks
        [HttpPost]
        public async Task<ActionResult<UserBook>> PostUserBook(UserBook userBook)
        {
            // Перевірка чи існує користувач
            var user = await _context.Users.FindAsync(userBook.UserId);
            if (user == null)
            {
                return NotFound(new { message = $"Користувача з ID {userBook.UserId} не знайдено." });
            }

            // Перевірка чи існує книга
            var book = await _context.Books.FindAsync(userBook.BookId);
            if (book == null)
            {
                return NotFound(new { message = $"Книгу з ID {userBook.BookId} не знайдено." });
            }

            // Перевірка чи такий зв’язок вже є
            if (await _context.UserBooks.AnyAsync(ub => ub.UserId == userBook.UserId && ub.BookId == userBook.BookId))
            {
                return Conflict(new { message = "Цей зв’язок вже існує." });
            }

            // Прив’язка навігаційних властивостей (не обов'язково, але добре)
            //userBook.User = user;
            //userBook.Book = book;

            _context.UserBooks.Add(userBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserBook),
                new { userId = userBook.UserId, bookId = userBook.BookId },
                userBook);
        }


        // DELETE: api/UserBooks/1/2
        [HttpDelete("{userId:int}/{bookId:int}")]
        public async Task<IActionResult> DeleteUserBook(int userId, int bookId)
        {
            var userBook = await _context.UserBooks.FindAsync(bookId, userId);
            if (userBook == null)
                return NotFound();

            _context.UserBooks.Remove(userBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserBookExists(int userId, int bookId)
        {
            return _context.UserBooks.Any(e => e.UserId == userId && e.BookId == bookId);
        }
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
//    public class UserBooksController : ControllerBase
//    {
//        private readonly LibraryAPIContext _context;

//        public UserBooksController(LibraryAPIContext context)
//        {
//            _context = context;
//        }

//        // GET: api/UserBooks
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<UserBook>>> GetUserBooks()
//        {
//            return await _context.UserBooks.ToListAsync();
//        }

//        // GET: api/UserBooks/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<UserBook>> GetUserBook(int id)
//        {
//            var userBook = await _context.UserBooks.FindAsync(id);

//            if (userBook == null)
//            {
//                return NotFound();
//            }

//            return userBook;
//        }

//        // PUT: api/UserBooks/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutUserBook(int id, UserBook userBook)
//        {
//            if (id != userBook.BookId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(userBook).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!UserBookExists(id))
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

//        // POST: api/UserBooks
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<UserBook>> PostUserBook(UserBook userBook)
//        {
//            _context.UserBooks.Add(userBook);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (UserBookExists(userBook.BookId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetUserBook", new { id = userBook.BookId }, userBook);
//        }

//        // DELETE: api/UserBooks/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUserBook(int id)
//        {
//            var userBook = await _context.UserBooks.FindAsync(id);
//            if (userBook == null)
//            {
//                return NotFound();
//            }

//            _context.UserBooks.Remove(userBook);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool UserBookExists(int id)
//        {
//            return _context.UserBooks.Any(e => e.BookId == id);
//        }
//    }
//}
