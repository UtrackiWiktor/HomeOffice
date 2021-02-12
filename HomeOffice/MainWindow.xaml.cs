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
using HomeOffice.classes.Passwords;
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
            

            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            //user.PESEL = Convert.ToInt64(PeselBox.Text);
            //var users = user.AllUsersToList();
            //User result =users.Single(u=>u.PESEL==user.PESEL);
            //if(result!=null)
            //{
            //    Password password = new Password(user.ID);
            //    var passwords = password.AllPasswordsToList();
            //    Password res = passwords.Single(p => p.ID == password.ID);
            //    if(res.Password_==password.Password_)
            //    {
            //        AdminPanel AdminPanel = new AdminPanel();
            //        AdminPanel.Show();
            //    }
            //}
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
        }
    }
}
