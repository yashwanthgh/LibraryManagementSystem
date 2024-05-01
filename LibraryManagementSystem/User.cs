using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    // User: Represents a library user with properties like name, ID, etc.
    public interface IUser
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
    }

    public class User(int id, string name, string email) : IUser
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
    }
}
