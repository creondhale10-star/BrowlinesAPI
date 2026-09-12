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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer_Services _service;

        public CustomerController(ICustomer_Services service)
        {
            _service = service;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET api/<Procedures>/5
        [HttpGet("get_by_id")]
        public async Task<IActionResult> GetById([FromBody] User_Model request)
        {
            if (request.Id < 1)
                return BadRequest("A valid Id must be provided in the request body.");

            var result = await _service.GetAllByIdAsync(request.Id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer_Model model)
        {
            var user = new Customer_Model()
            {
                Name = model.Name,
                Birthdate = model.Birthdate,
                ContactNo = model.ContactNo,
                Address = model.Address,
                Complaints = model.Complaints,
                Allergies = model.Allergies,
                Notes = model.Notes,
                Email = model.Email,
                CreatedBy = model.CreatedBy,
            };

            var result = await _service.CreateAsync(user);
            return Ok(result);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Customer_Model model)
        {
            var user = new Customer_Model()
            {
                Id = model.Id,
                Name = model.Name,
                Birthdate = model.Birthdate,
                ContactNo = model.ContactNo,
                Address = model.Address,
                Complaints = model.Complaints,
                Allergies = model.Allergies,
                Notes = model.Notes,
                Email = model.Email,
                CreatedBy = model.CreatedBy,
            };

            var result = await _service.UpdateAsync(user);
            return Ok(result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] User_Model request)
        {
            if (request.Id < 1)
                return BadRequest("A valid Id must be provided in the request body.");

            var result = await _service.DeleteAsync(request.Id);
            return Ok(result);
        }

    }
}
