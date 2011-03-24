using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HairAndSoleless.Models.Storage
{
    public interface ICoachRepository
    {
        IEnumerable<Coach> GetAllCoaches(params Expression<Func<Coach, object>>[] includeProperties);
        Coach GetById(int id);
        void InsertOrUpdate(Coach coach);
        void Delete(int id);
        void Save();
    }
}