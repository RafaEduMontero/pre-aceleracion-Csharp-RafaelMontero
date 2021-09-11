using ChallengeAlkemyDisney.Models;
using ChallengeAlkemyDisney.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChallengeAlkemyDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenderController : ControllerBase
    {
        private readonly IGenderRepository _genderRepository;

        public GenderController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        // GET
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var genders = _genderRepository.GetAllGenders();
                return Ok(genders);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET
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

        // POST
        [HttpPost]
        public IActionResult Post(Gender gender)
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

        // PUT 
        [HttpPut]
        public IActionResult Put(Gender gender)
        {
            try
            {
                Gender originalGender = _genderRepository.GetGender(gender.Id);
                if (originalGender == null) return BadRequest($"El género con {gender.Id} no existe");
                originalGender.Name = gender.Name;
                originalGender.Image = gender.Image;

                if (originalGender.MovieOrSeries != null)
                {
                    foreach (var g in originalGender.MovieOrSeries)
                    {
                        g.Id = gender.Id;
                    }
                }
                _genderRepository.UpdateGender(originalGender);
                return Ok(originalGender);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE 
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
