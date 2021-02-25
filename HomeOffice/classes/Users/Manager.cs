﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HomeOffice.classes.Tasks;
using HomeOffice.Data;

namespace HomeOffice.classes.Users
{
    public class Manager: User
    {
        internal static object manager;

        public Manager(string name, string surname, DateTime date, UserRoles typeOfUser, int unit,long pesel) : base(name, surname, date, typeOfUser, unit, pesel) { }
        public Manager(User u) : base(u) { }

        //Manager prints the report of all activities that 
        public String PrintTheReport<T>(List<T> list)
        {
            //search for a user's desktop and create folder there
            string path = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);
            path += "/Report.csv";
            //var file = File.CreateText(path);

            StreamWriter writer = new StreamWriter(path);

            writer.WriteLine(CreateCSVTextFile(list));
            writer.Flush();


            //save task list content to the list
            // file.Write(CreateCSVTextFile(list));

            return "Report saved as CSV file on a Desktop";
        }

        private string CreateCSVTextFile<T>(List<T> data)
        {
            var properties = typeof(T).GetProperties();
            var result = new StringBuilder();
            result.Insert(0, "TaskID,UserID,Name,Surname,TaskTitle,Description,Completed,");
            result.AppendLine();
            foreach (var row in data)
            {
                var values = properties.Select(p => p.GetValue(row, null))
                                       .Select(v => StringToCSVCell(Convert.ToString(v)));
                var line = string.Join(",", values);
                result.AppendLine(line);
            }


            return result.ToString();
        }

        private string StringToCSVCell(string str)
        {
            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }



            return str;
        }

        public override void AssignActivity(TaskDictionary task, User u)
        {
            Tasks.Task t = new Tasks.Task(u.ID, task.ID);
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Database.EnsureCreated();
                DbContext.Tasks.Add(t);
                DbContext.SaveChanges();
            }
        }

        public override void UnassignActivity(Tasks.Task t)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                DbContext.Remove(t);
                DbContext.SaveChanges();
            }
        }

        public override List<TaskDictionary> TaskDictionaryList()
        {
            return (base.TaskDictionaryList().Where(u => u.Unit == Unit)).ToList();
        }

        public override List<TaskDictionary> UnitTasksToList(User u)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var query = DbContext.TaskDictionary.Where(t => t.Unit == u.Unit).ToList();
                return query;
            }
        }

        public override List<User> UsersFromUnitToList(User u)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var query = DbContext.Users.Where(us => us.Unit == u.Unit).ToList();
                return query;
            }
        }

        public override void DeleteFromTaskDictionary(TaskDictionary taskDictionary)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                if (taskDictionary.IsEnabled == true)
                {
                    TaskDictionary toUpdate = DbContext.TaskDictionary.Where(t => t.ID == taskDictionary.ID).FirstOrDefault();
                    toUpdate.IsEnabled = false;
                    DbContext.SaveChanges();
                }
                else
                {
                    TaskDictionary toUpdate = DbContext.TaskDictionary.Where(t => t.ID == taskDictionary.ID).FirstOrDefault();
                    toUpdate.IsEnabled = true;
                    DbContext.SaveChanges();
                }
            }
        }
        public override void AddToTaskDictionary(TaskDictionary taskDictionary)
        {
            taskDictionary.AddTaskDictionary();
        }
    }
}
