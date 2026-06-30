using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entity
{
    public class Purchase : BaseEntity
    {
        public Customer Customer { get; set; }

        public DateTime PurchaseDate { get; set; }

        public List<PurchaseItem> Items { get; set; } = new();
    }
}
