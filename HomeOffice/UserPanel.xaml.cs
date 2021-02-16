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
using HomeOffice.classes.Passwords;
using HomeOffice.Views;
using System.Globalization;
using HomeOffice.ViewModels;

namespace HomeOffice
{
    public partial class UserPanel : Window
    {
        User user;
        public UserPanel()
        {
            user = null;
            InitializeComponent();
        }

        public UserPanel(User u)
        {
            InitializeComponent();
            user = u;
            if ((UserRoles)user.UserGroup == UserRoles.Administrator)
            {
                var viewModel = new AdminViewModel();
                this.DataContext = viewModel;
            }
            else if ((UserRoles)user.UserGroup == UserRoles.Employee)
            {
                var viewModel = new EmployeeViewModel();
                this.DataContext = viewModel;
            }
            
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime=new DateTime();
            try
            {
                dateTime = DateTime.ParseExact(UserDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
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
                    User newUser = new User(UserName.Text, UserSurname.Text, dateTime, typeOfUser, unit, Convert.ToInt64(UserPesel.Text));

                    user.AddUser(newUser);
                    Password password = new Password(newUser.ID);
                    user.AddPassword(password);
                    MessageBox.Show("User was added successfully.\n His password is: \""+password.GetPassword()+"\". \nPlease note it otherwise this data will be lost.");
                    password = null;//wipe data
                    UserGrid.ItemsSource = user.AllUsersToList();
                    //test
                    //((MainWindow)Application.Current.MainWindow).SetUser(new User("Ula", "Stańczyk", dateTime, UserRoles.Employee, 10, 100));
                }
                else
                {
                    MessageBox.Show("You probably provided incorrect data. Please correct it");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("You probably provided incorrect data. Please correct it");
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UserGrid.ItemsSource = user.AllUsersToList();
        }

        private void UserGrid_Loaded(object sender, RoutedEventArgs e)
        {
            UserGrid.ItemsSource = user.AllUsersToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            user.DeleteUser(((User)UserGrid.SelectedItem));
            UserGrid.ItemsSource = user.AllUsersToList();
        }
    }
}
