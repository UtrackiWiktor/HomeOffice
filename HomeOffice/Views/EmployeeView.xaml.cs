using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HomeOffice.classes.Users;
using System.Windows;
using System.Windows.Controls;
using HomeOffice.classes.Users;
using HomeOffice.classes.Tasks;
using System.Collections.Generic;
using HomeOffice.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;


namespace HomeOffice.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        User user;
        Task t = new Task();
        TaskDictionary task = new TaskDictionary();

        public EmployeeView()
        {
            user = new Employee(((MainWindow)Application.Current.MainWindow).GetUser());
            InitializeComponent();
        }

        void LoadTasks(object sender, RoutedEventArgs e)
        {
            try 
            { 
                using (var DbContext = new HomeOfficeContext())
                {
                    var query = (from tasks in DbContext.Tasks
                                 join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                 join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                 where tasks.Users_ID == user.ID
                                 select new { TaskID = tasks.Task_ID,TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                    tasksList.ItemsSource = query;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }

        }

        private void DoneUndone_Click(object sender, RoutedEventArgs e)
        {
            FinishTaskWindow finishtaskInstance = new FinishTaskWindow(user);
            finishtaskInstance.ShowDialog();
        }

        private void refresh()
        {
            try
            { 
                using (var DbContext = new HomeOfficeContext())
                {
                    var query = (from tasks in DbContext.Tasks
                                 join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                 join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                 where tasks.Users_ID == user.ID
                                 select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                    tasksList.ItemsSource = query;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }

        }

        private void click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            user.logOut();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try 
            { 
                if (Unfinished.IsChecked == true)
                {
                    using (var DbContext = new HomeOfficeContext())
                    {
                        var query = (from tasks in DbContext.Tasks
                                     join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                     join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                     where tasks.Users_ID == user.ID && tasks.Status != true && tasks.Status != false
                                     select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                        tasksList.ItemsSource = query;
                    }
                }
                else
                {
                    using (var DbContext = new HomeOfficeContext())
                    {
                        var query = (from tasks in DbContext.Tasks
                                     join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                     join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                     where tasks.Users_ID == user.ID && tasks.Status != false
                                     select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                        tasksList.ItemsSource = query;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            try 
            { 
                if (Finished.IsChecked == true)
                {
                    using (var DbContext = new HomeOfficeContext())
                    {
                        var query = (from tasks in DbContext.Tasks
                                     join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                     join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                     where tasks.Users_ID == user.ID && tasks.Status != true && tasks.Status != false
                                     select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                        tasksList.ItemsSource = query;
                    }
                }
                else
                {
                    using (var DbContext = new HomeOfficeContext())
                    {
                        var query = (from tasks in DbContext.Tasks
                                     join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                     join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                     where tasks.Users_ID == user.ID && tasks.Status != true
                                     select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                        tasksList.ItemsSource = query;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            { 
                if (Finished.IsChecked == true)
                {
                    using (var DbContext = new HomeOfficeContext())
                    {
                        var query = (from tasks in DbContext.Tasks
                                     join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                     join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                     where tasks.Users_ID == user.ID && tasks.Status != false
                                     select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                        tasksList.ItemsSource = query;
                    }
                }
                else
                {
                    using (var DbContext = new HomeOfficeContext())
                    {
                        var query = (from tasks in DbContext.Tasks
                                     join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                     join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                     where tasks.Users_ID == user.ID
                                     select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                        tasksList.ItemsSource = query;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            try
            { 
                if (Unfinished.IsChecked == true)
                {
                    using (var DbContext = new HomeOfficeContext())
                    {
                        var query = (from tasks in DbContext.Tasks
                                     join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                     join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                     where tasks.Users_ID == user.ID && tasks.Status != true
                                     select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                        tasksList.ItemsSource = query;
                    }
                }
                else
                {
                    using (var DbContext = new HomeOfficeContext())
                    {
                        var query = (from tasks in DbContext.Tasks
                                     join user1 in DbContext.Users on tasks.Users_ID equals user1.ID
                                     join taskdictionary in DbContext.TaskDictionary on tasks.TaskDictionary_ID equals taskdictionary.ID
                                     where tasks.Users_ID == user.ID
                                     select new { TaskID = tasks.Task_ID, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

                        tasksList.ItemsSource = query;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
        }
    }
}
