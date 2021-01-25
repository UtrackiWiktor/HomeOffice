using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Users
{
    class Manager: User
    {
        public Manager(string name, string surname, DateTime date, UserRoles typeOfUser, int unit,long pesel) : base(name, surname, date, typeOfUser, unit, pesel) { }
    }
}
