﻿using System;
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
            try
            {
                admin = new Administrator(((MainWindow)Application.Current.MainWindow).GetUser());
                userList = admin.AllUsersToList();
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem please restart your application.\nIf the problem will occur once more call your software provider.");
            }
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
            try
            { 
                 userList = admin.AllUsersToList();
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
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
            try
            {
                UserDataGrid.ItemsSource = admin.AllUsersToList();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Thhere is some problem please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
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
                        try 
                        {
                            if (selected is User)
                            {
                                admin.DeleteUser(((User)selected));
                                idList.Remove(((User)selected).ID);
                            }
                        }
                        catch (Exception exception)
                        {
                                MessageBox.Show("There is some problem please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                        }
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
                            try
                            {
                                Password password = new Password(((User)selected).ID);
                                var p = password.GetPassword();
                                password.EncodePassword();
                                password.ResetPassword();
                                MessageBox.Show("The password of User with ID=" + ((User)selected).ID + " was changed successfully.\n His password is: \"" + p + "\". \nPlease note it otherwise this data will be lost.");
                                p = null;
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show("There is some problem please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                            }
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
                        try
                        {
                            admin.UpdateUser(((User)selected));
                            idList.Remove(((User)selected).ID);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                        }
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
            TaskDictionaryDataGrid.ItemsSource = temp;
            if (TaskDictionaryDataGrid.Columns.Count > 1) // at init is 0
                TaskDictionaryDataGrid.Columns[0].IsReadOnly = true;
        }

        private void RefreshTaskDictonary()
        {
            TaskDictionaryDataGrid.ItemsSource = tasksDictionaryList = admin.TaskDictionaryList();
            FilterTasksDataGrid();
            WarningLabel2.Content = "";
            idList = new List<int>();
        }
        private void RefreshTasks_Click(object sender, RoutedEventArgs e)
        {
            RefreshTaskDictonary();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddToDictionaryWindow addToDictionryInstance = new AddToDictionaryWindow(admin);
            RefreshListEvent += new RefreshList(RefreshTaskDictonary);
            addToDictionryInstance.AddTaskDel = RefreshListEvent;
            addToDictionryInstance.Show();
        }
        private void SelectAll2_Checked(object sender, RoutedEventArgs e)
        {
            if (SelectAll2.IsChecked == true)
            {
                TaskDictionaryDataGrid.Focus();
                TaskDictionaryDataGrid.SelectAll();
            }
        }
        private void TasksDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                TaskDictionaryDataGrid.ItemsSource = tasksDictionaryList = admin.TaskDictionaryList();
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
            if (TaskDictionaryDataGrid.Columns.Count > 1)
                TaskDictionaryDataGrid.Columns[0].IsReadOnly = true;
            idList = new List<int>();
            WarningLabel2.Content = "";
            SelectAll2.IsChecked = false;
            TasksTitle.Text = "";
            TasksDescription.Text = "";
        }

        private void Update2_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selected in TaskDictionaryDataGrid.SelectedItems)
            {
                if (selected is TaskDictionary && idList.Contains(((TaskDictionary)selected).ID))
                {
                    try 
                    { 
                        admin.UpdateTaskDictionary(((TaskDictionary)selected));
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                    }
                    idList.Remove(((TaskDictionary)selected).ID);
                }

            }
            if (idList.Count == 0)
            {
                WarningLabel2.Content = "";
                idList = new List<int>();
            }
            else
            {
                WarningLabel2.Content = "Non committed changes for task definitions with ids: ";
                foreach (var id in idList)
                {
                    WarningLabel2.Content += id.ToString() + ", ";
                }
            }
        }
        private void Delete2_Click(object sender, RoutedEventArgs e)
        {
            if (TaskDictionaryDataGrid.SelectedItems[0] is TaskDictionary)
            {
                string str = "task definition";
                if (TaskDictionaryDataGrid.SelectedCells.Count > 1)
                    str += "s";
                if (MessageBox.Show("Do you want to change selected " + str + "?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    foreach (var selected in TaskDictionaryDataGrid.SelectedItems)
                    {
                        try
                        {
                            if (selected is TaskDictionary)
                            {
                                admin.DeleteFromTaskDictionary((TaskDictionary)selected);
                                idList.Remove(((TaskDictionary)selected).ID);
                            }
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                        }
            }

                RefreshTaskDictonary();
            }
        }
        private void TaskDictionaryDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (TaskDictionaryDataGrid.SelectedItem is TaskDictionary)
            {
                if (!idList.Contains(((TaskDictionary)TaskDictionaryDataGrid.SelectedItem).ID))
                {
                    idList.Add(((TaskDictionary)TaskDictionaryDataGrid.SelectedItem).ID);
                    if (idList.Count == 1)
                    {
                        WarningLabel2.Content = "Non committed changes for task definitions with ids: ";
                    }
                    var s = ((TaskDictionary)(TaskDictionaryDataGrid.SelectedItem)).ID.ToString();
                    WarningLabel2.Content += s + ", ";
                }
            }
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            admin.logOut();
        }

        private void FilterUnits()
        {
            List<Unit> temp = unitList;
            if (UnitNameFilter.Text != null)
            {
                //like %tasksTitle.Text%
                temp = temp.Where(u => u.UnitName.ToUpper().Contains(UnitNameFilter.Text.ToUpper())).ToList();
            }

            UnitDataGrid.ItemsSource = temp;
            if (UnitDataGrid.Columns.Count > 1) // at init is 0
                UnitDataGrid.Columns[0].IsReadOnly = true;
            
        }
        private void UnitNameFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterUnits();
        }
        private void RefreshUnits()
        {
            try 
            { 
                 UnitDataGrid.ItemsSource = unitList = admin.UnitList();
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
            FilterUnits();
            WarningLabel3.Content = null;
            idList = new List<int>();
        }
        private void RefreshUnits_Click(object sender, RoutedEventArgs e)
        {
            RefreshUnits();
        }

        private void AddNewUnit_Click(object sender, RoutedEventArgs e)
        {
            if (UnitNameAdd.Text != null)
            {
                try
                { 
                    admin.AddUnit(UnitNameAdd.Text);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                }
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
            if (UnitDataGrid.SelectedItems[0] is Unit)
            {
                string str = "Unit";
                if (UnitDataGrid.SelectedCells.Count > 1)
                    str += "s";
                if (MessageBox.Show("Do you want to delete selected " + str + "?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    foreach (var selected in UnitDataGrid.SelectedItems)
                    {
                        if (selected is Unit)
                        {
                            try
                            { 
                                admin.DeleteUnit(((Unit)selected).ID);
                                idList.Remove(((Unit)selected).ID);
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                            }
                        }

                    }
                RefreshUnits();
            }
        }

        private void Update3_Click(object sender, RoutedEventArgs e)
        {
            foreach (var selected in UnitDataGrid.SelectedItems)
            {
                if (selected is Unit && idList.Contains(((Unit)selected).ID))
                {
                    try 
                    { 
                        admin.UpdateUnit(((Unit)selected));
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
                    }
                    idList.Remove(((Unit)selected).ID);
                }

            }
            if (idList.Count == 0)
            {
                WarningLabel3.Content = "";
                idList = new List<int>();
            }
            else
            {
                WarningLabel3.Content = "Non committed changes for users with ids: ";
                foreach (var id in idList)
                {
                    WarningLabel3.Content += id.ToString() + ", ";
                }
            }
        }
        private void SelectAll3_Checked(object sender, RoutedEventArgs e)
        {
            if (SelectAll3.IsChecked == true)
            {
                UnitDataGrid.Focus();
                UnitDataGrid.SelectAll();
            }
        }
        private void UnitsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                UnitDataGrid.ItemsSource= unitList =admin.UnitList();
            }
            catch (Exception exception)
            {
                MessageBox.Show("There is some problem, please try once more or restart your application.\nIf the problem will occur once more call your software provider.");
            }
            if (UnitDataGrid.Columns.Count > 1)
                UnitDataGrid.Columns[0].IsReadOnly = true;
            idList = new List<int>();
            WarningLabel3.Content = "";
            SelectAll3.IsChecked = false;
            
            UnitNameFilter.Text = "";
        }

        private void UnitsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (UnitDataGrid.SelectedItem is Unit)
            {
                if (!idList.Contains(((Unit)UnitDataGrid.SelectedItem).ID))
                {
                    idList.Add(((Unit)UnitDataGrid.SelectedItem).ID);
                    if (idList.Count == 1)
                    {
                        WarningLabel3.Content = "Non committed changes for units with ids: ";
                    }
                    var s = ((Unit)(UnitDataGrid.SelectedItem)).ID.ToString();
                    WarningLabel3.Content += s + ", ";
                }
            }
        }

       
    }
}
