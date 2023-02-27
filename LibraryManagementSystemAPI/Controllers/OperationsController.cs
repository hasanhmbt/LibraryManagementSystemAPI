﻿using LibraryManagementSystemAPI.Entities;
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

        // Returns Operation list
        [HttpGet("[action]")]
        public ActionResult GetAllOperations()
        {
            return Ok(_operationRepository.GetAllOperations());
        }


        // Retunrs book list for operation (dropdown list)
        [HttpGet("[action]")]
        public ActionResult GetBooksCombo()
        {
            return Ok(_operationRepository.GetBooksCombo());
        }

        // Retrun one operation infromations by id 
        [HttpGet("[action]/{id}")]
        public ActionResult GetOperationById(int id)
        {
            return Ok(_operationRepository.GetOperationById(id));
        }


        // Add new operation
        [HttpPost("[action]")]
        public ActionResult AddOperation(Operation operation)
        {
            if (operation == null)
                return BadRequest();

            int newId = _operationRepository.AddOperation(operation);
            return CreatedAtAction(nameof(AddOperation), new { id = newId }, null);
        }

        //  Edit an existing operations
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

        // Delete one operation from list 
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
