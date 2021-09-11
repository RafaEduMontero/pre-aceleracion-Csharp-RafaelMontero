using ChallengeAlkemyDisney.Interfaces;
using ChallengeAlkemyDisney.Models;
using System.Collections.Generic;

namespace ChallengeAlkemyDisney.Repositories
{
    public interface ICelebrityRepository : IRepository<Celebrity>
    {
        List<Celebrity> GetAllCelebrities();
        Celebrity GetCelebrity(int id);
        Celebrity AddCelebrity(Celebrity celebrity);
        Celebrity UpdateCelebrity(Celebrity celebrity);
        void DeleteCelebrity(int id);
    }
}