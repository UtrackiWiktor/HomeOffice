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
using HomeOffice.classes.Passwords;
using System.Globalization;

namespace HomeOffice
{
    /// <summary>
    /// Logika interakcji dla klasy AddUser.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        User admin;
        public Delegate addUserDel;

        private void SetUser(User u)
        {
            admin = new Administrator(u);
        }
        public AddUserWindow(User u)
        {

            SetUser(u);
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime;
            try
            {
                dateTime = DateTime.ParseExact(UserDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                int unit;
                if (!String.IsNullOrWhiteSpace(UserName.Text) && !String.IsNullOrWhiteSpace(UserSurname.Text) && !String.IsNullOrWhiteSpace(UserDate.Text) && int.TryParse(UserUnit.Text, out unit))
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

                    admin.AddUser(newUser);
                    Password password = new Password(newUser.ID);
                    var p = password.GetPassword();
                    password.EncodePassword();
                    admin.AddPassword(password);
                    MessageBox.Show("User was added successfully.\n His password is: \"" + p + "\". \nPlease note it otherwise this data will be lost.");
                    password = null;//wipe data
                    addUserDel.DynamicInvoke();
                    this.Close();
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
    }
}
