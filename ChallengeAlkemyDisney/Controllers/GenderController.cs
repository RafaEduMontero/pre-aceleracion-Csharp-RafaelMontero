using ChallengeAlkemyDisney.Models;
using ChallengeAlkemyDisney.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChallengeAlkemyDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderRepository _genderRepository;

        public GenderController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        // GET: api/<GenderController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_genderRepository.GetAllGenders());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<GenderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Gender genderObtenido = _genderRepository.GetGender(id);
                if (genderObtenido == null) return BadRequest($"El género con {id} no existe");
                return Ok(genderObtenido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<GenderController>
        [HttpPost]
        public IActionResult Post([FromBody] Gender gender)
        {
            try
            {
                return Ok(_genderRepository.AddGender(gender));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<GenderController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Gender gender)
        {
            Gender originalGender = _genderRepository.GetGender(gender.Id);
            if (originalGender == null) return BadRequest($"El género con {gender.Id} no existe");
            originalGender.Name = gender.Name;
            originalGender.Image = gender.Image;
            _genderRepository.UpdateGender(originalGender);
            return Ok(originalGender);
        }

        // DELETE api/<GenderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _genderRepository.DeleteGender(id);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
