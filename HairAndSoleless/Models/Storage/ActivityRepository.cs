using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HairAndSoleless.Models.Storage
{ 
    public class ActivityRepository : IActivityRepository
    {
        HairAndSolelessContext context = new HairAndSolelessContext();

        public IEnumerable<Activity> GetAllActivities(params Expression<Func<Activity, object>>[] includeProperties)
        {
            IQueryable<Activity> query = context.Activities;

            try
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return query.ToList();
        }

        public Activity GetById(int id)
        {
            return context.Activities.Find(id);
        }

        public void InsertOrUpdate(Activity activity)
        {
            if (activity.ActivityId == default(int)) {
                // New entity
                context.Activities.Add(activity);
            } else {
                // Existing entity
                context.Activities.Attach(activity);
                context.Entry(activity).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var activity = context.Activities.Find(id);
            context.Activities.Remove(activity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}