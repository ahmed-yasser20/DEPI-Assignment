using BookStore.Entity;
using BookStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public List<Customer> GetAllCustomers()
        {
            return _repository.GetAll();
        }

        public Customer? GetCustomerById(int id)
        {
            return _repository.GetById(id);
        }

        public void RegisterCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new Exception("Customer name is required.");

            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new Exception("Email is required.");

            if (string.IsNullOrWhiteSpace(customer.City))
                throw new Exception("City is required.");

            bool exists = _repository
                .GetAll()
                .Any(c => c.Email == customer.Email);

            if (exists)
                throw new Exception("Email already exists.");

            _repository.Add(customer);
        }

        public Customer? SearchCustomer(int id)
        {
            return _repository.GetById(id);
        }
    }
}
