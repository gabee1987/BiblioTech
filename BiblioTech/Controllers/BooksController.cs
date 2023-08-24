using BiblioTech.DTO;
using BiblioTech.Models;
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
        /// <summary>
        /// Retrieves a list of all available books.
        /// </summary>
        /// <returns>A list of books.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _bookService.GetAllBookAsync();

            if ( books == null || !books.Any() )
                return Ok( new ApiResponse<IEnumerable<BookDTO>>
                {
                    Success = false,
                    Message = "No books found.",
                    Data    = null
                } );

            return Ok( new ApiResponse<IEnumerable<BookDTO>>
            {
                Success = true,
                Message = "Books retrieved successfully.",
                Data    = books
            } );
        }

        // GET: api/books/5
        /// <summary>
        /// Retrieves a book based on the given ID.
        /// </summary>
        /// <param name="id">The ID of the desired book.</param>
        /// <returns>The specified book or NotFound if not found.</returns>
        [HttpGet("id")]
        public async Task<ActionResult<BookDTO>> GetBook( int id )
        {
            var book = await _bookService.GetBookByIdAsync( id );

            if ( book == null )
            {
                return NotFound( new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = "Book not found.",
                    Data    = null
                } );
            }

            return Ok( new ApiResponse<BookDTO>
            {
                Success = true,
                Message = "Book retrieved successfully.",
                Data    = book
            } );
        }

        // POST: api/books
        /// <summary>
        /// Adds a new book.
        /// </summary>
        /// <param name="bookDTO">The details of the book to add.</param>
        /// <returns>The newly created book with its assigned ID.</returns>
        [HttpPost]
        public async Task<ActionResult<BookDTO>> PostBook( BookDTO bookDTO )
        {
            var book = await _bookService.AddBookAsync( bookDTO );

            if ( book == null )
            {
                return BadRequest( new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = "Failed to create the book.",
                    Data    = null
                } );
            }

            return CreatedAtAction(
                nameof( GetBook ),
                new { id = book.Id },
                new ApiResponse<BookDTO>
                {
                    Success = true,
                    Message = "Book created successfully.",
                    Data    = book
                }
            );
        }

        // PUT: api/books/5
        /// <summary>
        /// Updates the details of a specific book.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="bookDTO">The updated details of the book.</param>
        /// <returns>NoContent on success or NotFound if not found.</returns>
        [HttpPut("id")]
        public async Task<IActionResult> PutBook( int id, BookDTO bookDTO )
        {
            if ( id != bookDTO.Id )
            {
                return BadRequest();
            }

            var updatedBook = await _bookService.UpdateBookAsync( bookDTO );

            if ( updatedBook == null )
            {
                return NotFound( new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = "Book not found.",
                    Data    = null
                } );
            }

            return NoContent();
        }

        // DELETE: api/books/5
        /// <summary>
        /// Deletes a specific book by ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>NoContent on successful deletion or NotFound if not found.</returns>
        [HttpDelete( "id" )]
        public async Task<IActionResult> DeleteBook( int id )
        {
            var result = await _bookService.DeleteBookAsync( id );

            if ( !result )
            {
                return NotFound( new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = "Book not found.",
                    Data    = null
                } );
            }

            return NoContent();
        }

        // GET /api/books/search?query=searchTerm
        /// <summary>
        /// Searches for books based on a query term.
        /// </summary>
        /// <param name="searchModel">The search term or query to look for.</param>
        /// <returns>A list of books that match the search term.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooks( [FromQuery] BookSearchModel searchModel )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var books = await _bookService.SearchBooksAsync( searchModel.Query );

            return Ok( new ApiResponse<IEnumerable<BookDTO>>
            {
                Success = true,
                Message = "Books retrieved successfully.",
                Data    = books
            } );
        }

        // GET /api/books/search/title?query=searchTerm
        /// <summary>
        /// Searches for books based on title.
        /// </summary>
        /// <param name="searchModel">The search term or query to look for.</param>
        /// <returns>A list of books that match the search term.</returns>
        [HttpGet( "search/title" )]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooksByTitle( [FromQuery] BookSearchModel searchModel )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var books = await _bookService.SearchBooksByTitleAsync( searchModel.Query );

            if ( !books.Any() )
            {
                return Ok( new ApiResponse<IEnumerable<BookDTO>>
                {
                    Success = true,
                    Message = "No books found for the provided query.",
                    Data    = new List<BookDTO>()
                } );
            }

            return Ok( new ApiResponse<IEnumerable<BookDTO>>
            {
                Success = true,
                Message = "Books retrieved successfully.",
                Data    = books
            } );
        }

        // GET /api/books/search/author?query=searchTerm
        /// <summary>
        /// Searches for books based on author.
        /// </summary>
        /// <param name="searchModel">The search term or query to look for.</param>
        /// <returns>A list of books that match the search term.</returns>
        [HttpGet( "search/author" )]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooksByAuthor( [FromQuery] BookSearchModel searchModel )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var books = await _bookService.SearchBooksByAuthorAsync( searchModel.Query );

            if ( !books.Any() )
            {
                return Ok( new ApiResponse<IEnumerable<BookDTO>>
                {
                    Success = true,
                    Message = "No books found for the provided query.",
                    Data    = new List<BookDTO>()
                } );
            }

            return Ok( new ApiResponse<IEnumerable<BookDTO>>
            {
                Success = true,
                Message = "Books retrieved successfully.",
                Data    = books
            } );
        }

        // GET /api/books/search/genre?query=searchTerm
        /// <summary>
        /// Searches for books based on genre.
        /// </summary>
        /// <param name="searchModel">The search term or query to look for.</param>
        /// <returns>A list of books that match the search term.</returns>
        [HttpGet( "search/genre" )]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooksByGenre( [FromQuery] BookSearchModel searchModel )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            var books = await _bookService.SearchBooksByGenreAsync( searchModel.Query );

            if ( !books.Any() )
            {
                return Ok( new ApiResponse<IEnumerable<BookDTO>>
                {
                    Success = true,
                    Message = "No books found for the provided query.",
                    Data    = new List<BookDTO>()
                } );
            }

            return Ok( new ApiResponse<IEnumerable<BookDTO>>
            {
                Success = true,
                Message = "Books retrieved successfully.",
                Data    = books
            } );
        }
    }
}
