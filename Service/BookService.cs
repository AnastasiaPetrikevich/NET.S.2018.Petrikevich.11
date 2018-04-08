using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLogic;
using Storage;
using Logs;
using System.Collections;

namespace Service
{
    public class BookService : IEnumerable<Book>
    {
        /// <summary>
        /// List of Book.
        /// </summary>
        private List<Book> books = new List<Book>();
        
        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public BookService(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BookService(IEnumerable<Book> books, ILogger logger) : this(logger)
        {
            foreach (Book book in books)
            {
                AddBook(book);
            }
        }

        /// <summary>
        /// Add book to the list of books.
        /// </summary>
        /// <param name="book">Book to be added.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null.</exception>
        /// <exception cref="InvalidOperationException">This book is already exist in list.</exception>
        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (IsContains(book))
            {
                throw new InvalidOperationException(nameof(book));
            }

            books.Add(book);
            logger.Debug($"Book {book.Title} successfully added.");
        }

        /// <summary>
        /// Remove book from the list of books.
        /// </summary>
        /// <param name="book">Book to be removed.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null.</exception>
        /// <exception cref="InvalidOperationException">No such element in list.</exception>
        public void RemoveBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (!IsContains(book))
            {
                throw new InvalidOperationException(nameof(book));
            }

            foreach (Book b in books)
            {
                if (b.Equals(book))
                {
                    books.Remove(b);
                }
            }

            logger.Debug($"Book {book.Title} successfully removed.");
        }

        /// <summary>
        /// Find book by tag.
        /// </summary> 
        /// <param name="predicate"> Tag by which fiding.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null.</exception>
        /// <returns>List of found books.</returns>
        public IEnumerable<Book> FindBookByTag(IPredicate<Book> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return books.Where(predicate.IsMatch);
        }

        /// <summary>
        /// Sort book by tag.
        /// </summary> 
        /// <param name="comparer"> Tag by which sorting.</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            books.Sort(comparer);
        }

        /// <summary>
        /// Saving books to binary file
        /// </summary>
        /// <param name="storage">Storage provider</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        public void SaveToStorage(IBookListStorage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            storage.Save(books);
            logger.Debug($"List of books saved to the storage");
        }

        /// <summary>
        /// Loading books from binary file
        /// </summary>
        /// <param name="storage"></param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        /// <returns>Before safed BookListServide</returns>
        public void LoadFromStorage(IBookListStorage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));

            }

            foreach (Book book in storage.Load())
            {
                AddBook(book);
            }

            logger.Debug($"List of books loaded from storage");
        }

        /// <summary>
        /// Check if the book contains in the books.
        /// </summary>
        /// <param name="book"></param>
        /// <returns>True if contains.</returns>
        private bool IsContains(Book book)
        {
            if (book == null)
            {
                return false;
            }

            foreach (Book i in books)
            {
                if (book.Equals(i))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<Book> GetEnumerator()
        {
            foreach (Book book in books)
            {
                yield return book;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
