using HomeOffice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Tasks
{
    public class Task
    {
        public int Task_ID { get; set; }
        public int Users_ID { get; set; }
        public int TaskDictionary_ID { get; set; }
        public bool Status { get; set; }
        public Task() { }//used by entity framework, otherwise it throws exp
        public Task(int UID,int TDID)
        {
            Users_ID = UID;
            TaskDictionary_ID = TDID;
            Status = false;
        }
        public void AddTask()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();

                DbContext.Tasks.Add(this);
                DbContext.SaveChanges();
            }
        }

        public List<Task> AllTasksToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var tasks = DbContext.Tasks.ToList();

                return tasks;
            }
        }
        public void DeleteTask()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(this);
                DbContext.SaveChanges();
            }
        }
    }
}
