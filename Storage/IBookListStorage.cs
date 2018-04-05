using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLogic;

namespace Storage
{
    public interface IBookListStorage
    {
        void Save(IEnumerable<Book> books);
        IEnumerable<Book> Load();
    }
}
