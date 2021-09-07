using ChallengeAlkemyDisney.Context;
using ChallengeAlkemyDisney.Models;

namespace ChallengeAlkemyDisney.Repositories
{
    public class MovieOrSerieRepository : BaseRepository<MovieOrSerie, DisneyContext>, IMovieOrSerieRepository
    {
        public MovieOrSerieRepository(DisneyContext dbContext) : base(dbContext)
        {
        }

        public MovieOrSerie AddMovieOrSerie(MovieOrSerie movieOrSerie)
        {
            return Add(movieOrSerie);
        }
    }
}
