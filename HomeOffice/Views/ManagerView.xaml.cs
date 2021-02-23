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
                             select new { TaskID = tasks.Task_ID, User_ID=tasks.Users_ID, Name = user.Name, Surname = user.Surname, TaskTitle = taskdictionary.TaskName, Description = taskdictionary.TaskDescription, Completed = tasks.Status }).ToList();

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

        }

        private void assignButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteAssigned_Click(object sender, RoutedEventArgs e)
        {

        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
