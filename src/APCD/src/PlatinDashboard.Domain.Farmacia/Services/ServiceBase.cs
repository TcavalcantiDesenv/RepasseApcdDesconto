using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual TEntity Add(TEntity obj, DbConnection dbConnection)
        {
            return _repository.Add(obj, dbConnection);
        }

        public virtual IEnumerable<TEntity> GetAll(DbConnection dbConnection)
        {
            return _repository.GetAll(dbConnection);
        }

        public virtual TEntity GetById(int id, DbConnection dbConnection)
        {
            return _repository.GetById(id, dbConnection);
        }

        public virtual void Remove(TEntity obj, DbConnection dbConnection)
        {
            _repository.Remove(obj, dbConnection);
        }

        public void Update(TEntity obj, DbConnection dbConnection)
        {
            _repository.Update(obj, dbConnection);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> consulta, DbConnection dbConnection)
        {
            return _repository.GetByFilter(consulta, dbConnection);
        }
    }
}
