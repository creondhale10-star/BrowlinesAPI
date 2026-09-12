using Browlines_API.Interface;
using Browlines_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Browlines_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser_Services _userService;

        public UserController(IUser_Services userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result);
        }

        // GET api/<Procedures>/5
        [HttpGet("get_by_id")]
        public async Task<IActionResult> GetById([FromBody] User_Model request)
        {
            if (request.Id < 1)
                return BadRequest("A valid product Id must be provided in the request body.");

            var result = await _userService.GetAllByIdAsync(request.Id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User_Model model)
        {
            var user = new User_Model()
            {
                Username = model.Username,
                Pass = HashPassword(model.Pass),
                Name = model.Name,
                Birthdate = model.Birthdate,
                ContactNo = model.ContactNo,
                Address = model.Address,
                Role = model.Role,
                About = model.About,
                Img = model.Img
            };

            var result = await _userService.CreateAsync(user);
            return Ok(result);
        }


        [HttpPut("change_password")]
        public async Task<IActionResult> ChangePass([FromBody] User_Model model)
        {
            var user = new User_Model()
            {
                Id = model.Id,
                Pass = HashPassword(model.Pass),
            };
            var result = await _userService.ChangePass(user);
            return Ok(result);
        }



        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User_Model model)
        {
            var user = new User_Model()
            {
                Id = model.Id,
                Username = model.Username,
                Name = model.Name,
                Birthdate = model.Birthdate,
                ContactNo = model.ContactNo,
                Address = model.Address,
                Role = model.Role,
                About = model.About,
                Img = model.Img
            };

            var result = await _userService.UpdateAsync(user);
            return Ok(result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] User_Model request)
        {
            if (request.Id < 1)
                return BadRequest("A valid product Id must be provided in the request body.");

            var result = await _userService.DeleteAsync(request.Id);
            return Ok(result);
        }

        private string HashPassword(string password)
        {
            using var sha1 = SHA1.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha1.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }
    }
}
