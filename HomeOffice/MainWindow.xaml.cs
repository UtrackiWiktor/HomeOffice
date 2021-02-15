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
                //MessageBox.Show(user.Surname);//to debbuging
                Warning.Content = "";
                UserPanel userPanel = new UserPanel(user);
                userPanel.Show();
                
            }
            else
            {
                Warning.Content = "No user with such PESEL in database";
            }
            //var users = user.AllUsersToList();
            //User result =users.Single(u=>u.PESEL==user.PESEL);
            //if(result!=null)
            //{
            //    Password password = new Password(user.ID);
            //    var passwords = password.AllPasswordsToList();
            //    Password res = passwords.Single(p => p.ID == password.ID);
            //    if(res.Password_==password.Password_)
            //    {
            //        UserPanel UserPanel = new UserPanel();
            //        UserPanel.Show();
            //    }
            //}

            //test normalnie wyszukaj w bazie
           // user= new Administrator("Paweł", "Tomaszewski", DateTime.Now, UserRoles.Administrator, 10, 89020119495);

        }
    }
}
