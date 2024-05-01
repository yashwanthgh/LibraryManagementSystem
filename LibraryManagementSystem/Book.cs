using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    // Book: Represents a book with properties like title, author, ISBN, etc.
    public interface IBook
    {
        public int ISBN { get; }
        public string Title { get; }
        public string Author { get; }
        public string Genre { get; }
        public int InStock { get; set; } 
    }

    public class Book(int isbn, string title, string author, string genre, int inStock) : IBook
    {
        public int ISBN { get; } = isbn;
        public string Title { get; } = title;
        public string Author { get; } = author;
        public string Genre { get; } = genre;
        public int InStock { get; set; }  = inStock;
    }
}
