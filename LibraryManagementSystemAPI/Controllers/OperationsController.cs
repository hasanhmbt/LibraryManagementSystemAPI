using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Returns Operation list
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public ActionResult GetAllOperations()
        {
            return Ok(_operationRepository.GetAllOperations());
        }


        /// <summary>
        /// Retunrs book list for operation (dropdown list)
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public ActionResult GetBooksCombo()
        {
            return Ok(_operationRepository.GetBooksCombo());
        }

        /// <summary>
        /// Retrun one operation infromations by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public ActionResult GetOperationById(int id)
        {
            return Ok(_operationRepository.GetOperationById(id));
        }


        /// <summary>
        /// Add new operation
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public ActionResult AddOperation(Operation operation)
        {
            if (operation == null)
                return BadRequest();

            int newId = _operationRepository.AddOperation(operation);
            return CreatedAtAction(nameof(AddOperation), new { id = newId }, null);
        }

        /// <summary>
        ///  Edit an existing operations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete one operation from list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
