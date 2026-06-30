using BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class InMemoryRepository<T> : IRepository<T>
    where T : BaseEntity
    {
        private readonly List<T> _items = new();
        private int _nextId = 1;

        public void Add(T entity)
        {
            entity.Id = _nextId++;
            _items.Add(entity);
        }

        public List<T> GetAll()
        {
            return _items.ToList();
        }

        public T? GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var entity = GetById(id);

            if (entity != null)
            {
                _items.Remove(entity);
            }
        }

        public void Update(T entity)
        {
            var index = _items.FindIndex(x => x.Id == entity.Id);

            if (index != -1)
            {
                _items[index] = entity;
            }
        }
    }

}
