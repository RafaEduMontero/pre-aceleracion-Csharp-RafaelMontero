using ChallengeAlkemyDisney.Interfaces;
using ChallengeAlkemyDisney.Models;
using System.Collections.Generic;

namespace ChallengeAlkemyDisney.Repositories
{
    public interface IGenderRepository : IRepository<Gender>
    {
        List<Gender> GetAllGenders();
        Gender GetGender(int id);
        Gender AddGender(Gender gender);
        Gender UpdateGender(Gender genderOriginal);
        void DeleteGender(int id);
    }
}