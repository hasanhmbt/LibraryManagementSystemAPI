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

        /// <summary>
        /// Returns category informations
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public ActionResult GetAllCategories()
        {
            return Ok(_bookCategoryRepository.GetAllCategories());
        }

        /// <summary>
        /// Retunrs category list for dropdown 
        /// </summary>
        /// <returns></returns>

        [HttpGet("[action]")]
        public ActionResult GetBookCategorysCombo() 
        {
            return Ok(_bookCategoryRepository.GetCategoriesCombo());
        }

        /// <summary>
        /// Retrun one category infromations by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public ActionResult GetBookCategoryById(int id)
        {
            return Ok(_bookCategoryRepository.GetCategoryById(id));
        }

        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="bookCategory"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public ActionResult AddBookCategory(BookCategory bookCategory)
        {
            if (bookCategory == null)
                return BadRequest();

            int newId = _bookCategoryRepository.AddCategory(bookCategory);
            return CreatedAtAction(nameof(AddBookCategory), new { id = newId }, null);
        }

        /// <summary>
        ///  Edit an existing category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookCategory"></param>
        /// <returns></returns>
        [HttpPut("[action]/{id}")]
        public ActionResult EditBookCategory(int id, [FromBody] BookCategory? bookCategory)
        {
            if (bookCategory.Id == null || bookCategory.Name == null || bookCategory.Status == null)
                return BadRequest();

            if (_bookCategoryRepository.GetCategoryById(id) == null)
                return NotFound();

            bookCategory.Id = id;
            _bookCategoryRepository.UpdateCategory(bookCategory);
            return NoContent();

        }

        /// <summary>
        /// Delete one category from list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
