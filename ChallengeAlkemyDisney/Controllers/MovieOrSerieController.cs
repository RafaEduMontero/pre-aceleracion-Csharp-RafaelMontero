using ChallengeAlkemyDisney.Models;
using ChallengeAlkemyDisney.Repositories;
using ChallengeAlkemyDisney.ViewModels.MoOrSerie;
using ChallengeAlkemyDisney.ViewModels.MovOrSerie;
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
    public class MovieOrSerieController : ControllerBase
    {
        private readonly IMovieOrSerieRepository _movieOrSerieRepository;
        private readonly ICelebrityRepository _celebrityRepository;
        public MovieOrSerieController(IMovieOrSerieRepository movieOrSerieRepository, ICelebrityRepository celebrityRepository)
        {
            _movieOrSerieRepository = movieOrSerieRepository;
            _celebrityRepository = celebrityRepository;
        }
        // GET
        [HttpGet("movies")]
        public IActionResult Get()
        {
            try
            {
                var movieOrSeries = _movieOrSerieRepository.GetAllMovieOrSeries();
                var mosViewModel = new List<MsResponseViewModel>();
                foreach (var mos in movieOrSeries)
                {
                    var msViewModel = new MsResponseViewModel
                    {
                        Image = mos.Image,
                        Title = mos.Title,
                        CreationDate = mos.CreationDate
                    };

                    mosViewModel.Add(msViewModel);
                }
                return Ok(mosViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get(string title, string gender)
        {
            try
            {
                var movies = _movieOrSerieRepository.GetAllMovieOrSeries();
                var moviesViewModel = new List<MsResponseViewModel>();

                if (!string.IsNullOrEmpty(title))
                {
                    var movie = movies.Find(m => m.Title == title);
                    if (movie != null)
                    {
                        var movieViewModel = new MsResponseViewModel
                        {
                            Image = movie.Image,
                            Title = movie.Title,
                            CreationDate = movie.CreationDate
                        };
                        moviesViewModel.Add(movieViewModel);
                    }
                    else
                    {
                        return BadRequest($"La palícula con el Título {title} no existe");
                    }
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    var moviesFilter = movies.Where(m => m.Gender.Name == gender);
                    if (moviesFilter != null)
                    {
                        foreach (var m in moviesFilter)
                        {
                            var movieViewModel = new MsResponseViewModel
                            {
                                Image = m.Image,
                                Title = m.Title,
                                CreationDate = m.CreationDate
                            };
                            moviesViewModel.Add(movieViewModel);
                        }
                    }
                }

                if (movies == null) return NoContent();

                return Ok(moviesViewModel);
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
                MovieOrSerie moSerieObtenido = _movieOrSerieRepository.GetMovieOrSerie(id);
                if (moSerieObtenido == null) return BadRequest($"La pelicula con Id:{id} no existe");
                return Ok(moSerieObtenido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST 
        [HttpPost]
        public IActionResult Post(MsRequestViewModel msRequestViewModel)
        {
            var msOriginal = _movieOrSerieRepository.GetAllMovieOrSeries().FirstOrDefault(ms => ms.Title == msRequestViewModel.Title);

            if (msOriginal != null)
            {
                return BadRequest($"La película o serie con Título {msRequestViewModel.Title} ya existe");
            }

            var movieOrSerie = new MovieOrSerie
            {
                Image = msRequestViewModel.Image,
                Title = msRequestViewModel.Title,
                CreationDate = msRequestViewModel.CreationDate,
                Valuation = msRequestViewModel.Valuation,
                Gender = msRequestViewModel.IdGender
            };

            if (msRequestViewModel.Celebrities != null)
            {
                foreach(var c in msRequestViewModel.Celebrities)
                {
                    if(c.Id != 0)
                    {
                        var celebrity = _celebrityRepository.GetAllCelebrities().FirstOrDefault(ce => ce.Id == c.Id);
                        if(celebrity != null)
                        {
                            movieOrSerie.Celebrities.Add(celebrity);
                        }
                    }
                }
            }

            return Ok(_movieOrSerieRepository.AddMovieOrSerie(movieOrSerie));
        }

        // PUT 
        [HttpPut]
        public IActionResult Put(MovieOrSerie movieOrSerie)
        {
            var originaMovie = _movieOrSerieRepository.GetMovieOrSerie(movieOrSerie.Id);

            if (originaMovie == null) return BadRequest($"La película o serie con id {movieOrSerie.Id} no existe");

            originaMovie.Image = movieOrSerie.Image;
            originaMovie.Title = movieOrSerie.Title;
            originaMovie.CreationDate = movieOrSerie.CreationDate;
            originaMovie.Valuation = movieOrSerie.Valuation;
            originaMovie.Gender = movieOrSerie.Gender;

            if (movieOrSerie.Celebrities != null)
            {
                foreach(var c in movieOrSerie.Celebrities)
                {
                    if (c.Id != 0)
                    {
                        var celebrity = _celebrityRepository.GetAllCelebrities().FirstOrDefault(ce => ce.Id == c.Id);
                        if (celebrity != null)
                        {
                            originaMovie.Celebrities.Add(celebrity);
                        }
                    }
                }
            }

            _movieOrSerieRepository.UpdateMovieOrSerie(originaMovie);

            return Ok(originaMovie);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _movieOrSerieRepository.DeleteMovieOrSerie(id);
        }
    }
}
