using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeOffice.classes.Tasks;
using HomeOffice.Data;

namespace HomeOffice.classes.Users
{
    public class Employee: User
    {
        public Employee(string name, string surname, DateTime date, UserRoles typeOfUser, int unit, long Pesel) : base(name, surname, date, typeOfUser, unit, Pesel) { }
        public Employee(User u) : base(u) { }


        //List filled with tasks that user was or still is assigned to
        public List<Task> usersTasks = new List<Task>();


        //Simply change current state of task that employee has finished already to true
        public override String FinishMyActivity(Task task)
        {
            string quote = "";
            if(task.Status == true){
                task.Status = false;
            }
            else{
                task.Status = true;
            }
            
            Random rnd = new Random();
            int rand = rnd.Next(0, 11);      
    
            using (var DbContext = new HomeOfficeContext())
            {
                var result = DbContext.Tasks.SingleOrDefault(t => t.Task_ID == task.Task_ID);
                if (result != null)
                {
                    DbContext.Entry(result).CurrentValues.SetValues(task);
                    DbContext.SaveChanges();
                    
                }
                
            }

            switch (rand)
            {
                case 0:
                    quote = "Work hard and be patient. The rest will follow.";
                    break;
                case 1:
                    quote = "Working is the future and the future starts with you";
                    break;
                case 2:
                    quote = "No pain, no gain.";
                    break;
                case 3:
                    quote = "You were hired because you met expectations,\nyou will be promoted if you can exceed them.";
                    break;
                case 4:
                    quote = "The only thing that overcomes hard luck is hard work.";
                    break;
                case 5:
                    quote = "Expect nothing and you will never be disappointed";
                    break;
                case 6:
                    quote = "You weren't hired to just hang around\nBuckle up and go back to work!";
                    break;
                case 7:
                    quote = "Nobody, ever, drowned in sweat!";
                    break;
                case 8:
                    quote = "You have gone so far, don't quit now! (for real we're short on staff)";
                    break;
                case 9:
                    quote = "No man goes before his time - unless the boss leaves early";
                    break;
                case 10:
                    quote = "The best way to appreciate your job is to imagine yourself without one";
                    break;
            }
            return "The task has been finished successfully!\nGreat job!\n\nRemember:\n" + quote;
        }


        public String ShowMyActivity(TaskDictionary taskdic)
        {
            return taskdic.TaskName + "\n" + "\n" + taskdic.TaskDescription;
        }
    }
}
