using ChallengeAlkemyDisney.Context;
using ChallengeAlkemyDisney.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Repositories
{
    public class GenderRepository : BaseRepository<Gender, DisneyContext>, IGenderRepository
    {
        public GenderRepository(DisneyContext dbContext) : base(dbContext)
        {
            
        }

        public List<Gender> GetAllGenders()
        {
            return GetAllModels();
        }

        public Gender GetGender(int id)
        {
            return Get(id);
        }

        public Gender AddGender(Gender gender)
        {
            return Add(gender);
        }

        public Gender UpdateGender(Gender gender)
        {
            return Update(gender);
        }

        public void DeleteGender(int id)
        {
            Delete(id);
        }
    }
}
