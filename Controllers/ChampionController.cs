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
    public class ChampionController : ControllerBase
    {
        private readonly IChampions_Services _services;

        public ChampionController(IChampions_Services services)
        {
            _services = services;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAllAsync();
            return Ok(result);
        }

        // GET api/<Procedures>/5
        [HttpGet("get_by_id")]
        public async Task<IActionResult> GetById([FromBody] Champion_Model request)
        {
            if (request.Id < 1)
                return BadRequest("A valid product Id must be provided in the request body.");

            var result = await _services.GetAllByIdAsync(request.Id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Champion_Model model)
        {            
            var result = await _services.CreateAsync(model);
            return Ok(result);
        }

        // PUT api/<UserController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Champion_Model model)
        {            
            var result = await _services.UpdateAsync(model);
            return Ok(result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] User_Model request)
        {
            if (request.Id < 1)
                return BadRequest("A valid product Id must be provided in the request body.");

            var result = await _services.DeleteAsync(request.Id);
            return Ok(result);
        }    
    }
}
