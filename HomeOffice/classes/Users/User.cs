using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual void addUser(string name, string surname, DateTime date, TypeOfUser typeOfUser, int unit) { }
        public virtual string usersToString()
        {
            return null;
        }
    }
}
