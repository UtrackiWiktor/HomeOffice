using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using MySql.Data.MySqlClient;

namespace HomeOffice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            //User user;
            //string Data = "admin";
            //if (Data == "admin")
            //    user = new Administrator();
            //else if (Data == "employee")
            //    user = new Employee();
            //else
            //    user = new Manager();
            //user.addUser();
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
        }
    }
}
