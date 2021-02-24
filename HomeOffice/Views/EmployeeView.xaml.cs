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
        //Employee emp;
        public void SetUser(User u)
        {
            //emp = new Employee(u);
        }

        public EmployeeView()
        {
            user = new Employee(((MainWindow)Application.Current.MainWindow).GetUser());
            InitializeComponent();
        }

        void LoadTasks(object sender, RoutedEventArgs e)
        {
            using (var DbContext = new HomeOfficeContext())
            {
                tasksList.ItemsSource = t.AllTasksToList();
            }
            
        }

        private void DoneUndone_Click(object sender, RoutedEventArgs e)
        {
              foreach (var selected in tasksList.SelectedItems)
                {
                    if (selected is Task)
                    {
                      System.Windows.Forms.MessageBox.Show(user.FinishMyActivity((Task)selected));
                    }
                }
        }

        private void refreshTasks_Click(object sender, RoutedEventArgs e)
        {

        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            user.logOut();
        }

        private void ShowMyActivity(object sender, RoutedEventArgs e){

        }

    }
}
