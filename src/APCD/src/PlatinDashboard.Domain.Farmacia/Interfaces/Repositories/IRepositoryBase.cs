using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity obj, DbConnection dbConnection);
        TEntity GetById(int id, DbConnection dbConnection);
        IEnumerable<TEntity> GetAll(DbConnection dbConnection);
        IEnumerable<TEntity> GetByFilter(System.Linq.Expressions.Expression<Func<TEntity, bool>> consulta, DbConnection dbConnection);
        void Update(TEntity obj, DbConnection dbConnection);
        void Remove(TEntity obj, DbConnection dbConnection);
        DbContext GetContext(DbConnection dbConnection);
        void Dispose();
    }
}
