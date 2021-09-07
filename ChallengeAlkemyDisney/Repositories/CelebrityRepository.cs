using ChallengeAlkemyDisney.Context;
using ChallengeAlkemyDisney.Models;
using System.Collections.Generic;

namespace ChallengeAlkemyDisney.Repositories
{
    public class CelebrityRepository : BaseRepository<Celebrity,DisneyContext>, ICelebrityRepository
    {
        public CelebrityRepository(DisneyContext dbContext) : base(dbContext)
        {

        }
        public List<Celebrity> GetAllCelebrities()
        {
            return GetAllModels();
        }

        public Celebrity GetCelebrity(int id)
        {
            return Get(id);
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
