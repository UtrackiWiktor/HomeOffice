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
using HomeOffice.Views;

namespace HomeOffice
{
    public partial class MainWindow : Window
    {
       private User user = new User();
        public void SetUser(User u)
        {
            user = u;
        }
        public User GetUser()
        {
            return user;
        }
        public MainWindow()
        {
            

            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                user = new User(Convert.ToInt64(PeselBox.Text));
            }
            catch
            {
               Warning.Content="The PESEL value is incorrect";
            }
            if (user != null && user.ID >= 1 )
            {
                try
                { 
                    Password password = new Password();
                    password.FromDatabase(user.ID);
                    if (password.CompareWithPassword(PasswordBox.Text)&&password.ID>=1)
                    {
                        Warning.Content = "";
                        UserPanel userPanel = new UserPanel(user);
                        userPanel.Show();
                        PeselBox.Text = "";
                        PasswordBox.Text = "";
                        this.Hide();
                    }
                    else
                        Warning.Content = "Wrong password";
                }
                catch (Exception exception)
                {
                    MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                }
            }
            else
            {
                Warning.Content = "No user with such PESEL in database";
            }
        }
    }
}
