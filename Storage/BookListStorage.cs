using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookLogic;

namespace Storage
{
    public class BookListStorage : IBookListStorage
    {
        /// <summary>
        /// File path.
        /// </summary>
        private string filePath;

        /// <summary>
        /// Constructor BookListStorage
        /// </summary>
        public BookListStorage(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        /// <summary>
        /// Saving data to binary file
        /// </summary>
        /// <param name="books">Data to be saved</param>
        public void Save(IEnumerable<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                foreach (Book b in books)
                {
                    writer.Write(b.ISBN);
                    writer.Write(b.AuthorName);
                    writer.Write(b.Title);
                    writer.Write(b.Publisher);
                    writer.Write(b.NumberOfPages);
                    writer.Write(b.Year);
                    writer.Write(b.Price);
                }
            }
        }

        /// <summary>
        /// Loading data from binary file
        /// </summary>
        /// <exception cref="InvalidOperationException">FilePath is wrong</exception>
        /// <returns>before saved data from binary file</returns>
        public IEnumerable<Book> Load()
        {
            if (!File.Exists(filePath)) throw new InvalidOperationException("Enter a correct filepath");

            List<Book> books = new List<Book>();
            
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string isbn = reader.ReadString();
                    string authorName = reader.ReadString();
                    string title = reader.ReadString();
                    string publisher = reader.ReadString();
                    int numberOfPages = reader.ReadInt32();
                    int year = reader.ReadInt32();
                    decimal price = reader.ReadDecimal();

                    books.Add(new Book(isbn, authorName, title, publisher, numberOfPages, year, price));
                }
            }

            return books;
        }
    }
}
