using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HairAndSoleless.Models.Storage
{ 
    public class CoachRepository : ICoachRepository
    {
        HairAndSolelessContext context = new HairAndSolelessContext();

        public IEnumerable<Coach> GetAllCoaches(params Expression<Func<Coach, object>>[] includeProperties)
        {
            IQueryable<Coach> query = context.Coaches;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public Coach GetById(int id)
        {
            return context.Coaches.Find(id);
        }

        public void InsertOrUpdate(Coach coach)
        {
            if (coach.CoachId == default(int)) {
                // New entity
                context.Coaches.Add(coach);
            } else {
                // Existing entity
                context.Coaches.Attach(coach);
                context.Entry(coach).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var coach = context.Coaches.Find(id);
            context.Coaches.Remove(coach);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}