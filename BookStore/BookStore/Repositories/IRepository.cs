using BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T entity);

        void Remove(int id);

        T? GetById(int id);

        List<T> GetAll();

        void Update(T entity);
    }
}
