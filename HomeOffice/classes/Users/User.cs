using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Users
{
    class User
    {
        uint id { get; set; }
        string name { get; set; }
        string surname { get; set; }
        DateTime birthDate { get; set; }

        virtual public void addUser(string name, string surname, DateTime date, TypeOfUser user){}
    }
}
