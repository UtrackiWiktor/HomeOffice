using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeOffice.classes.Passwords;

namespace HomeOffice.classes.Users
{
    class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Unit { get; set; }
        public int UserGroup { get; set; }
        public long PESEL { get; set; }
        public User() { }//used by entity framework, otherwise it throws exp
        public User(string name, string surname, DateTime date, UserRoles typeOfUser, int unit, long pesel)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = date;
            UserGroup = (int)typeOfUser;
            Unit = unit;
            PESEL = pesel;
        }
        public virtual void AddPassword(Password password) { }
        public virtual void AddUser(User user) { }
        public virtual List<User> AllUsersToList()
        {
            return null;
        }

        public virtual void DeleteUser(User user)
        { }
    }
}
