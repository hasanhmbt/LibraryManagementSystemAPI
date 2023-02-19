using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPI.Repositories.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategorysController : ControllerBase
    {

        private readonly IBookCategoryRepository _bookCategoryRepository;
        public BookCategorysController(IBookCategoryRepository bookCategoryRepository)
        {
            _bookCategoryRepository = bookCategoryRepository;
        }
        [HttpGet("[action]")]

        public ActionResult GetAllCategories()
        {
            return Ok(_bookCategoryRepository.GetAllCategories());
        }


        [HttpGet("[action]")]
        public ActionResult GetBookCategorysCombo()
        {
            return Ok(_bookCategoryRepository.GetCategoriesCombo());
        }


        [HttpGet("[action]/{id}")]
        public ActionResult GetBookCategoryById(int id)
        {
            return Ok(_bookCategoryRepository.GetCategoryById(id));
        }

        [HttpPost("[action]")]
        public ActionResult AddBookCategory(BookCategory BookCategory)
        {
            if (BookCategory == null)
                return BadRequest();

            int newId = _bookCategoryRepository.AddCategory(BookCategory);
            return CreatedAtAction(nameof(AddBookCategory), new { id = newId }, null);
        }


        [HttpPut("[action]/{id}")]
        public ActionResult EditBookCategory(int id, [FromBody] BookCategory? BookCategory)
        {
            if (  BookCategory.Id == null || BookCategory.Name == null || BookCategory.Status == null)
                return BadRequest();

            if (_bookCategoryRepository.GetCategoryById(id) == null)
                return NotFound();

            BookCategory.Id = id;
            _bookCategoryRepository.UpdateCategory(BookCategory);
            return NoContent();

        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteBookCategorys(int id)
        {

            if (_bookCategoryRepository.GetCategoryById(id) == null)
                return NotFound();

            _bookCategoryRepository.DeleteCategory(id);
            return NoContent();

        }



    }
}
