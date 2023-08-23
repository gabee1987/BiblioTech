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
            _controller      = new BooksController(_bookServiceMock.Object);
        }

        [Test]
        public async Task GetBooks_ReturnsOk_WhenBooksExist()
        {
            // Arrange
            _bookServiceMock.Setup( s => s.GetAllBookAsync() ).ReturnsAsync( new List<BookDTO> { new BookDTO { Id = 1, Title = "Test Book" } } );

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

            _bookServiceMock.Setup( s => s.GetBookByIdAsync( bookId ) ).ReturnsAsync( expectedBook );

            // Act
            var actionResult = await _controller.GetBook( bookId );
            var okResult     = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull( okResult );
            //Assert.AreEqual( 200, okResult.StatusCode );
            Assert.That( okResult.StatusCode, Is.EqualTo( 200 ) );
        }
    }
}
