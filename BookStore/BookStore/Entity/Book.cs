using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entity
{
    public abstract class Book : BaseEntity
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Category Category { get; set; }

        public Author Author { get; set; }

        
    }
}
