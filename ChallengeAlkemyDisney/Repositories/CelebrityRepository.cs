using ChallengeAlkemyDisney.Context;
using ChallengeAlkemyDisney.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChallengeAlkemyDisney.Repositories
{
    public class CelebrityRepository : BaseRepository<Celebrity,DisneyContext>, ICelebrityRepository
    {
        public CelebrityRepository(DisneyContext dbContext) : base(dbContext)
        {

        }
        public List<Celebrity> GetAllCelebrities()
        {
            return DbSet.Include(g => g.MovieOrSeries).ToList();
        }

        public Celebrity GetCelebrity(int id)
        {
            return DbSet.Include(g => g.MovieOrSeries).FirstOrDefault(g => g.Id == id);
        }

        public Celebrity AddCelebrity(Celebrity celebrity)
        {
            return Add(celebrity);
        }

        public Celebrity UpdateCelebrity(Celebrity celebrity)
        {
            return Update(celebrity);
        }

        public void DeleteCelebrity(int id)
        {
            Delete(id);
        }
    }
}
