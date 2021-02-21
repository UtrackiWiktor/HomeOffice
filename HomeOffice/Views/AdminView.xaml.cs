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
using HomeOffice.classes.Passwords;
using System.Globalization;

namespace HomeOffice.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        User admin;
        public delegate void RefreshList();
        public event RefreshList RefreshListEvent;
        List<User> userList;
        public void SetUser(User u)
        {
            admin = new Administrator(u);
        }
        public AdminView()
        {
            admin = new Administrator(((MainWindow)Application.Current.MainWindow).GetUser());
            userList = admin.AllUsersToList();
            InitializeComponent();
            SelectedTypeOfUser.SelectedIndex = 3;
        }

        private void filterUserDataGrid()
        {
            List<User> temp = userList;
            if(UserName.Text != null)
            {
                //like %UserName.Text%
                temp = temp.Where(u => u.Name.ToUpper().Contains(UserName.Text.ToUpper())).ToList();
            }
            if (UserSurname.Text != null)
            {
                temp = temp.Where(u => u.Surname.ToUpper().Contains(UserSurname.Text.ToUpper())).ToList();
            }
            if(UserUnit.Text != null)
            {
                temp = temp.Where(u => u.Unit.ToString().Contains(UserUnit.Text)).ToList();
            }
            if(SelectedTypeOfUser.SelectedItem != null)
            {
                UserRoles userRole;
                var index = SelectedTypeOfUser.SelectedIndex;
                if (index != 3)
                {
                    if (index == 0)//employee
                        userRole = UserRoles.Employee;
                    else if (index == 1)//employee
                        userRole = UserRoles.Manager;
                    else if (index == 2)//employee
                        userRole = UserRoles.Administrator;
                    else
                        userRole = UserRoles.Error;

                    temp = temp.Where(u => u.UserGroup.ToString().Contains(((int)userRole).ToString())).ToList();
                }
            }
            UserDataGrid.ItemsSource = temp;
        }
        public void RefreshUserList()
        {
            userList = admin.AllUsersToList();
            filterUserDataGrid();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserInstance = new AddUserWindow(admin);
            RefreshListEvent += new RefreshList(RefreshUserList);
            addUserInstance.addUserDel = RefreshListEvent;
            addUserInstance.Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
        }

        private void UserGrid_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataGrid.ItemsSource = admin.AllUsersToList();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void User_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterUserDataGrid();
        }

        private void SelectedTypeOfUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterUserDataGrid();
        }

        private void SelectedTypeOfUser_Unselected(object sender, RoutedEventArgs e)
        {
            SelectedTypeOfUser.SelectedIndex= -1;
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                admin.DeleteUser(((User)UserDataGrid.SelectedItem));
            RefreshUserList();
        }
        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to reset password of this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Password password = new Password( ((User)UserDataGrid.SelectedItem).ID );
                var p = password.GetPassword();
                password.EncodePassword();
                password.ResetPassword();
              
                MessageBox.Show("User password was changed successfully.\n His password is: \"" + p + "\". \nPlease note it otherwise this data will be lost.");
                p = null;
            }
        }
    }
}
