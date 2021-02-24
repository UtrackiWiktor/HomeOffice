using HomeOffice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeOffice.classes.Tasks
{
    public class TaskDictionary
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int Unit { get; set; }
        public bool IsEnabled { get; set; }
        public TaskDictionary() { }//used by entity framework, otherwise it throws exp
        public TaskDictionary(string tskName, string tskDesctiption, int unit,bool enabled=true)
        {
             TaskName = tskName;
             TaskDescription = tskDesctiption;
             Unit = unit;
             IsEnabled = enabled;
        }
        public void AddTaskDictionary()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                DbContext.TaskDictionary.Add(this);
                DbContext.SaveChanges();
            }
        }

        public List<TaskDictionary> AllTaskDictionaryToList()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var tasksDictionary = DbContext.TaskDictionary.ToList();

                return tasksDictionary;
            }
        }
        public void DeleteTaskDictionary()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(this);
                DbContext.SaveChanges();
            }
        }
        public void UpdateTaskDictionary()
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var result = DbContext.TaskDictionary.SingleOrDefault(t => t.ID == ID);
                if (result != null)
                {
                    DbContext.Entry(result).CurrentValues.SetValues(this);
                    DbContext.SaveChanges();
                }

            }
        }
    }
}
