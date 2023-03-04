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
    public class ReaderController : ControllerBase
    {

        private readonly IReaderRepository _ReaderRepository;

        public ReaderController(IReaderRepository readerRepository)
        {
            _ReaderRepository = readerRepository;
        }

        /// <summary>
        /// Retruns readers informations list
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public ActionResult GetAllReaders()
        {
            return Ok(_ReaderRepository.GetAllReaders());
        }

        /// <summary>
        /// Readers list for dropdown list 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public ActionResult GetReadersCombo()
        {
            return Ok(_ReaderRepository.GetReadersCombo());
        }

        /// <summary>
        /// returns one reader informations  by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public ActionResult GetReaderById(int id)
        {
            return Ok(_ReaderRepository.GetReaderById(id));
        }
        
        /// <summary>
        /// Add new reader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public ActionResult AddReader(Reader reader)
        {
            if (reader == null)
                return BadRequest();

            int newId = _ReaderRepository.AddReader(reader);
            return CreatedAtAction(nameof(AddReader), new { id = newId }, null);
        }

        /// <summary>
        ///  Edit an existing reader
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reader"></param>
        /// <returns></returns>

        [HttpPut("[action]/{id}")]
        public ActionResult EditReader(int id, [FromBody] Reader? reader)
        {
            if (reader == null || reader.Email == null || reader.Name == null || reader.Status == null)
                return BadRequest();

            if (_ReaderRepository.GetReaderById(id) == null)
                return NotFound();

            reader.Id = id;
            _ReaderRepository.EditReader(reader);
            return NoContent();

        }

        /// <summary>
        /// Delete reader
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteReaders(int id)
        {

            if (_ReaderRepository.GetReaderById(id) == null)
                return NotFound();

            _ReaderRepository.DeleteReaders(id);
            return NoContent();

        }


    }
}
