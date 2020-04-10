using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Infra.Data.Farmacia.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace PlatinDashboard.Infra.Data.Farmacia.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public TEntity Add(TEntity obj, DbConnection dbConnection)
        {
            var db = new PostgreContext(dbConnection);
            var entity = db.Set<TEntity>().Add(obj);
            db.SaveChanges();
            return entity;
        }
        
        public IEnumerable<TEntity> GetAll(DbConnection dbConnection)
        {
            var db = GetContext(dbConnection);
            return db.Set<TEntity>().AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> consulta, DbConnection dbConnection)
        {
            var db = GetContext(dbConnection);
            return db.Set<TEntity>().AsNoTracking().Where(consulta).ToList();
        }

        public TEntity GetById(int id, DbConnection dbConnection)
        {
            var db = GetContext(dbConnection);
            return db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj, DbConnection dbConnection)
        {
            var db = GetContext(dbConnection);
            db.Set<TEntity>().Attach(obj);
            db.Set<TEntity>().Remove(obj);
            db.SaveChanges();
        }

        public void Update(TEntity obj, DbConnection dbConnection)
        {
            var db = GetContext(dbConnection);
            db.Set<TEntity>().AddOrUpdate(obj);
            db.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public DbContext GetContext(DbConnection dbConnection)
        {
            //Verificando qual provider deve ser utilizado na conexão
            if (dbConnection.ToString() == "MySql.Data.MySqlClient.MySqlConnection")
            {
                return new MySqlContext(dbConnection);
            }
            return new PostgreContext(dbConnection);
        }
    }
}
