using BiblioTech.DTO;
using BiblioTech.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTech.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController( IBookService bookService )
        {
            _bookService = bookService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            return Ok(await _bookService.GetAllBookAsync());
        }

        // GET: api/books/5
        [HttpGet("id")]
        public async Task<ActionResult<BookDTO>> GetBook( int id )
        {
            var book = await _bookService.GetBookByIdAsync( id );

            if ( book == null )
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<BookDTO>> PostBook( BookDTO bookDTO )
        {
            var book = await _bookService.AddBookAsync( bookDTO );

            return CreatedAtAction( nameof( GetBook ), new { id = book.Id }, book );
        }

        // PUT: api/books/5
        [HttpPut]
        public async Task<IActionResult> PutBook( int id, BookDTO bookDTO )
        {
            if ( id != bookDTO.Id )
            {
                return BadRequest();
            }

            var updatedBook = await _bookService.UpdateBookAsync( bookDTO );

            if ( updatedBook == null )
            {
                return NotFound( nameof( GetBook ) );
            }

            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete]
        public async Task<IActionResult> DeleteBook( int id )
        {
            var result = await _bookService.DeleteBookAsync( id );

            if ( !result )
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
