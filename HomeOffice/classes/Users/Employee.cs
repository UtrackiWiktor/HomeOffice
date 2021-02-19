using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeOffice.classes.Tasks;

namespace HomeOffice.classes.Users
{
    public class Employee: User
    {
        public Employee(string name, string surname, DateTime date, UserRoles typeOfUser, int unit, long Pesel) : base(name, surname, date, typeOfUser, unit, Pesel) { }

        //List filled with tasks that user was or still is assigned to
        public List<Task> usersTasks = new List<Task>();


        //Simply change current state of task that employee has finished already to true
        public void FinishMyActivity(Task task)
        {
            task.Status = true;
            //W widoku employee powinno byc jeszcze odswiezenie tabeli
        }


        public String ShowMyActivity(TaskDictionary taskdic)
        {
            return taskdic.TaskName + "\n" + "\n" + taskdic.TaskDescription;

            //W widoku employee powinno jeszcze byc
            //System.Windows.Forms.MessageBox.Show(employee.ShowMyActivity);
        }
    }
}
