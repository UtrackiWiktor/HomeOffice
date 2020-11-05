using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        override public void addUser(string name,string surname, DateTime date, TypeOfUser user) 
        {
            //send data to database
            string server = "homeoffice.c0pmexmy2ypg.eu-central-1.rds.amazonaws.com";
            string database = "mydb";
            string uid = "admin";
            string password = "hom3off1ce";
            string connectionString;
            connectionString = "SERVER=" + server + "; PORT = 3306 ;" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            var mycon = new MySqlConnection(connectionString);

            mycon.Open();
            MySqlCommand a = new MySqlCommand("INSERT INTO Units(UnitName) VALUES(\"TestowaGrupa5555\")", mycon);
            a.ExecuteNonQuery();

        }
    }
}
