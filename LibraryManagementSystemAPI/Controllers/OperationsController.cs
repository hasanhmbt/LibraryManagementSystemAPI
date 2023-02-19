using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using LibraryManagementSystemAPI.Repositories.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperationsController : ControllerBase
    {

        private readonly IOperationRepository _operationRepository;

        public OperationsController(IOperationRepository operationRepository)
        {
            _operationRepository= operationRepository;
        }
        [HttpGet("[action]")]

        public ActionResult GetAllOperations()
        {
            return Ok(_operationRepository.GetAllOperations());
        }


        [HttpGet("[action]")]
        public ActionResult GetBooksCombo()
        {
            return Ok(_operationRepository.GetBooksCombo());
        }


        [HttpGet("[action]/{id}")]
        public ActionResult GetOperationById(int id)
        {
            return Ok(_operationRepository.GetOperationById(id));
        }

        [HttpPost("[action]")]
        public ActionResult AddOperation(Operation operation)
        {
            if (operation == null)
                return BadRequest();

            int newId = _operationRepository.AddOperation(operation);
            return CreatedAtAction(nameof(AddOperation), new { id = newId }, null);
        }


        [HttpPut("[action]/{id}")]
        public ActionResult EditOperation(int id, [FromBody] Operation?  operation)
        {
            if (operation == null || operation.BookId == null || operation.UserId == null || operation.ReaderId == null)
                return BadRequest();

            if (_operationRepository.GetOperationById(id) == null)
                return NotFound();

            operation.Id = id;
            _operationRepository.EditOperation(operation);
            return NoContent();

        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteOperations(int id)
        {

            if (_operationRepository.GetOperationById(id) == null)
                return NotFound();

            _operationRepository.DeleteOperations(id);
            return NoContent();

        }


    }
}
