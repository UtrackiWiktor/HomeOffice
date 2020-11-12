using HomeOffice.Data;
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

        public override void AddUser(string name, string surname, DateTime date, TypeOfUser typeOfUser, int unit)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                User user = new User
                {
                    Name = name,
                    Surname = surname,
                    DateOfBirth = date,
                    UserGroup = (int)typeOfUser,
                    Unit = unit
                };
                DbContext.Users.Add(user);
                DbContext.SaveChanges();
            }
        }

        public override List<User> UsersToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var users = DbContext.Users.ToList();

                return users;
            }
        }
        public override void DeleteUser(User user)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(user);
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
