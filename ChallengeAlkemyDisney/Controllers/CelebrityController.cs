using ChallengeAlkemyDisney.Models;
using ChallengeAlkemyDisney.Repositories;
using ChallengeAlkemyDisney.ViewModels;
using ChallengeAlkemyDisney.ViewModels.Cele;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChallengeAlkemyDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CelebrityController : ControllerBase
    {
        private readonly ICelebrityRepository _celebrityRepository;
        private readonly IMovieOrSerieRepository _movieOrSerieRepository;
        public CelebrityController(ICelebrityRepository celebrityRepository, IMovieOrSerieRepository movieOrSerieRepository)
        {
            _celebrityRepository = celebrityRepository;
            _movieOrSerieRepository = movieOrSerieRepository;
        }
        // GET
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var celebrities = _celebrityRepository.GetAllCelebrities();
                var celebritiesViewModel = new List<CeleResponseViewModel>();
                foreach (var celebrity in celebrities)
                {
                    var celebrityViewModel = new CeleResponseViewModel
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

        // GET 
        [HttpGet("celebrities")]
        public IActionResult Get(string name, int age, int idMovie)
        {
            try
            {
                var celebrities = _celebrityRepository.GetAllCelebrities().ToList();
                var celebritiesViewModel = new List<CeleResponseViewModel>();

                if (!string.IsNullOrEmpty(name))
                {
                    var celebrity = celebrities.Find(c => c.Name == name);
                    if(celebrity != null)
                    {
                        var celebrityViewModel = new CeleResponseViewModel
                        {
                            Image = celebrity.Image,
                            Name = celebrity.Name
                        };
                        celebritiesViewModel.Add(celebrityViewModel);
                    }
                    else
                    {
                        return BadRequest($"Personaje con nombre {name} no existe");
                    }
                }

                if (age > 0)
                {
                    var celebridades = celebrities.Where(c => c.Age == age);
                    if(celebridades != null )
                    {
                        foreach (var celebrity in celebridades)
                        {
                            var celebrityViewModel = new CeleResponseViewModel
                            {
                                Image = celebrity.Image,
                                Name = celebrity.Name
                            };
                            celebritiesViewModel.Add(celebrityViewModel);
                        }
                    }
                    else
                    {
                        return BadRequest($"Personaje con edad {age} no existe");
                    }
                }

                if (idMovie > 0)
                {
                    if (celebrities.Count != 0)
                    {
                        foreach(var celebrity in celebrities)
                        {
                            foreach(var mos in celebrity.MovieOrSeries)
                            {
                                if(mos.Id == idMovie)
                                {
                                    var celebrityViewModel = new CeleResponseViewModel
                                    {
                                        Image = celebrity.Image,
                                        Name = celebrity.Name
                                    };
                                    celebritiesViewModel.Add(celebrityViewModel);
                                }
                            }
                        }
                    }
                }

                if (!celebrities.Any()) return NoContent();

                return Ok(celebritiesViewModel);
            }catch(Exception ex)
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
                Celebrity celebrityObtenido = _celebrityRepository.GetCelebrity(id);
                if (celebrityObtenido == null) return BadRequest($"La celebridad con Id:{id} no existe");
                return Ok(celebrityObtenido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST 
        [HttpPost]
        public IActionResult Post(CeleRequestPostViewModel celebrity)
        {
            var celebrityOrginal = _celebrityRepository.GetAllCelebrities().FirstOrDefault(c => c.Name == celebrity.Name);

            if(celebrityOrginal != null)
            {
                return BadRequest($"La celebridad con nombre {celebrity.Name} ya existe");
            }

            var celebridad = new Celebrity
            {
                Image = celebrity.Image,
                Name = celebrity.Name,
                Age = celebrity.Age,
                Weight = celebrity.Weight,
                History = celebrity.History
            };

            if (celebrity.MovieOrSeries != null)
            {
                foreach(var mos in celebrity.MovieOrSeries)
                {
                    if(mos.Id != 0)
                    {
                        var movie = _movieOrSerieRepository.GetAllMovieOrSeries().FirstOrDefault(m => m.Id == mos.Id);
                        if(movie != null)
                        {
                            celebridad.MovieOrSeries.Add(movie);
                        }
                    }
                }
            }

            return Ok(_celebrityRepository.AddCelebrity(celebridad));
        }

        // PUT 
        [HttpPut]
        public IActionResult Put(Celebrity celebrity)
        {
            var originalCelebrity = _celebrityRepository.GetCelebrity(celebrity.Id);

            if (originalCelebrity == null) return BadRequest($"La celebridad con id {celebrity.Id} no existe");

            originalCelebrity.Image = celebrity.Image;
            originalCelebrity.Name = celebrity.Name;
            originalCelebrity.Age = celebrity.Age;
            originalCelebrity.Weight = celebrity.Weight;
            originalCelebrity.History = celebrity.History;

            if (celebrity.MovieOrSeries != null)
            {
                foreach(var c in originalCelebrity.MovieOrSeries)
                {
                    c.Id = celebrity.Id;
                }
            }

            _celebrityRepository.UpdateCelebrity(originalCelebrity);

            return Ok(originalCelebrity);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _celebrityRepository.DeleteCelebrity(id);
        }
    }
}
