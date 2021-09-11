using ChallengeAlkemyDisney.Interfaces;
using ChallengeAlkemyDisney.Models;
using System.Collections.Generic;

namespace ChallengeAlkemyDisney.Repositories
{
    public interface IMovieOrSerieRepository : IRepository<MovieOrSerie>
    {
        List<MovieOrSerie> GetAllMovieOrSeries();
        MovieOrSerie GetMovieOrSerie(int id);
        MovieOrSerie AddMovieOrSerie(MovieOrSerie movieOrSerie);
        MovieOrSerie UpdateMovieOrSerie(MovieOrSerie movieOrSerie);
        void DeleteMovieOrSerie(int id);
    }
}