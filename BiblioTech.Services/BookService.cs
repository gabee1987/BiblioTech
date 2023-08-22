using AutoMapper;
using BiblioTech.Domain.Entities;
using BiblioTech.Domain.Repositories;
using BiblioTech.DTO;
using BiblioTech.Services.Interfaces;

namespace BiblioTech.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService( IBookRepository bookRepository, IMapper mapper )
        {
            _bookRepository = bookRepository;
            _mapper         = mapper;
        }

        public async Task<BookDTO> AddBookAsync( BookDTO bookDTO )
        {
            var bookEntity = _mapper.Map<Book>( bookDTO );
            var result     = await _bookRepository.AddAsync( bookEntity );

            return _mapper.Map<BookDTO>( result );
        }

        public async Task<IEnumerable<BookDTO>> GetAllBookAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<BookDTO>>( books );
        }

        public async Task<BookDTO> GetBookByIdAsync( int id )
        {
            var book = await _bookRepository.GetByIdAsync( id );

            return _mapper.Map<BookDTO>( book );
        }

        public async Task<BookDTO> UpdateBookAsync( BookDTO bookDTO )
        {
            var bookEntity  = _mapper.Map<Book>(bookDTO );
            var updatedBook = await _bookRepository.UpdateAsync( bookEntity );

            return _mapper.Map<BookDTO>( updatedBook );
        }

        public async Task<bool> DeleteBookAsync( int id )
        {
            return await _bookRepository.DeleteAsync( id );
        }

        public async Task<IEnumerable<BookDTO>> SearchBooksAsync( string query )
        {
            var books = await _bookRepository.SearchBooksAsync( query );
            return _mapper.Map<IEnumerable<BookDTO>>( books );
        }
    }
}
