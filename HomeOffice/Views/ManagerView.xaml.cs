using System.Windows;
using System.Windows.Controls;
using HomeOffice.classes.Users;
using HomeOffice.classes.Tasks;
using System.Collections.Generic;
using HomeOffice.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace HomeOffice.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ManagerView.xaml
    /// </summary>
    public partial class ManagerView : UserControl
    {
        Manager manager;
        Task t = new Task();
        TaskDictionary task = new TaskDictionary();
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;
        public void SetUser(User u)
        {
            manager = new Manager(u);
        }

        public ManagerView()
        {
            manager = new Manager(((MainWindow)Application.Current.MainWindow).GetUser());
            InitializeComponent();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            //first check if checked all is checked
            using (var DbContext = new HomeOfficeContext())
            {
                var query = (from tasks in DbContext.Tasks
                             join user in DbContext.Users on tasks.Users_ID equals user.ID
                             join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                             where manager.Unit == user.Unit
                             select new { TaskID = tasks.Task_ID, UsersID = tasks.Users_ID, Name = user.Name, Surname = user.Surname, TaskDictionaryID = taskdictionary.ID, TaskName = taskdictionary.TaskName, TaskDescription = taskdictionary.TaskDescription, Status = tasks.Status }).ToList();

                tasksGrid.ItemsSource = query;
            }
        }

        //tutaj
        private void reportButton_Click(object sender, RoutedEventArgs e)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var query = DbContext.TaskDictionary.Where(t => t.Unit == manager.Unit).ToList();
                System.Windows.Forms.MessageBox.Show(manager.PrintTheReport(query));
            }

        }

        private void tasksGrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var query = (from tasks in DbContext.Tasks
                             join user in DbContext.Users on tasks.Users_ID equals user.ID
                             join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                             where manager.Unit == user.Unit
                             select new { TaskID = tasks.Task_ID, UsersID = tasks.Users_ID, Name = user.Name, Surname = user.Surname, TaskDictionaryID = taskdictionary.ID, TaskName = taskdictionary.TaskName, TaskDescription = taskdictionary.TaskDescription, Status = tasks.Status }).ToList();

                tasksGrid.ItemsSource = query;
            }
        }

        private void empGrid_Loaded(object sender, RoutedEventArgs e)
        {
            empGrid.ItemsSource = manager.UsersFromUnitToList(manager);
        }

        private void taskDicGrid_Loaded(object sender, RoutedEventArgs e)
        {
            taskDicGrid.ItemsSource = manager.UnitTasksToList(manager);
        }

        private void filterButton2nd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void refreshButton2nd_Click(object sender, RoutedEventArgs e)
        {
            empGrid.ItemsSource = manager.UsersFromUnitToList(manager);
            taskDicGrid.ItemsSource = manager.UnitTasksToList(manager);
        }

        private void assignButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selectedEmp in empGrid.SelectedItems)
            {
                if (selectedEmp is User)
                {
                    foreach (var selectedTask in taskDicGrid.SelectedItems)
                    {
                        if (selectedTask is TaskDictionary)
                        {
                            manager.AssignActivity((TaskDictionary)selectedTask, (User)selectedEmp);
                        }
                    }
                }
            }
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteAssigned_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selected in tasksGrid.SelectedItems)
            {
                string toDelete = selected.ToString();
                string[] split = toDelete.Split(new char[] { ',' });
                string taskID = Regex.Match(split[0], @"\d+").Value;
                int task_ID = Convert.ToInt32(taskID);
                Task todelete = new Task(task_ID);
                manager.UnassignActivity(todelete);
            }
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            manager.logOut();
        }

        private void deleteTask_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selected in taskDicGrid.SelectedItems)
            {
                if (selected is TaskDictionary)
                {
                    manager.DeleteFromTaskDictionary((TaskDictionary)selected);
                }
            }
        }

        private void Finished_Checked(object sender, RoutedEventArgs e)
        {
            if (finished.IsChecked == true)
            {
                tasksGrid.Focus();
                using (var DbContext = new HomeOfficeContext())
                {
                    var query = (from tasks in DbContext.Tasks
                                 join user in DbContext.Users on tasks.Users_ID equals user.ID
                                 join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                 where manager.Unit == user.Unit && tasks.Status == true
                                 select new { TaskID = tasks.Task_ID, UsersID = tasks.Users_ID, Name = user.Name, Surname = user.Surname, TaskDictionaryID = taskdictionary.ID, TaskName = taskdictionary.TaskName, TaskDescription = taskdictionary.TaskDescription, Status = tasks.Status }).ToList();

                    tasksGrid.ItemsSource = query;
                }
            }
        }

        private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if(selectAll1st.IsChecked == true)
            {
                tasksGrid.Focus();
                tasksGrid.SelectAll();
            }
        }

        private void SelectAllUsers_Checked(object sender, RoutedEventArgs e)
        {
            if(selectAllUsers.IsChecked == true)
            {
                empGrid.Focus();
                empGrid.SelectAll();
            }
        }

        private void SelectAllTasks_Checked(object sender, RoutedEventArgs e)
        {
            if(selectAllTasks.IsChecked == true)
            {
                taskDicGrid.Focus();
                taskDicGrid.SelectAll();
            }
        }
    }
}
