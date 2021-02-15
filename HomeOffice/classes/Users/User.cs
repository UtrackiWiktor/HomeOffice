using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeOffice.classes.Passwords;
using HomeOffice.Data;

namespace HomeOffice.classes.Users
{
    public class User
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
        public User(long pesel)
        {
            
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                var query = DbContext.Users.Where(us => us.PESEL == pesel);
                User u = query.FirstOrDefault<User>();
                if (u != null)
                {
                    ID = u.ID;
                    Name = u.Name;
                    Surname = u.Surname;
                    DateOfBirth = u.DateOfBirth;
                    UserGroup = u.UserGroup;
                    Unit = u.Unit;
                    PESEL = u.PESEL;
                }
                else
                    ID = -1;
            }
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
