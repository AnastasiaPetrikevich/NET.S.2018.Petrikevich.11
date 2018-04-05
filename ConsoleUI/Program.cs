using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLogic;
using Logs;
using Service;
using Storage;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            Book book1 = new Book("123-4-56-78910-1", "Author1", "title1", "publisher1", 2012, 345, 56.10m);

            Console.WriteLine(book1.ToString("G",null));
            Console.WriteLine(book1.ToString("AT", null));
            Console.WriteLine(book1.ToString("ATPYP", null));

            Book book2 = new Book("123-4-56-78910-2", "Author1", "title2", "publisher2", 2012, 1051, 78.10m);
            Book book3 = new Book("123-4-56-78910-3", "Author2", "title3", "publisher1", 2015, 1237, 45.10m);
            List<Book> books = new List<Book> { book1, book2 };
            BookService service = new BookService(books, new NLogger(), new BookListStorage(@"storage.bin"));
            Show(service);
            service.AddBook(book3);
            Show(service);
            service.SaveToStorage();
            service.FindBookByTag(new AuthorNamePredicate("Author1"));
            service.RemoveBook(book1);
            Show(service);

            Console.ReadKey();

        }

        public static void Show(BookService service)
        {
            foreach (Book book in service)
            {
                Console.WriteLine(book.ToString("G", null));
            }
        }
    }
}
