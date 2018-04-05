using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLogic;
using Service;

namespace ConsoleUI
{
    /// <summary>
    /// Pregicates to find.
    /// </summary>
    
    public class TitlePredicate : IPredicate<Book>
    {
        private string title;

        public TitlePredicate(string title)
        {
            this.title = title;
        }

        public bool IsMatch(Book book)
        {
            return book.Title == title;
        }
    }

    public class AuthorNamePredicate : IPredicate<Book>
    {
        private string authorName;

        public AuthorNamePredicate(string authorName)
        {
            this.authorName = authorName;
        }

        public bool IsMatch(Book book)
        {
            return book.AuthorName == authorName;
        }
    }

    public class YearPredicate : IPredicate<Book>
    {
        private int year;

        public YearPredicate(int year)
        {
            this.year = year;
        }

        public bool IsMatch(Book book)
        {
            return book.Year == year;
        }
    }
}
