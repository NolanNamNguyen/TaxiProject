using System;
using System.Collections.Generic;
using System.Text;

namespace Taxi.Domain.Interfaces
{
    public interface IBasicRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
    }
}
