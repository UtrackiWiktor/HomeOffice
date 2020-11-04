using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Users
{
    class Administrator: User
    {
        override public void addUser(string name,string surname, DateTime date, TypeOfUser user) 
        {
            //send data to database
        }
    }
}
