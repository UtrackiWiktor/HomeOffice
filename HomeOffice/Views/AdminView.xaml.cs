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
using HomeOffice.classes.Tasks;
using System.Globalization;
using HomeOffice.classes.Units;

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
        List<TaskDictionary> tasksDictionaryList;
        List<int> idList;
        List<Unit> unitList;

        public void SetUser(User u)
        {
            admin = new Administrator(u);
        }
        public AdminView()
        {
            admin = new Administrator(((MainWindow)Application.Current.MainWindow).GetUser());
            userList = admin.AllUsersToList();
            InitializeComponent();
        }

        private void FilterUserDataGrid()
        {
            List<User> temp = userList;
            if (UserName.Text != null)
            {
                //like %UserName.Text%
                temp = temp.Where(u => u.Name.ToUpper().Contains(UserName.Text.ToUpper())).ToList();
            }
            if (UserSurname.Text != null)
            {
                temp = temp.Where(u => u.Surname.ToUpper().Contains(UserSurname.Text.ToUpper())).ToList();
            }
            if (UserUnit.Text != null)
            {
                temp = temp.Where(u => u.Unit.ToString().Contains(UserUnit.Text)).ToList();
            }
            if (SelectedTypeOfUser.SelectedItem != null)
            {
                UserRoles userRole;
                var index = SelectedTypeOfUser.SelectedIndex;
                if (index != 3)
                {
                    if (index == 0)
                        userRole = UserRoles.Administrator;
                    else if (index == 1)
                        userRole = UserRoles.Manager;
                    else if (index == 2)
                        userRole = UserRoles.Employee;
                    else
                        userRole = UserRoles.Error;

                    temp = temp.Where(u => u.UserGroup.ToString().Contains(((int)userRole).ToString())).ToList();
                }
            }
            UserDataGrid.ItemsSource = temp;
            if(UserDataGrid.Columns.Count>1) // at init is 0
                UserDataGrid.Columns[0].IsReadOnly = true;



        }
        public void RefreshUserList()
        {
            userList = admin.AllUsersToList();
            WarningLabel.Content = "";
            idList = new List<int>();
            
            FilterUserDataGrid();
            UserDataGrid.Columns[0].IsReadOnly = true;
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
            UserDataGrid.Columns[0].IsReadOnly = true;
            idList = new List<int>();
            WarningLabel.Content = "";
            UserName.Text = "";
            UserSurname.Text = "";
            UserUnit.Text = "";
            SelectedTypeOfUser.SelectedIndex = 3;
            SelectAll.IsChecked = false;
        }
        private void UserDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            if (UserDataGrid.SelectedItem is User)
            {
                if (!idList.Contains(((User)UserDataGrid.SelectedItem).ID))
                {
                    idList.Add(((User)UserDataGrid.SelectedItem).ID);
                    if (idList.Count==1)
                    {
                        WarningLabel.Content = "Non committed changes for users with ids: ";
                    }
                    var s= ((User)(UserDataGrid.SelectedItem)).ID.ToString();
                    WarningLabel.Content+=s +", ";
                }
            }
        }

        private void User_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterUserDataGrid();

        }

        private void SelectedTypeOfUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterUserDataGrid();
        }

        private void SelectedTypeOfUser_Unselected(object sender, RoutedEventArgs e)
        {
            SelectedTypeOfUser.SelectedIndex = -1;
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItems[0] is User)
            {
                string str = "User";
                if (UserDataGrid.SelectedCells.Count > 1)
                    str += "s";
                if (MessageBox.Show("Do you want to delete selected " + str + "?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    foreach (var selected in UserDataGrid.SelectedItems)
                    {
                        if (selected is User)
                        admin.DeleteUser(((User)selected));
                    }

                    RefreshUserList();
            }
        }
        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItems[0] is User)
            {
                string str = "User";
                if (UserDataGrid.SelectedCells.Count > 1)
                    str += "s";
                if (MessageBox.Show("Do you want to reset password of selected " + str + "?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (var selected in UserDataGrid.SelectedItems)
                    {
                        if (selected is User)
                        {
                            Password password = new Password(((User)selected).ID);
                            var p = password.GetPassword();
                            password.EncodePassword();
                            password.ResetPassword();

                            MessageBox.Show("The password of User with ID="+((User)selected).ID + " was changed successfully.\n His password is: \"" + p + "\". \nPlease note it otherwise this data will be lost.");
                            p = null;
                        }
                    }
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string str = "User";
            if (UserDataGrid.SelectedCells.Count > 1)
                str += "s";
            if (MessageBox.Show("Do you want to update selected " + str + "?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var selected in UserDataGrid.SelectedItems)
                {
                    if (selected is User && idList.Contains(((User)selected).ID))
                    {
                        admin.UpdateUser(((User)selected));
                        idList.Remove(((User)selected).ID);
                    }

                }
                if(idList.Count==0)
                {
                        WarningLabel.Content = "";
                        idList = new List<int>();
                }
                else
                {
                        WarningLabel.Content = "Non committed changes for users with ids: ";
                        foreach(var id in idList)
                        {
                        WarningLabel.Content += id.ToString() + ", ";
                        }
                }
            }
           
        }
        private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if (SelectAll.IsChecked == true)
            {
                UserDataGrid.Focus();
                UserDataGrid.SelectAll();
            }
        }


        private void Tasks_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterTasksDataGrid();
        }

        private void FilterTasksDataGrid()
        {
            List<TaskDictionary> temp = tasksDictionaryList;
            if (TasksTitle.Text != null)
            {
                //like %tasksTitle.Text%
                temp = temp.Where(t => t.TaskName.ToUpper().Contains(TasksTitle.Text.ToUpper())).ToList();
            }
            if (TasksDescription.Text != null)
            {
                temp = temp.Where(t => t.TaskDescription.ToUpper().Contains(TasksDescription.Text.ToUpper())).ToList();
            }
            TasksDataGrid.ItemsSource = temp;
            if (TasksDataGrid.Columns.Count > 1) // at init is 0
                TasksDataGrid.Columns[0].IsReadOnly = true;
        }

        private void RefreshTasks_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TasksDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            TasksDataGrid.ItemsSource = tasksDictionaryList = admin.TaskDictionaryList();
            if(TasksDataGrid.Columns.Count>1)
                TasksDataGrid.Columns[0].IsReadOnly = true;
            idList = new List<int>();
            WarningLabel2.Content = "";
            SelectAll2.IsChecked = false;
            TasksTitle.Text = "";
            TasksDescription.Text = "";
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void filterUnits()
        {
            List<Unit> temp = unitList;
            if (UnitNameFilter.Text != null)
            {
                //like %tasksTitle.Text%
                temp = temp.Where(u => u.UnitName.ToUpper().Contains(UnitNameFilter.Text.ToUpper())).ToList();
            }

            UnitsDataGrid.ItemsSource = temp;
            if (UnitsDataGrid.Columns.Count > 1) // at init is 0
                UnitsDataGrid.Columns[0].IsReadOnly = true;
        }
        private void UnitNameFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterUnits();
        }
        private void RefreshUnits()
        {
            UnitsDataGrid.ItemsSource = unitList = admin.UnitList();
            filterUnits();
        }
        private void RefreshUnits_Click(object sender, RoutedEventArgs e)
        {
            RefreshUnits();
        }

        private void AddNewUnit_Click(object sender, RoutedEventArgs e)
        {
            if (UnitNameAdd.Text != null)
            { 
                admin.AddUnit(UnitNameAdd.Text);
                RefreshUnits();
                MessageBox.Show("Unit was added!");
            }
            else
            {
                MessageBox.Show("You probably provided incorrect data. Please correct it");
            }
        }
        private void Delete3_Click(object sender, RoutedEventArgs e)
        {
            if (UnitsDataGrid.SelectedItems[0] is Unit)
            {
                string str = "Unit";
                if (UnitsDataGrid.SelectedCells.Count > 1)
                    str += "s";
                if (MessageBox.Show("Do you want to delete selected " + str + "?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    foreach (var selected in UnitsDataGrid.SelectedItems)
                    {
                        if (selected is Unit)
                            admin.DeleteUnit(((Unit)selected).ID);
                    }

                RefreshUnits();
            }
        }

        private void Update3_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SelectAll3_Checked(object sender, RoutedEventArgs e)
        {
            if (SelectAll3.IsChecked == true)
            {
                UnitsDataGrid.Focus();
                UnitsDataGrid.SelectAll();
            }
        }
        private void UnitsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            UnitsDataGrid.ItemsSource= unitList =admin.UnitList();
            if (UnitsDataGrid.Columns.Count > 1)
                UnitsDataGrid.Columns[0].IsReadOnly = true;
            idList = new List<int>();
            WarningLabel3.Content = "";
            SelectAll3.IsChecked = false;
            
            UnitNameFilter.Text = "";
        }
    }
}
