using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity obj, DbConnection dbConnection);
        TEntity GetById(int id, DbConnection dbConnection);
        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> consulta, DbConnection dbConnection);
        IEnumerable<TEntity> GetAll(DbConnection dbConnection);
        void Update(TEntity obj, DbConnection dbConnection);
        void Remove(TEntity obj, DbConnection dbConnection);
        void Dispose();
    }
}
