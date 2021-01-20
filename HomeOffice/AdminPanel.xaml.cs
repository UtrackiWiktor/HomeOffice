using HomeOffice.classes.Users;
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
using System.Windows.Shapes;
using HomeOffice.classes.Users;
using System.Globalization;

namespace HomeOffice
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        User user = new Administrator();
        public AdminPanel()
        {
           
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime=new DateTime();
            try
            {
                dateTime = DateTime.ParseExact(UserDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            catch(Exception exception)
            {
                MessageBox.Show("You probably provided incorrect data. Please correct it");
            }
            int unit;
            if (!String.IsNullOrWhiteSpace(UserName.Text) && !String.IsNullOrWhiteSpace(UserSurname.Text) && !String.IsNullOrWhiteSpace(UserDate.Text)&& int.TryParse(UserUnit.Text, out unit))
            {
                UserRoles typeOfUser;
                var index = SelectedTypeOfUser.SelectedIndex;
                if (index == 0)//employee
                    typeOfUser = UserRoles.Employee;
                else if (index == 1)//employee
                    typeOfUser = UserRoles.Manager;
                else if (index == 2)//employee
                    typeOfUser = UserRoles.Administrator;
                else
                    typeOfUser = UserRoles.Error;

                user.AddUser(UserName.Text, UserSurname.Text, dateTime, typeOfUser,unit);
                MessageBox.Show("User was added successfully");
                UserGrid.ItemsSource = user.UsersToList();
            }
            else
            {
                MessageBox.Show("You probably provided incorrect data. Please correct it");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UserGrid.ItemsSource = user.UsersToList();
        }

        private void UserGrid_Loaded(object sender, RoutedEventArgs e)
        {
            UserGrid.ItemsSource = user.UsersToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            user.DeleteUser(((User)UserGrid.SelectedItem));
            UserGrid.ItemsSource = user.UsersToList();
        }
    }
}
