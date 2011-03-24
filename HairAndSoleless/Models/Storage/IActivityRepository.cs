using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HairAndSoleless.Models.Storage
{
    public interface IActivityRepository
    {
        IEnumerable<Activity> GetAllActivities(params Expression<Func<Activity, object>>[] includeProperties);
        Activity GetById(int id);
        void InsertOrUpdate(Activity activity);
        void Delete(int id);
        void Save();
    }
}