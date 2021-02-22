using HomeOffice.Data;
using HomeOffice.classes.Passwords;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HomeOffice.classes.Tasks;

namespace HomeOffice.classes.Users
{
    public class Administrator: User
    {
        public Administrator(string name, string surname, DateTime date, UserRoles typeOfUser, int unit,long Pesel) : base(name, surname, date, typeOfUser, unit, Pesel) { }
        public Administrator(User u) : base(u) { }
        public override void AddUser(User user)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                DbContext.Users.Add(user);
                DbContext.SaveChanges();
            }
        }
        public override void AddPassword(Password password)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                DbContext.Passwords.Add(password);
                DbContext.SaveChanges();
            }
        }
        public override List<User> AllUsersToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
            
                var users = DbContext.Users.ToList();

                return users;
            }
        }
        public override void UpdateUser(User user)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var result = DbContext.Users.SingleOrDefault(u => u.ID == user.ID);
                if (result != null)
                {
                    DbContext.Entry(result).CurrentValues.SetValues(user);
                    DbContext.SaveChanges();
                    
                }
                
            }
        }
        public override void DeleteUser(User user)
        {
            DeletePasswordOfUser(user);
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(user);
                DbContext.SaveChanges();
            }
        }
        public override void AddToTaskDictionary(TaskDictionary taskDictionary)
        {
            taskDictionary.AddTaskDictionary();
        }

        public override void DeleteFromTaskDictionary(TaskDictionary taskDictionary)
        {
            taskDictionary.DeleteTaskDictionary();
        }

        private void DeletePasswordOfUser(User user)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                Password password = new Password(user.ID);
                DbContext.Remove(password);
                DbContext.SaveChanges();
            }
        }
    }
}
