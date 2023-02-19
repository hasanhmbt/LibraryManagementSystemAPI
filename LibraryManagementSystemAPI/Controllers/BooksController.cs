using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        
        private readonly IBookRepository  _bookRepository;
        public BooksController(IBookRepository iBookRepository)
        {
            _bookRepository = iBookRepository;
        }

        [HttpGet("[action]")]
        public ActionResult GetAllBooks()
        {
            return Ok(_bookRepository.GetAllBooks());
        }

        [HttpGet("[action]/{id}")]
        public ActionResult GetBookById(int id)
        {
            return Ok(_bookRepository.GetBookById(id));
        }



        [HttpPost("[action]")]
        public ActionResult AddBook(Book book)
        {
            if(book == null)
                    return BadRequest();

           int id = _bookRepository.AddBook(book);
            return Ok(id);
        }

        [HttpPut("[action]/{id}")]
        public ActionResult EditBook(int id ,[FromBody] Book? book)
        {
            if (book == null  || book.Count == null || book.Status == null || book.Author == null)
                return BadRequest();

            if (_bookRepository.GetBookById(id) == null) 
                return NotFound();

            
            _bookRepository.EditBook(book);
            return NoContent();

        }

        [HttpDelete("[action]/{id}")]

        public ActionResult DeleteBook(int id)
        {
            if (_bookRepository.GetBookById(id) == null)
                return NotFound();
            _bookRepository.DeleteBooks(id);
            return NoContent();
        }

    }
}
