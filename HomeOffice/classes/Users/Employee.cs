using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Users
{
    public class Employee: User
    {
        public Employee(string name, string surname, DateTime date, UserRoles typeOfUser, int unit, long Pesel) : base(name, surname, date, typeOfUser, unit, Pesel) { }
    }
}
