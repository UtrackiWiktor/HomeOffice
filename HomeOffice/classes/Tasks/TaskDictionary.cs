﻿using HomeOffice.Data;
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
        public TaskDictionary() { }//used by entity framework, otherwise it throws exp
        public TaskDictionary(string tskName, string tskDesctiption, int unit)
        {
             TaskName = tskName;
             TaskDescription = tskDesctiption;
             Unit = unit;
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
        public void AddUser_Click() { 
        }
    }
}
