using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entity
{
    public class PurchaseItem
    {
        public Book Book { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
