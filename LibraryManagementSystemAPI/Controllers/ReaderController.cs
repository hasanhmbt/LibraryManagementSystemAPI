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

        [HttpGet("[action]")]

        public ActionResult GetAllReaders()
        {
            return Ok(_ReaderRepository.GetAllReaders());
        }


        [HttpGet("[action]")]
        public ActionResult GetReadersCombo()
        {
            return Ok(_ReaderRepository.GetReadersCombo());
        }


        [HttpGet("[action]/{id}")]
        public ActionResult GetReaderById(int id)
        {
            return Ok(_ReaderRepository.GetReaderById(id));
        }

        [HttpPost("[action]")]
        public ActionResult AddReader(Reader reader)
        {
            if (reader == null)
                return BadRequest();

            int newId = _ReaderRepository.AddReader(reader);
            return CreatedAtAction(nameof(AddReader), new { id = newId }, null);
        }


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
