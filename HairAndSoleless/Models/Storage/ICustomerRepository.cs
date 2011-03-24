using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HairAndSoleless.Models.Storage
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers(params Expression<Func<Customer, object>>[] includeProperties);
        Customer GetById(int id);
        void InsertOrUpdate(Customer customer);
        void Delete(int id);
        void Save();
    }
}