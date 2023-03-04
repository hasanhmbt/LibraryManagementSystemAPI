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


        /// <summary>
        /// Return list of users
        /// </summary>
        /// <returns></returns>
         [HttpGet("[action]")]
        public ActionResult GetAllUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }


        /// <summary>
        /// Retrun one user infromations by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{id}")]
        public ActionResult GetUserById(int id)
        {
            return Ok(_userRepository.GetUserById(id));
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public ActionResult AddUser(User user)
        {
            if (user == null)
                return BadRequest();

            int newId = _userRepository.AddUsers(user);
            return CreatedAtAction(nameof(AddUser), new { id = newId }, null);
        }

        /// <summary>
        ///  Edit an existing user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("[action]/{id}")]
        public ActionResult EditUser(int id, [FromBody] User? user)
        {
            if (user == null || user.Email == null || user.Name == null || user.Status == null)
                return BadRequest();

            if (_userRepository.GetUserById(id) == null)
                return NotFound();

            user.Id = id;
            _userRepository.EditUser(user);
            return NoContent();
        }


        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
