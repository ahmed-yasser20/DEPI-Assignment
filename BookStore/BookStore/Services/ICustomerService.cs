using BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface ICustomerService
    {
        void RegisterCustomer(Customer customer);

        Customer? SearchCustomer(int id);
        Customer? GetCustomerById(int id);

        List<Customer> GetAllCustomers();
    }
}
