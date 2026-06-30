using BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IPurchaseService
    {
        void CreatePurchase(Purchase purchase);

        List<Purchase> GetAllPurchases();

        decimal GetTotalRevenue();

        Book? GetBestSellingBook();

        Customer? GetTopCustomer();
    }
}
