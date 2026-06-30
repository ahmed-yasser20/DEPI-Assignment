using BookStore.Entity;
using BookStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IRepository<Book> _bookRepository;

        public PurchaseService(
            IRepository<Purchase> purchaseRepository,
            IRepository<Book> bookRepository)
        {
            _purchaseRepository = purchaseRepository;
            _bookRepository = bookRepository;
        }

        public void CreatePurchase(Purchase purchase)
        {
            if (purchase.Customer == null)
                throw new Exception("Customer is required.");

            if (purchase.Items.Count == 0)
                throw new Exception("Purchase must contain at least one book.");

            foreach (var item in purchase.Items)
            {
                if (item.Quantity <= 0)
                    throw new Exception("Quantity must be greater than zero.");

                if (item.Book.Stock < item.Quantity)
                    throw new Exception($"'{item.Book.Title}' is out of stock.");

                item.UnitPrice = item.Book.Price;

                item.Book.Stock -= item.Quantity;
            }

            purchase.PurchaseDate = DateTime.Now;

            _purchaseRepository.Add(purchase);
        }

        public List<Purchase> GetAllPurchases()
        {
            return _purchaseRepository.GetAll();
        }

        public Book? GetBestSellingBook()
        {
            return _purchaseRepository
                .GetAll()
                .SelectMany(p => p.Items)
                .GroupBy(i => i.Book)
                .OrderByDescending(g => g.Sum(i => i.Quantity))
                .Select(g => g.Key)
                .FirstOrDefault();
        }

        public Customer? GetTopCustomer()
        {
            return _purchaseRepository
                .GetAll()
                .GroupBy(p => p.Customer)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();
        }

        public decimal GetTotalRevenue()
        {
            return _purchaseRepository
                .GetAll()
                .Sum(p => p.Items.Sum(i => i.UnitPrice * i.Quantity));
        }
    }
}
