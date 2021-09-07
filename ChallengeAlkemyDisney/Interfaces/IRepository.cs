using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAllModels();
        public T Get(int id);
        public T Add(T entity);
        public T Update(T entity);
        public void Delete(int id);
    }
}
