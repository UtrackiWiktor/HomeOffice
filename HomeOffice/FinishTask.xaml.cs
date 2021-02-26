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


namespace HomeOffice
{
    /// <summary>
    /// Logika interakcji dla klasy FinishTask.xaml
    /// </summary>
    /// 
    /// 
    public partial class FinishTaskWindow : Window
    {
        User user;
        Task t = new Task();

        public void SetUser(User u)
        {
            user = new Employee(u);
        }

        public FinishTaskWindow(User u)
        {
            SetUser(u);
            InitializeComponent();
        }


        private void windowList_Loaded(object sender, RoutedEventArgs e)
        {
            List<Task> x = t.AllTasksToList();
            foreach (Task task in x.ToList())
            {
                if (task.Users_ID != user.ID)
                {
                    x.Remove(task);
                }
            }
            windowList.ItemsSource = x;
        }


        void LoadTasks(object sender, RoutedEventArgs e)
        {
            List<Task> x = t.AllTasksToList();
            foreach (Task task in x)
            {
                if (task.Users_ID != user.ID)
                {
                    x.Remove(task);
                }
            }
            windowList.ItemsSource = x;

        }

        private void DoneUndone_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selected in windowList.SelectedItems)
            {
                if (selected is Task)
                {
                    System.Windows.Forms.MessageBox.Show(user.FinishMyActivity((Task)selected));
                }
            }
            refresh();

        }

        private void ShowMyActivity(object sender, RoutedEventArgs e)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                foreach (var selected in windowList.SelectedItems)
                {
                   System.Windows.Forms.MessageBox.Show(user.Show((Task)selected));

                }


            }
        }

        private void refresh()
        {
            List<Task> x = t.AllTasksToList();
            foreach (Task task in x.ToList())
            {
                if (task.Users_ID != user.ID)
                {
                    x.Remove(task);
                }
            }
            windowList.ItemsSource = x;
        }

    }
}
