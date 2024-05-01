using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public interface ILibrary
    {
        public void BorrowBook(int userId, int ISBN);
        public void ReturnBook(int userId, int ISBN);
    }
    public class Library : ILibrary
    {
        private static readonly object borrowLock = new object();
        private static readonly object returnLock = new object();


        private static List<IBook> _books = [];
        private static List<IUser> _users = [];
        private static Dictionary<int, List<IBook>> _usersBooks = [];
        private static int _userIDs = 100;

        public Library()
        {
            PopulateBooks();
            PopulateUsers();
            AssignBooksToUsers();
        }

        // Borrow Book
        public void BorrowBook(int userId, int ISBN)
        {
            lock (borrowLock)
            {
                IBook? book = FindBook(ISBN);
                if (book == null)
                {
                    Console.WriteLine("The Book is OUT OF STOCK!");
                    return;
                }
                IUser? user = FindUser(userId);

                if (user != null)
                {
                    int currentUserId = user.Id;
                    if (_usersBooks.ContainsKey(currentUserId))
                    {
                        _usersBooks[currentUserId].Add(book);
                        book.InStock--;
                        Console.WriteLine($"The Book '{book.Title}' is Borrowed by {user.Name} ");
                    } else
                    {
                        _usersBooks.Add(currentUserId, []);
                        _usersBooks[currentUserId].Add(book);
                        book.InStock--;
                        Console.WriteLine($"The Book '{book.Title}' is Borrowed by {user.Name} ");
                    }
                }
            }
        }

        public void BorrowBook()
        {
            IUser user = AddUser();
            int id = user.Id;
            Console.WriteLine($"User ID is {id} for Name {user.Name}" );
            Console.WriteLine("Enter the Book ISBN: ");
            int isbn = Convert.ToInt32(Console.ReadLine());
            BorrowBook(id, isbn);
        }

        // Return Book
        public void ReturnBook(int userId, int ISBN)
        {
            lock (returnLock)
            {
                IBook? book = FindBook(ISBN);
                if (book == null)
                {
                    Console.WriteLine("Book Don't belong here!");
                    return;
                }

                List<IBook> books = _usersBooks[userId];
                bool result = true;
                foreach (IBook b in books)
                {
                    if (b.ISBN == ISBN)
                    {
                        result = false;
                        break;
                    };
                }
                if (books.Count < 1 || result)
                {
                    Console.WriteLine("Nothing to return!");
                }
                else
                {
                    books.Remove(book);
                    Console.WriteLine($"The Book {book.Title} is Returned!");
                }
            }
        }

        private static IBook? FindBook(int isbn)
        {
            IBook? book = _books.FirstOrDefault(b => b.ISBN == isbn && b.InStock > 0);

            if (book != null)
            {
                return book;
            }
            return null;
        }

        private static IUser? FindUser(int userId)
        {
            IUser? user = _users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return user;
            }
            return null;
        }


        private static IUser AddUser()
        {
            Console.WriteLine("Enter the Name of the USER: ");
            string? name = Console.ReadLine();
            Console.WriteLine("Enter the Email: ");
            string? email = Console.ReadLine();
            IUser user = new User(++_userIDs, name, email);
            _users.Add(user);
            return user;
        }

        private static void PopulateBooks()
        {
            _books.Add(new Book(1, "Harry Potter and the Sorcerer's Stone", "J.K. Rowling", "Fantasy", 10));
            _books.Add(new Book(2, "Harry Potter and the Chamber of Secrets", "J.K. Rowling", "Fantasy", 15));
            _books.Add(new Book(3, "Harry Potter and the Prisoner of Azkaban", "J.K. Rowling", "Fantasy", 20));
            _books.Add(new Book(4, "Harry Potter and the Goblet of Fire", "J.K. Rowling", "Fantasy", 12));
            _books.Add(new Book(5, "Harry Potter and the Order of the Phoenix", "J.K. Rowling", "Fantasy", 8));
            _books.Add(new Book(6, "Harry Potter and the Half-Blood Prince", "J.K. Rowling", "Fantasy", 5));
            _books.Add(new Book(7, "Harry Potter and the Deathly Hallows", "J.K. Rowling", "Fantasy", 7));
            _books.Add(new Book(8, "The Hunger Games", "Suzanne Collins", "Young Adult", 18));
            _books.Add(new Book(9, "Twilight", "Stephenie Meyer", "Young Adult", 14));
            _books.Add(new Book(10, "The Da Vinci Code", "Dan Brown", "Mystery", 22));
        }

        private static void PopulateUsers()
        {
            _users.Add(new User(1, "John Doe", "john@example.com"));
            _users.Add(new User(2, "Jane Smith", "jane@example.com"));
            _users.Add(new User(3, "Alice Johnson", "alice@example.com"));
            _users.Add(new User(4, "Bob Brown", "bob@example.com"));
            _users.Add(new User(5, "Emily Davis", "emily@example.com"));
            _users.Add(new User(6, "Michael Wilson", "michael@example.com"));
            _users.Add(new User(7, "Olivia Martinez", "olivia@example.com"));
            _users.Add(new User(8, "William Anderson", "william@example.com"));
            _users.Add(new User(9, "Sophia Taylor", "sophia@example.com"));
            _users.Add(new User(10, "Daniel Thomas", "daniel@example.com"));
        }

        private static void AssignBooksToUsers()
        {
            _usersBooks.Add(1, [_books[0], _books[1], _books[2], _books[3], _books[4]]);
            _usersBooks.Add(2, [_books[5], _books[6], _books[7], _books[8], _books[9]]);
            _usersBooks.Add(3, [_books[0], _books[2], _books[4], _books[6], _books[8]]);
            _usersBooks.Add(4, [_books[1], _books[3], _books[5], _books[7], _books[9]]);
            _usersBooks.Add(5, [_books[0], _books[1], _books[2], _books[5], _books[7]]);
        }

    }
}
