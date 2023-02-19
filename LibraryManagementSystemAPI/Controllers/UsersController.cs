using LibraryManagementSystemAPI.Entities;
using LibraryManagementSystemAPI.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository ;
        }


         [HttpGet("[action]")]

        public ActionResult GetAllUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

 

        [HttpGet("[action]/{id}")]
        public ActionResult GetUserById(int id)
        {
            return Ok(_userRepository.GetUserById(id));
        }

        [HttpPost("[action]")]
        public ActionResult AddUser(User User)
        {
            if (User == null)
                return BadRequest();

            int newId = _userRepository.AddUsers(User);
            return CreatedAtAction(nameof(AddUser), new { id = newId }, null);
        }


        [HttpPut("[action]/{id}")]
        public ActionResult EditUser(int id, [FromBody] User? User)
        {
            if (User == null || User.Email == null || User.Name == null || User.Status == null)
                return BadRequest();

            if (_userRepository.GetUserById(id) == null)
                return NotFound();

            User.Id = id;
            _userRepository.EditUser(User);
            return NoContent();

        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteUsers(int id)
        {

            if (_userRepository.GetUserById(id) == null)
                return NotFound();

            _userRepository.DeleteUsers(id);
            return NoContent();

        }



    }
}
