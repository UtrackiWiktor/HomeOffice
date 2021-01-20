using HomeOffice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Tasks
{
    class TaskDictionary
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }

        public void AddTaskDictionary(string tskName, string tskDesctiption)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                TaskDictionary taskDictionary = new TaskDictionary
                {
                    TaskName = tskName,
                    TaskDescription = tskDesctiption

                };
                DbContext.TaskDictionary.Add(taskDictionary);
                DbContext.SaveChanges();
            }
        }

        public List<TaskDictionary> TaskDictionaryToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var tasksDictionary = DbContext.TaskDictionary.ToList();

                return tasksDictionary;
            }
        }
        public void DeleteTaskDictionary(TaskDictionary taskDictionary)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(taskDictionary);
                DbContext.SaveChanges();
            }
        }
    }
}
