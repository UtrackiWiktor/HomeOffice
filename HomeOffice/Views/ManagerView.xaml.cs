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
        List<User> userList;
        Task t = new Task();
        TaskDictionary task = new TaskDictionary();
        List<TaskDictionary> tasksDictionaryList;
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;
        public void SetUser(User u)
        {
            manager = new Manager(u);
        }

        public ManagerView()
        {
            manager = new Manager(((MainWindow)Application.Current.MainWindow).GetUser());
            userList = manager.UsersFromUnitToList(manager);
            tasksDictionaryList = manager.UnitTasksToList(manager);
            InitializeComponent();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterTasks();
        }

        private void FilterTasks()
        {
            if(usersNames1st.Text != null)
            {
                using (var DbContext = new HomeOfficeContext())
                {
                    var query = (from tasks in DbContext.Tasks
                                 join user in DbContext.Users on tasks.Users_ID equals user.ID
                                 join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                 where manager.Unit == user.Unit && usersNames1st.Text == user.Name
                                 select new { TaskID = tasks.Task_ID, UsersID = tasks.Users_ID, Name = user.Name, Surname = user.Surname, TaskDictionaryID = taskdictionary.ID, TaskName = taskdictionary.TaskName, TaskDescription = taskdictionary.TaskDescription, Status = tasks.Status }).ToList();
                    if (query.Any())
                    {
                        tasksGrid.ItemsSource = query;
                    }
                }
            }
            if (usersSurnames1st.Text != null)
            {
                using (var DbContext = new HomeOfficeContext())
                {
                    var query = (from tasks in DbContext.Tasks
                                 join user in DbContext.Users on tasks.Users_ID equals user.ID
                                 join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                 where manager.Unit == user.Unit && usersSurnames1st.Text == user.Surname
                                 select new { TaskID = tasks.Task_ID, UsersID = tasks.Users_ID, Name = user.Name, Surname = user.Surname, TaskDictionaryID = taskdictionary.ID, TaskName = taskdictionary.TaskName, TaskDescription = taskdictionary.TaskDescription, Status = tasks.Status }).ToList();
                    if (query.Any())
                    {
                        tasksGrid.ItemsSource = query;
                    }
                }
            }
            if (tasksTypes1st.Text != null)
            {
                using (var DbContext = new HomeOfficeContext())
                {
                    var query = (from tasks in DbContext.Tasks
                                 join user in DbContext.Users on tasks.Users_ID equals user.ID
                                 join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                 where manager.Unit == user.Unit && tasksTypes1st.Text == taskdictionary.TaskName
                                 select new { TaskID = tasks.Task_ID, UsersID = tasks.Users_ID, Name = user.Name, Surname = user.Surname, TaskDictionaryID = taskdictionary.ID, TaskName = taskdictionary.TaskName, TaskDescription = taskdictionary.TaskDescription, Status = tasks.Status }).ToList();
                    if (query.Any())
                    {
                        tasksGrid.ItemsSource = query;
                    }
                }
            }
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
            FilterTasks();
        }

        //tutaj
        private void reportButton_Click(object sender, RoutedEventArgs e)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                var query = (from tasks in DbContext.Tasks
                             join user in DbContext.Users on tasks.Users_ID equals user.ID
                             join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                             where manager.Unit == user.Unit
                             select new { TaskID = tasks.Task_ID, UsersID = tasks.Users_ID, Name = user.Name, Surname = user.Surname, TaskName = taskdictionary.TaskName, TaskDescription = taskdictionary.TaskDescription, Status = tasks.Status }).ToList();
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
            FilterUserDataGrid();
            FilterTasksDataGrid();
        }
        private void refresh2()
        {
            userList = manager.UsersFromUnitToList(manager);
            tasksDictionaryList = manager.UnitTasksToList(manager);
            empGrid.ItemsSource = manager.UsersFromUnitToList(manager);
            taskDicGrid.ItemsSource = manager.UnitTasksToList(manager);
            FilterUserDataGrid();
            FilterTasksDataGrid();
        }
        private void refreshButton2nd_Click(object sender, RoutedEventArgs e)
        {
            refresh2();
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
                            if (((TaskDictionary)selectedTask).IsEnabled == true)
                            {
                                manager.AssignActivity((TaskDictionary)selectedTask, (User)selectedEmp);
                            }
                            else
                            {
                                MessageBox.Show("You can't assign task that is disabled");
                            }
                        }
                    }
                }
            }
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddToDictionary addToDictionryInstance = new AddToDictionary(manager);
            RefreshListEvent += new RefreshList(refresh2);
            addToDictionryInstance.AddTaskDel = RefreshListEvent;
            addToDictionryInstance.Show();
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

        private void enableDisableTask_Click(object sender, RoutedEventArgs e)
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

                    if (query.Any())
                    {
                        tasksGrid.ItemsSource = query;
                    }
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

        private void FilterUserDataGrid()
        {
            List<User> temp = userList;
            if (usersNames2nd.Text != null)
            {
                //like %UserName.Text%
                temp = temp.Where(u => u.Name.ToUpper().Contains(usersNames2nd.Text.ToUpper())).ToList();
            }
            if (usersSurnames2nd.Text != null)
            {
                temp = temp.Where(u => u.Surname.ToUpper().Contains(usersSurnames2nd.Text.ToUpper())).ToList();
            }
            empGrid.ItemsSource = temp;
            if (empGrid.Columns.Count > 1) // at init is 0
                empGrid.Columns[0].IsReadOnly = true;
        }

        private void FilterTasksDataGrid()
        {
            List<TaskDictionary> temp = tasksDictionaryList;
            if (tasksTypes2nd.Text != null)
            {
                //like %tasksTitle.Text%
                temp = temp.Where(t => t.TaskName.ToUpper().Contains(tasksTypes2nd.Text.ToUpper())).ToList();
            }
            taskDicGrid.ItemsSource = temp;
            if (taskDicGrid.Columns.Count > 1) // at init is 0
                taskDicGrid.Columns[0].IsReadOnly = true;
        }
    }
}
