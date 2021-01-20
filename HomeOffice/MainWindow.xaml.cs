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
using MySql.Data.MySqlClient;
using HomeOffice.Data;
using System.Xaml;
using Microsoft.EntityFrameworkCore;
using HomeOffice.classes.Tasks;
using HomeOffice.classes.Units;

namespace HomeOffice
{
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
