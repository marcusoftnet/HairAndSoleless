using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HairAndSoleless.Models.Storage
{ 
    public class CustomerRepository : ICustomerRepository
    {
        HairAndSolelessContext context = new HairAndSolelessContext();

        public IEnumerable<Customer> GetAllCustomers(params Expression<Func<Customer, object>>[] includeProperties)
        {
            IQueryable<Customer> query = context.Customers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public Customer GetById(int id)
        {
            return context.Customers.Find(id);
        }

        public void InsertOrUpdate(Customer customer)
        {
            if (customer.CustomerId == default(int)) {
                // New entity
                context.Customers.Add(customer);
            } else {
                // Existing entity
                context.Customers.Attach(customer);
                context.Entry(customer).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

}