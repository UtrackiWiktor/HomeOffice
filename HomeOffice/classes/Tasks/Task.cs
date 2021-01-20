using HomeOffice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Tasks
{
    class Task
    {
        public int ID { get; set; }
        public int UsersID { get; set; }
        public int TaskDictionaryID { get; set; }
        public bool Status { get; set; }

        public void AddTask(int UID, int TDID)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                Task task = new Task
                {
                    UsersID = UID,
                    TaskDictionaryID = TDID,
                    Status = false

                };
                DbContext.Tasks.Add(task);
                DbContext.SaveChanges();
            }
        }

        public List<Task> TasksToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var tasks = DbContext.Tasks.ToList();

                return tasks;
            }
        }
        public void DeleteTask(Task task)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(task);
                DbContext.SaveChanges();
            }
        }
    }
}
