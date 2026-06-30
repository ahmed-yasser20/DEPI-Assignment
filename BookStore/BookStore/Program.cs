using BookStore.Entity;
using BookStore.Repositories;
using BookStore.Services;

namespace BookStore
{
    internal class Program
    {
        private static IRepository<Book> bookRepository =
            new InMemoryRepository<Book>();

        private static IRepository<Customer> customerRepository =
            new InMemoryRepository<Customer>();

        private static IRepository<Purchase> purchaseRepository =
            new InMemoryRepository<Purchase>();

        private static IBookService bookService =
            new BookService(bookRepository);

        private static ICustomerService customerService =
            new CustomerService(customerRepository);

        private static IPurchaseService purchaseService =
            new PurchaseService(
                purchaseRepository,
                bookRepository);

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();

                ShowMenu();

                Console.Write("\nChoose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input.");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddBook();
                            break;

                        case 2:
                            RemoveBook();
                            break;

                        case 3:
                            SearchBook();
                            break;

                        case 4:
                            ListBooks();
                            break;

                        case 5:
                            RegisterCustomer();
                            break;

                        case 6:
                            CreatePurchase();
                            break;

                        case 0:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }

        }


    }
}