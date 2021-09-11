using ChallengeAlkemyDisney.Context;
using ChallengeAlkemyDisney.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeAlkemyDisney.Repositories
{
    public class MovieOrSerieRepository : BaseRepository<MovieOrSerie, DisneyContext>, IMovieOrSerieRepository
    {
        public MovieOrSerieRepository(DisneyContext dbContext) : base(dbContext)
        {
        }

        public List<MovieOrSerie> GetAllMovieOrSeries()
        {
            return DbSet.Include(c => c.Celebrities).ToList();
        }

        public MovieOrSerie GetMovieOrSerie(int id)
        {
            return DbSet.Include(c => c.Celebrities).FirstOrDefault(c => c.Id == id);
        }

        public MovieOrSerie AddMovieOrSerie(MovieOrSerie movieOrSerie)
        {
            return Add(movieOrSerie);
        }

        public MovieOrSerie UpdateMovieOrSerie(MovieOrSerie movieOrSerie)
        {
            return Update(movieOrSerie);
        }

        public void DeleteMovieOrSerie(int id)
        {
            Delete(id); 
        }
    }
}
