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
        public void SetUser(User u)
        {
            admin = new Administrator(u);
        }
        public AdminView()
        {
            admin = new Administrator(((MainWindow)Application.Current.MainWindow).GetUser());
            InitializeComponent();
           
        }
        public void RefreshUserList()
        {
            UserGrid.ItemsSource = admin.AllUsersToList();
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
            UserGrid.ItemsSource = admin.AllUsersToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this user?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                admin.DeleteUser(((User)UserGrid.SelectedItem));
            RefreshUserList();
        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
