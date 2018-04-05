using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookLogic
{
    public class Book : IComparable, IComparable<Book>, IEquatable<Book>, IFormattable
    {
        #region Fields
        /// <summary>
        /// ISBN.
        /// </summary>
        private string isbn;
        /// <summary>
        /// Author name.
        /// </summary>
        private string authorName;
        /// <summary>
        /// Book title.
        /// </summary>
        private string title;
        /// <summary>
        /// Publishing house.
        /// </summary>
        private string publisher;
        /// <summary>
        /// Year of publication
        /// </summary>
        private int year;
        /// <summary>
        /// Number of pages.
        /// </summary>
        private int numberOfPages;
        /// <summary>
        ///Book price.
        /// </summary>
        private decimal price;
        #endregion

        #region Properties
        /// <summary>
        /// Property for isbn.
        /// </summary>
        public string ISBN
        {
            get => isbn;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }

                var regex = new Regex("(ISBN[-]*(1[03])*[ ]*(: ){0,1})*(([0-9Xx][- ]*){13}|([0-9Xx][- ]*){10})");

                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }

                isbn = value;
            }
        }

        /// <summary>
        /// Property for authorName.
        /// </summary>
        public string AuthorName
        {
            get => authorName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }

                authorName = value;
            }
        }

        /// <summary>
        /// Property for title.
        /// </summary>
        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }

                title = value;
            }
        }

        /// <summary>
        /// Property for publisher..
        /// </summary>
        public string Publisher
        {
            get => publisher;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }

                publisher = value;
            }
        }

        /// <summary>
        /// Property for year.
        /// </summary>
        public int Year
        {
            get => year;
            set
            {
                if (value < 0 || value > DateTime.Today.Year)
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }

                year = value;
            }
        }

        /// <summary>
        /// Property for numberOfPages.
        /// </summary>
        public int NumberOfPages
        {
            get => numberOfPages;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }

                numberOfPages = value;
            }
        }

        /// <summary>
        /// Property for price.
        /// </summary>
        public decimal Price
        {
            get => price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }

                price = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Book
        /// </summary>
        public Book(string isbn, string authorName, string title, string publisher, int year, int numberOfPages, decimal price)
        {
            ISBN = isbn;
            AuthorName = authorName;
            Title = title;
            Publisher = publisher;
            Year = year;
            NumberOfPages = numberOfPages;
            Price = price;
        }

        #endregion

        #region Override methos
        /// <summary>
        /// override ToString() methods.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString() => $"ISBN 13: {ISBN}, AuthorName: {AuthorName}, Title: {Title}, Publisher: {Publisher}, Year: {Year}, Number of pages: {NumberOfPages}, Price: {Price}";

        /// <summary>
        /// override Equals() methods.
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>True if objects are equivalent, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (obj.GetType() == GetType())
            {
                Book book = obj as Book;
                return this.Equals(book);
            }

            return false;
        }

        /// <summary>
        /// override Equals() methods.
        /// </summary>
        /// <returns>Hash code with isbn, publish house,  year, name divided by 4</returns>
        public override int GetHashCode() => ISBN.GetHashCode() + AuthorName.GetHashCode() + Title.GetHashCode();

        #endregion

        #region IComparable, IComparable<Book> and IEquatable<Book> Methods

        /// <summary>
        /// IComparable CompareTo
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>1 if obj more then this, and -1 otherwise</returns>
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            return obj.GetType() != GetType() ? 1 : CompareTo((Book)obj);
        }

        /// <summary>
        /// IComparable(Book) CompareTo
        /// </summary>
        /// <param name="other">object to compare</param>
        /// <returns>1 if other more then this, and -1 otherwise</returns>
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            return ReferenceEquals(other, null) ? 1 : string.Compare(AuthorName, other.AuthorName, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// IEquatable Equals
        /// </summary>
        /// <param name="other">object to compare</param>
        /// <returns>true if ohter and this equals.</returns>
        public bool Equals(Book other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return other.ISBN == ISBN;
        }
        #endregion

        #region IFormattable

        /// <summary>
        /// IIFormattable ToString
        /// </summary>
        /// <param name="format">Type of format.</param>
        /// <param name="formatProvider"> Format provider.</param>
        /// <returns>String representation.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (ReferenceEquals(formatProvider, null))
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                format = "G";
            }

            switch (format)
            {
                case "AT":
                    return $"AuthorName: {AuthorName}, Title: {Title}";
                case "ATPY":
                    return $"AuthorName: {AuthorName}, Title: {Title}, Publisher: {Publisher}, Year: {Year.ToString(formatProvider)}";
                case "IATPYP":
                    return $"ISBN 13: {ISBN}, AuthorName: {AuthorName}, Title: {Title}, Publisher: {Publisher}, Year: {Year.ToString(formatProvider)}, Price: {Price.ToString(formatProvider)}";
                case "ATPYP":
                    return $"AuthorName: {AuthorName}, Title: {Title}, Publisher: {Publisher}, Price: {Price.ToString(formatProvider)}";
                case "G":
                    return $"ISBN 13: {ISBN}, AuthorName: {AuthorName}, Title: {Title}, Publisher: {Publisher}, Year: {Year.ToString(formatProvider)}, Number of pages: {NumberOfPages.ToString(formatProvider)}, Price: {Price.ToString(formatProvider)}";
                default:
                    throw new FormatException($"The {format} format string is error");
            }
        }

        #endregion
    }
}
