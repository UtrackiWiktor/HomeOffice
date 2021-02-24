using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeOffice.classes.Passwords;
using HomeOffice.Data;
using HomeOffice.classes.Tasks;
using HomeOffice.classes.Units;
using System.Windows;

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
        public User(User u)
        {
            ID = u.ID;
            Name = u.Name;
            Surname = u.Surname;
            DateOfBirth = u.DateOfBirth;
            UserGroup = u.UserGroup;
            Unit = u.Unit;
            PESEL = u.PESEL;
        }

        public void logOut()
        {
            App.Current.Windows[0].Show();
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 1; intCounter--)
            {
                App.Current.Windows[intCounter].Close();
            }
                
        }

        public virtual void AddPassword(Password password) { }
        public virtual void AddUser(User user) { }
        public virtual List<User> AllUsersToList()
        {return null;}
        public virtual List<TaskDictionary> TaskDictionaryList()
        { return (new TaskDictionary()).AllTaskDictionaryToList(); }
        public virtual void DeleteUser(User user) { }
        public virtual void UpdateUser(User user) { }
        public virtual void AssignActivity(TaskDictionary task, User u) { }
        public virtual void UnassignActivity(Tasks.Task t) { }
        public virtual void AddToTaskDictionary(TaskDictionary taskDictionary) { }
        public virtual List<TaskDictionary> UnitTasksToList(User u) { return null; }
        public virtual List<User> UsersFromUnitToList(User u) { return null; }
        public virtual void DeleteFromTaskDictionary(TaskDictionary taskDictionary) { }
        public virtual void AddUnit(String name) { }
        public virtual void DeleteUnit(int id) { }
        public virtual void UpdateUnit(int id) { }
        
        public virtual List<Unit> UnitList() { return null; }
        public virtual String FinishMyActivity(Tasks.Task task) { return null; }
        public virtual String Show(Tasks.Task task) { return null; }
    }

}
