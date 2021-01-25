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
namespace HomeOffice.classes.Users
{
    class Administrator: User
    {
        public Administrator(string name, string surname, DateTime date, UserRoles typeOfUser, int unit,long Pesel) : base(name, surname, date, typeOfUser, unit, Pesel) { }

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
        public override void DeleteUser(User user)
        {
            DeletePasswordOfUser(user);
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(user);
                DbContext.SaveChanges();
            }
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
        //override public void addUser(string name, string surname, DateTime date, TypeOfUser user,int unit)
        //{
        //    //send data to database
        //    string server = "homeoffice.c0pmexmy2ypg.eu-central-1.rds.amazonaws.com";
        //    string database = "mydb";
        //    string uid = "admin";
        //    string password = "hom3off1ce";
        //    string connectionString;
        //    connectionString = "SERVER=" + server + "; PORT = 3306 ;" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        //    var mycon = new MySqlConnection(connectionString);

        //    mycon.Open();
        //    MySqlCommand a = new MySqlCommand("INSERT INTO Units(UnitName) VALUES(\"TestowaGrupa5555\")", mycon);
        //    a.ExecuteNonQuery();

        //}
    }
}
