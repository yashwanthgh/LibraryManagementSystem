using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class LibraryManager
    {
        private static readonly Library? library = new();

        public static void PromptUser()
        {
            Console.WriteLine("1. To Borrow Book.");
            Console.WriteLine("2. To Return Book.");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("1. Existing User!");
                        Console.WriteLine("2. New User!");
                        int userChoice = Convert.ToInt32(Console.ReadLine());
                        switch (userChoice)
                        {
                            case 1:
                                Console.WriteLine("Enter ID: ");
                                int id = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter ISBN: ");
                                int isbn = Convert.ToInt32(Console.ReadLine());
                                library.BorrowBook(id, isbn);
                                break;
                            case 2:
                                library.BorrowBook();
                                break;
                        }
                    }
                    break;
                case 2:
                    {
                        Console.WriteLine("Enter the ID of User: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the Books ISBN: ");
                        int isbn = Convert.ToInt32(Console.ReadLine());
                        library.ReturnBook(id, isbn);
                    }
                    break;

            }
        }
    }
}
