using ChallengeAlkemyDisney.Models;
using ChallengeAlkemyDisney.Repositories;
using ChallengeAlkemyDisney.ViewModels;
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
    public class CelebrityController : ControllerBase
    {
        private readonly ICelebrityRepository _celebrityRepository;
        public CelebrityController(ICelebrityRepository celebrityRepository)
        {
            _celebrityRepository = celebrityRepository;
        }
        // GET: api/<CelebrityController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var celebrities = _celebrityRepository.GetAllCelebrities();
                var celebritiesViewModel = new List<CelebrityResponseViewModel>();
                foreach (var celebrity in celebrities)
                {
                    var celebrityViewModel = new CelebrityResponseViewModel
                    {
                        Image = celebrity.Image,
                        Name = celebrity.Name
                    };

                    celebritiesViewModel.Add(celebrityViewModel);
                }
                return Ok(celebritiesViewModel);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CelebrityController>/5
        [HttpGet]
        public IActionResult GetCelebrityName(string name)
        {
            try
            {
                var celebrities = _celebrityRepository.GetAllCelebrities();
                var celebritiesViewModel = new List<CelebrityResponseViewModel>();

                if (string.IsNullOrEmpty(name))
                {
                    var celebrity = celebrities.Find(c => c.Name == name);
                    var celebrityViewModel = new CelebrityResponseViewModel
                    {
                        Image = celebrity.Image,
                        Name = celebrity.Name
                    };
                    celebritiesViewModel.Add(celebrityViewModel);
                }

                if (!celebrities.Any()) return NoContent();

                return Ok(celebrities);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetCelebrityAge(int age)
        {
            try
            {
                var celebrities = _celebrityRepository.GetAllCelebrities();
                var celebritiesViewModel = new List<CelebrityResponseViewModel>();

                if (age != null)
                {
                    var celebridades = celebrities.Where(c => c.Age == age);
                    foreach (var celebrity in celebridades)
                    {
                        var celebrityViewModel = new CelebrityResponseViewModel
                        {
                            Image = celebrity.Image,
                            Name = celebrity.Name
                        };
                        celebritiesViewModel.Add(celebrityViewModel);
                    }
                }

                if (!celebrities.Any()) return NoContent();

                return Ok(celebrities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetCelebrityMovie(int idPelicula)
        {
            try
            {
                var celebrities = _celebrityRepository.GetAllCelebrities();
                var celebritiesViewModel = new List<CelebrityResponseViewModel>();

                //if (idPelicula != null)
                //{
                //    var celebridades = 
                //}

                if (!celebrities.Any()) return NoContent();

                return Ok(celebrities);
            }
            catch (Exception ex)
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
                Celebrity celebrityObtenido = _celebrityRepository.GetCelebrity(id);
                if (celebrityObtenido == null) return BadRequest($"La celebridad con {id} no existe");
                return Ok(celebrityObtenido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CelebrityController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CelebrityController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CelebrityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
