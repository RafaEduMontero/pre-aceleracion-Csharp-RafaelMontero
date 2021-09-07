using ChallengeAlkemyDisney.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Repositories
{
    public abstract class BaseRepository<TModel, TContext> : IRepository<TModel>
        where TModel : class
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        protected BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TModel> GetAllModels()
        {
            return _dbContext.Set<TModel>().ToList();
        }
        public TModel Add(TModel model)
        {
            _dbContext.Set<TModel>().Add(model);
            _dbContext.SaveChanges();
            return model;
        }

        public void Delete(int id)
        {
            TModel model = _dbContext.Find<TModel>(id);
            _dbContext.Remove(model);
        }

        public TModel Get(int id)
        {
            return _dbContext.Set<TModel>().Find(id);
        }

        public TModel Update(TModel model)
        {
            _dbContext.Attach(model);
            _dbContext.Entry(model).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return model;
        }
    }
}
