using BiblioTech.Controllers;
using BiblioTech.DTO;
using BiblioTech.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BiblioTech.Tests.Controllers
{
    [TestFixture]
    public class BooksControllerTests
    {
        private Mock<IBookService> _bookServiceMock;
        private BooksController _controller;

        [SetUp]
        public void Setup()
        {
            _bookServiceMock = new Mock<IBookService>();
            _controller      = new BooksController( _bookServiceMock.Object );
        }

        [Test]
        public async Task GetBooks_ReturnsOk_WhenBooksExist()
        {
            // Arrange
            _bookServiceMock.Setup( service => service.GetAllBookAsync() ).ReturnsAsync( new List<BookDTO> { new BookDTO { Id = 1, Title = "Test Book" } } );

            // Act
            var result = await _controller.GetBooks();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>( result.Result );
        }

        [Test]
        public async Task GetBookById_ReturnsOk_WhenBookExist()
        {
            // Arrange
            int bookId = 1;
            var expectedBook = new BookDTO
            {
                Id       = bookId,
                Title    = "Test Book",
                Subtitle = "A book of Test",
                ISBN10   = "0123456789",
                Authors  = new List<AuthorDTO> { new AuthorDTO { Id = 2, Name = "Test Author" } },
                Genres   = new List<GenreDTO> { new GenreDTO { Id = 3, Name = "Test Genre" } }
            };

            _bookServiceMock.Setup( service => service.GetBookByIdAsync( bookId ) ).ReturnsAsync( expectedBook );

            // Act
            var actionResult = await _controller.GetBook( bookId );
            var okResult     = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull( okResult );
            //Assert.AreEqual( 200, okResult.StatusCode );
            Assert.That( okResult.StatusCode, Is.EqualTo( 200 ) );
        }

        [Test]
        public async Task PostBook_ValidBookDTO_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var bookDTO = new BookDTO
            {
                Title    = "New Test Book Title",
                Subtitle = "New Test Book Subtitle",
                ISBN10   = "0123456789",
                Authors  = new List<AuthorDTO> { new AuthorDTO { Id = 2, Name = "New Test Author" } },
                Genres   = new List<GenreDTO> { new GenreDTO { Id = 3, Name = "New Test Genre" } }
            };

            var createdBook = new BookDTO
            {
                Id       = 1,
                Title    = bookDTO.Title,
                Subtitle = bookDTO.Subtitle,
                ISBN10   = bookDTO.ISBN10,
                Authors  = bookDTO.Authors,
                Genres   = bookDTO.Genres,
            };

            _bookServiceMock.Setup( service => service.AddBookAsync( bookDTO ) ).ReturnsAsync( createdBook );

            // A more flexible, broader test setup if the specific input isn't the main focus of the test would be like this:
            //_bookServiceMock.Setup( s => s.AddBookAsync( It.IsAny<BookDTO>() ) ).ReturnsAsync( createdBook );

            // Act
            var actionResult          = await _controller.PostBook( bookDTO );
            var createdAtActionResult = actionResult.Result as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull( createdAtActionResult );
            Assert.That( createdAtActionResult.StatusCode, Is.EqualTo( 201 ) );

            var returnedBookDTO = createdAtActionResult.Value as ApiResponse<BookDTO>;
            Assert.That( returnedBookDTO.Data.Id, Is.EqualTo( createdBook.Id ) );

        }

        [Test]
        public async Task PutBook_ReturnsBadRequest_WhenIdDoesNotMatchBookDTOId()
        {

        }

        [Test]
        public async Task PutBook_ReturnsNotFound_WhenBookDoesNotExist()
        {

        }

        [Test]
        public async Task DeleteBook_ReturnsNoContent_WhenDeletionIsSuccessful()
        {

        }

        [Test]
        public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
        {

        }

        [Test]
        public async Task SearchBooks_ReturnsBadRequest_ForInvalidModelState()
        {

        }

        [Test]
        public async Task SearchBooks_ReturnsOk_WithMatchingBooks()
        {

        }

        [Test]
        public async Task SearchBooks_ReturnsOk_WithNoMatchingBooks()
        {

        }
    }
}
